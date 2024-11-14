using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP
{
    public class ManagedView : MonoBehaviour, IManagedView
    {
        [SerializeField] protected Button CloseButton;
        public event Action CloseAction;
        
        protected RectTransform RectTransform => (RectTransform)transform;

        public event Action<bool> OpenEvent;
        public event Action<bool> CloseEvent;
        public event Action CloseCompleteEvent;
        public event Action OpenCompleteEvent;

        public virtual void Initialize()
        {
            if (CloseButton == null) return;
            CloseButton.onClick.AddListener(() => CloseAction?.Invoke());
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public void Open(bool animated)
        {
            OpenEvent?.Invoke(animated);
        }

        public void Close(bool animated)
        {
            CloseEvent?.Invoke(animated);
        }
        
        public void OnOpenComplete()
        {
            OpenCompleteEvent?.Invoke();
        }
        
        public void OnCloseComplete()
        {
            CloseCompleteEvent?.Invoke();
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
            CloseButton?.onClick.RemoveListener(() => CloseAction?.Invoke());
        }
    }
}