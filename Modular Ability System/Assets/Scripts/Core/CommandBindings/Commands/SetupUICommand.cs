using Core.Systems.CommandSystem;
using UI.MVP;
using UI.Windows.HUDWindow;
using UI.WindowSystem;

namespace Core.CommandBindings.Commands
{
    public class SetupUICommand : Command
    {
        private readonly IWindowManager _windowManager;

        public SetupUICommand(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        
        protected override void Execute(ICommandPayload commandPayload)
        {
            Retain();
            _windowManager.Open<HudPresenter>(ViewState.Open);
            Release();
        }
    }
}