using UI.WindowSystem;

namespace UI.Windows.HUDWindow
{
    public class HudMapper : WindowMapper<HudPresenter, HudView, HudModel>
    {
        protected HudMapper(IWindowRegistration windowRegister, IWindowManagerFactory windowFactory) : base(windowRegister, windowFactory)
        {
            
        }
    }
}
