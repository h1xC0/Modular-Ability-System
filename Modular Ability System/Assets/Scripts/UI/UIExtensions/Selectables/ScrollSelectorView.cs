using DG.Tweening;
using Shared.Constants;
using Shared.ExtensionTools;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIExtensions.Selectables
{
    public class ScrollSelectorView : SelectorView
    {
        private const float SelectedScale = 1.025f;
        
        public RectTransform ContentRoot => _contentRoot;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentRoot;

        public override void Initialize()
        {
            foreach (var selectableOption in _selectables)
            {
                selectableOption.SelectedEvent += flag => OnSelection(flag, selectableOption);
            }
            base.Initialize();
        }
        
        private void OnSelection(bool flag, SelectableOption selectableOption)
        {
            _scrollRect.content = _contentRoot;
            selectableOption.transform.DOScale(flag ? Vector3.one * SelectedScale : Vector3.one, AnimationConstants.Quarter);

            if (flag)
            {
                StartCoroutine(_scrollRect.FocusOnItemCoroutine((RectTransform)selectableOption.transform, 3f));
            }
        }
    }
}