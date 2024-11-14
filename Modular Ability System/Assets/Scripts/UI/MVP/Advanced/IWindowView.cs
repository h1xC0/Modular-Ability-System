using UI.UIExtensions;

namespace UI.MVP.Advanced
{
    public interface IWindowView : IManagedView
    {
        ViewAnimation ViewAnimation { get; }
    }
}