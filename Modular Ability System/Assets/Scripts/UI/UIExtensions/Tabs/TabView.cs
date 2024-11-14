using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIExtensions.Tabs
{
    public class TabView : RawView, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action TabSelectedEvent;

        public TabContent Content => _content;
        
        [SerializeField] private TabContent _content;
        [SerializeField] private Image _underline;
        [SerializeField] private TMP_Text _tmpText;

        private Color _selectedColor = new(0.96f, 0.93f, 0.88f, 1f);
        private Color _highlightedColor = new(0.96f, 0.93f, 0.88f, 0.5f);
        private Color _normalColor = new(0.96f, 0.93f, 0.88f, 0.08f);

        private bool _selected;

        public void OnSelect()
        {
            SetUnderLine(true);
            _tmpText.color = _selectedColor;
            _selected = true;

            if (_content == null)
                return;

            _content.gameObject.SetActive(true);
        }

        private void SetHighlight(bool flag)
        {
            _tmpText.color = _selected 
                ? _selectedColor : flag 
                    ? _highlightedColor : _normalColor;
        }

        public void OnDeselect()
        {
            SetUnderLine(false);
            _tmpText.color = _normalColor;
            _selected = false;
            
            if (_content == null)
                return;

            _content.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHighlight(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHighlight(false);
        }

        private void SetUnderLine(bool flag)
        {
            _underline.gameObject.SetActive(flag);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TabSelectedEvent?.Invoke();
            _selected = true;
        }
    }
}