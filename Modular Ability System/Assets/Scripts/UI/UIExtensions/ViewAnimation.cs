using System;
using System.Collections.Generic;
using Core.Services.ResourceProvider;
using Core.Services.WindowAnimation;
using DG.Tweening;
using Shared.Enums;
using Shared.ExtensionTools.Attributes.ReadOnly;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIExtensions
{
    public class ViewAnimation : MonoBehaviour
    {
        public WindowAnimationType WindowAnimationType;
        public ViewAnimationState State { get; private set; }
        public bool UseUnscaledTime { get; set; }
        
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Image Image => _image;
        public Vector3 Offset => _offset;

        [ReadOnly] public Vector3 OriginalPosition;

        public const float In = 1;
        public const float Out = 0;

        private IWindowAnimationService _windowAnimationService;

        [SerializeField] private bool _playOnEnable;
        [SerializeField] private bool _playOnStart;
        [SerializeField] [HideInInspector] private CanvasGroup _canvasGroup;
        [SerializeField] [HideInInspector] private Sprite _newSprite;
        [SerializeField] [HideInInspector] private Sprite _oldSprite;
        [SerializeField] [HideInInspector] private Image _image;

        [SerializeField] [HideInInspector] private Vector2 _originalSize;
        [SerializeField] [HideInInspector] private Vector3 _offset;

        [SerializeField] private List<ViewAnimation> _subAnimations;
        [SerializeField] private List<ViewAnimation> _joinedAnimations;
        
        private RectTransform _rectTransform;

        private void OnEnable()
        {
            if (_playOnEnable == false) return;
            _windowAnimationService ??= new WindowAnimationService(new ResourceProviderService());
            
            Setup();
            ViewSequence(In).Play();
        }

        private void Start()
        {
            if (_playOnStart == false) return;
            _windowAnimationService ??= new WindowAnimationService(new ResourceProviderService());
            
            Setup();
            ViewSequence(In).Play();
        }

        public void Initialize(IWindowAnimationService windowAnimationService)
        {
            UseUnscaledTime = true;
            _windowAnimationService = windowAnimationService;
            Setup();
        }

        private void Setup()
        {
            _rectTransform = (RectTransform)transform;
            OriginalPosition = transform.position;
            
            if (WindowAnimationType == WindowAnimationType.SpriteChange)
                _oldSprite = _image.sprite;
        }
        
        public Sequence ViewSequence(float value)
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
            var sequence = DOTween.Sequence();
            sequence.SetTarget(_canvasGroup is not null ? _canvasGroup : _image);
            bool tryImage = _canvasGroup is null;
            
            switch (WindowAnimationType)
            {
                case WindowAnimationType.Fade:
                    sequence = tryImage == false 
                        ? _windowAnimationService.FadeInOut(CanvasGroup, value) 
                        : _windowAnimationService.FadeInOut(Image, value);
                    break;
                case WindowAnimationType.Scale:
                        sequence = _windowAnimationService.ScaleInOut(transform, value);
                    break;
                case WindowAnimationType.Jump:
                        sequence = _windowAnimationService.JumpInOut(transform, Offset, OriginalPosition, value);
                    break;
                case WindowAnimationType.Float:
                    sequence = tryImage == false 
                        ? _windowAnimationService.FloatInOut(CanvasGroup, Offset, OriginalPosition, value) 
                        : _windowAnimationService.FloatInOut(Image, Offset, OriginalPosition, value);
                    break;
                case WindowAnimationType.SpriteChange:
                    sequence = _windowAnimationService.SpriteChange(_image, _oldSprite, _newSprite, value);
                    break;
                case WindowAnimationType.Size:
                    sequence = _windowAnimationService.SizeInOut(_rectTransform, _originalSize, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            foreach (var joinAnimation in _joinedAnimations)
            {
                joinAnimation.Initialize(_windowAnimationService);
                sequence.Join(joinAnimation.ViewSequence(value));
            }
            
            foreach (var subAnimation in _subAnimations)
            {
                subAnimation.Initialize(_windowAnimationService);
                if (value == 0)
                {
                    sequence.Prepend(subAnimation.ViewSequence(value));
                }
                else
                {
                    sequence.Append(subAnimation.ViewSequence(value));
                }
            }

            sequence.AppendCallback(() => State = value == 1 ? ViewAnimationState.In : ViewAnimationState.Out);
            sequence.SetUpdate(UseUnscaledTime);
            return sequence;
        }
    }
}
