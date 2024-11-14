using Core.Systems.CommandSystem;
using Zenject;

namespace Core.Systems
{
    public class CommandSystemInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<CommandBinder>()
                .FromNew()
                .AsSingle()
                .CopyIntoAllSubContainers();

            Container
                .BindInterfacesTo<CommandDispatcher>()
                .FromNew()
                .AsSingle();
        }
    }
}
