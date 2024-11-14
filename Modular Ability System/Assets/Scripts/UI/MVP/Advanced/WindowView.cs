using Shared.Enums;
using UI.UIExtensions;
using UnityEngine;

namespace UI.MVP.Advanced
{
    [RequireComponent(typeof(ViewAnimation))]
    public class WindowView : ManagedView, IWindowView
    {
        public ViewAnimation ViewAnimation { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            ViewAnimation = GetComponent<ViewAnimation>();

            switch (ViewAnimation.WindowAnimationType)
            {
                case WindowAnimationType.Scale:
                    break;
                case WindowAnimationType.Fade:
                    ViewAnimation.CanvasGroup.alpha = 0;
                    ViewAnimation.CanvasGroup.interactable = ViewAnimation.CanvasGroup.blocksRaycasts = false;
                    break;
                case WindowAnimationType.Jump:
                    break;
                case WindowAnimationType.Float:
                    ViewAnimation.CanvasGroup.alpha = 0;
                    ViewAnimation.CanvasGroup.interactable = ViewAnimation.CanvasGroup.blocksRaycasts = false;
                    break;
                case WindowAnimationType.SpriteChange:
                    break;
                case WindowAnimationType.Size:
                    break;
            }
        }
    }
}