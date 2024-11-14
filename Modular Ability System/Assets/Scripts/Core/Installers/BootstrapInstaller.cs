using Core.CommandBindings.Payloads;
using Core.CommandBindings.Signals;
using Core.Systems.CommandSystem;
using Shared.Constants;
using Zenject;

namespace Core.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        private ICommandDispatcher _commandDispatcher;
        
        public override void InstallBindings()
        {
            _commandDispatcher = Container.Resolve<ICommandDispatcher>();
            LoadGameScene();
        }

        private void LoadGameScene()
        {
            _commandDispatcher.Dispatch<LoadNextSceneSignal>(new SceneNamePayload(SceneNames.Game));
        }
    }
}