using UI.MVP;

namespace UI.UIExtensions.Tabs
{
    public interface ITabNavigationView : IView
    {
        void SetTabNavigation(bool flag);
        void NextTab();
        void PreviousTab();
        TabView GetSelectedTab();
    }
}