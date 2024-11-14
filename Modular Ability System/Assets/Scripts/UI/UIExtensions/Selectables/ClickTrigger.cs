using System;
using DG.Tweening;
using Shared.Constants;
using Shared.Extensions.Rx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIExtensions.Selectables
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ClickTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDisposable, IPointerDownHandler, IPointerUpHandler
    {
        public IReactiveProperty<bool> Interactable => _interactable;
        [SerializeField] private ReactiveProperty<bool> _interactable = new(true);
        [HideInInspector] public UnityEvent OnClick;
        [HideInInspector] public UnityEvent OnClickComplete;
        [SerializeField] private MaskableGraphic _graphic;
        
        private CanvasGroup _canvasGroup;

        private const float DisableMultiplier = 0.78f;
        private const float DisableAlpha = 0.5f;
        private const float NormalAlpha = 1f;
        private const float HighlightedAlpha = 0.9f;
        private const float SelectedAlpha = 0.96f;
        
        private Color _disabledColor;
        private Color _normalColor;
        private Color _highlightedColor;
        private Color _selectedColor;

        private const float HoldingDuration = 0.3f;
        private bool _holding;
        private float _holdingTimer;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = NormalAlpha;
            
            SetColors();
            _interactable.Subscribe(SetTriggerInteraction);
        }

        private void Update()
        {
            ApplyHolding();
        }

        private void ApplyHolding()
        {
            if (_holding && _holdingTimer <= HoldingDuration)
            {
                _holdingTimer += Time.unscaledDeltaTime;
            }

            if (_holdingTimer >= HoldingDuration)
            {
                if(_interactable.Value == false)
                    return;
                
                OnClick?.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_interactable.Value == false)
                return;
            
            _canvasGroup.DOFade(HighlightedAlpha, AnimationConstants.Half);
            _graphic.DOColor(_highlightedColor, AnimationConstants.Half);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _holding = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_interactable.Value == false)
                return;

            OnClick?.Invoke();
            
            _canvasGroup.DOFade(SelectedAlpha, AnimationConstants.Quarter);
            _graphic.DOColor(_selectedColor, AnimationConstants.Half)
                .OnComplete(() => OnClickComplete?.Invoke());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _holding = false;
            _holdingTimer = 0;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_interactable.Value == false)
                return;

            _canvasGroup?.DOFade(NormalAlpha, AnimationConstants.Half);
            _graphic?.DOColor(_normalColor, AnimationConstants.Half);
        }

        private void SetTriggerInteraction(bool flag)
        {
            _canvasGroup.DOFade(flag ? NormalAlpha : DisableAlpha, AnimationConstants.Half);
            _graphic.DOColor(flag ? _normalColor : _disabledColor, AnimationConstants.Half);
            _canvasGroup.interactable = _canvasGroup.blocksRaycasts = flag;
        }

        private void SetColors()
        {
            var color = _graphic.color;

            _disabledColor = new Color(1 - DisableMultiplier, 1 - DisableMultiplier, 1 - DisableMultiplier);
            _normalColor = new Color(color.r, color.g, color.b, NormalAlpha);
            _highlightedColor = new Color(color.r - (1 - HighlightedAlpha), color.g - (1 - HighlightedAlpha), color.b - (1 - HighlightedAlpha));
            _selectedColor = new Color(color.r - (1 - SelectedAlpha), color.g - (1 - SelectedAlpha), color.b - (1 - SelectedAlpha));
        }

        public void Dispose()
        {
            _canvasGroup?.DOKill();
            _graphic?.DOKill();
            
            OnClick.RemoveAllListeners();
            OnClickComplete.RemoveAllListeners();
        }
    }
}