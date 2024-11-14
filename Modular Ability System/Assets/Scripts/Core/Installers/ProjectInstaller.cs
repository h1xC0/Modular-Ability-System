using Core.CommandBindings.Commands;
using Core.CommandBindings.Signals;
using Core.Services.ResourceProvider;
using Core.Services.SceneTransition;
using Core.Services.ViewLayerService;
using Core.Services.WindowAnimation;
using Core.Systems;
using Core.Systems.CommandSystem;
using Core.Systems.EventBus;
using UI.WindowSystem;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ViewLayerService _viewLayerService;
        [SerializeField] private SceneTransitionService _sceneTransitionService;
        
        public override void InstallBindings()
        {
            BindInstallers();
            BindServices();
            BindCommands();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesTo<ViewLayerService>()
                .FromComponentInNewPrefab(_viewLayerService)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<SceneTransitionService>()
                .FromComponentInNewPrefab(_sceneTransitionService)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<WindowAnimationService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<ResourceProviderService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<EventBusService>()
                .FromNew()
                .AsSingle();
        }

        private void BindInstallers()
        {
            Container.Install<WindowInstaller>();
            Container.Install<CommandSystemInstaller>();
        }
        
        private void BindCommands()
        {
            var commandBinder = Container.Resolve<ICommandBinder>();
            commandBinder.Bind<LoadSceneSignal>()
                .To<LoadSceneCommand>();
        
            commandBinder.Bind<UnloadSceneSignal>()
                .To<UnloadSceneCommand>();
            
            commandBinder
                .Bind<LoadNextSceneSignal>()
                .To<LoadSceneCommand>()
                .To<UnloadSceneCommand>();
        }
    }
}
