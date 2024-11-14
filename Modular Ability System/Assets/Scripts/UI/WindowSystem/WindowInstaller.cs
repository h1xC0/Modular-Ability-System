using Zenject;

namespace UI.WindowSystem
{
    public class WindowInstaller : Installer
    {
        public override void InstallBindings()
        {
            BindManagers();
        }
        
        private void BindManagers()
        {
            Container
                .BindInterfacesTo<WindowManagerFactory>()
                .FromNew()
                .AsSingle()
                .CopyIntoAllSubContainers();

            Container
                .BindInterfacesTo<WindowManager>()
                .FromNew()
                .AsSingle()
                .CopyIntoAllSubContainers();
        }
    }
}