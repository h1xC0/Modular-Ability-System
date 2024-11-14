using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIExtensions.Selectables
{
    public abstract class SelectableOption : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler, ISubmitHandler
    {
        public event Action<bool> SelectedEvent;
        public event Action DisposeEvent;
        [HideInInspector] public Selectable Selectable;
        
        public virtual void Initialize()
        {
            Selectable = GetComponent<Selectable>();
        }

        public void SetupNavigation(Selectable previousButton, Selectable nextButton, Navigation.Mode navigationMode)
        {
            Navigation navigation = Selectable.navigation;
            navigation.mode = Navigation.Mode.Explicit;

            switch (navigationMode)
            {
                case Navigation.Mode.Vertical:
                    navigation.selectOnUp = previousButton;
                    navigation.selectOnDown = nextButton;
                    break;
                case Navigation.Mode.Horizontal:
                    navigation.selectOnRight = nextButton;
                    navigation.selectOnLeft = previousButton;
                    break;
            }

            Selectable.navigation = navigation;
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
            SelectedEvent?.Invoke(true);
        }

        public virtual void OnDeselect(BaseEventData eventData)
        {
            SelectedEvent?.Invoke(false);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            
        }


        private void OnDisable()
        {
            DisposeEvent?.Invoke();
        }
    }
}