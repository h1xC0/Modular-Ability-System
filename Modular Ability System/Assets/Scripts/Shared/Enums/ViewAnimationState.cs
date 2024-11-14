
namespace Shared.Enums
{
    public enum ViewAnimationState : byte
    {
        Out,
        In
    }

    public static class ViewAnimationExt
    {
        public static bool ToBool(this ViewAnimationState viewAnimationState) =>
            viewAnimationState switch
            {
                ViewAnimationState.Out => false,
                ViewAnimationState.In => true,
                _ => false
            };
    }
}