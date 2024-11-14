using System.Collections.Generic;
using DG.Tweening;
using Shared.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Shared.ExtensionTools
{
    [RequireComponent(typeof(Image))]
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private bool _playOnStart = true;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _fillDuration = 0.25f;
        
        [SerializeField] private List<Sprite> _sprites;

        private Image _image;
        private Sequence _sequence;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _sequence = GetAnimation();
        }

        private void Start()
        {
            if (_playOnStart)
                _sequence.Play();
        }

        public void Play()
        {
            _sequence.Play();
        }

        public void Pause()
        {
            _sequence.Pause();
        }
        
        private Sequence GetAnimation()
        {
            var sequence = (_sprites == null || _sprites.Count == 0) && _image.sprite != null ? SpriteRotate() : SpriteChange();
            return sequence;
        }

        private Sequence SpriteChange()
        {
            var sequence = DOTween.Sequence();

            foreach (var sprite in _sprites)
            {
                sequence.AppendCallback(() => _image.sprite = sprite);
                sequence.AppendInterval(AnimationConstants.OneTenth);
            }
            
            sequence.SetDelay(.5f);
            sequence.SetLoops(-1, LoopType.Yoyo);

            return sequence;
        }

        private Sequence SpriteRotate()
        {
            var sequence = DOTween.Sequence();
                
            sequence.AppendCallback(() => _image.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime));
            sequence.AppendCallback(() => _image.fillAmount = Mathf.PingPong(Time.time * AnimationConstants.Quarter, .8f) + AnimationConstants.OneTenth);
                
            sequence.SetLoops(-1, LoopType.Incremental); 
            
            sequence.OnPause(() => _image
                    .DOFillAmount(1, 1)
                    .SetEase(Ease.InOutCubic));

            return sequence;
        }
    }
}