using Core.CommandBindings.Commands;
using Core.CommandBindings.Signals;
using Core.Services.CameraTransition;
using Core.Services.GameFactory;
using Core.Systems.CommandSystem;
using Game.Level.Environment.Spawpoints;
using UI.Windows.HUDWindow;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPoint _playerSpawn;
        [SerializeField] private int _targetFrameRate = 60;
        [SerializeField] private bool _enableLogger;

        private GameEntryPoint _gameEntryPoint;
        private ICommandBinder _commandBinder;
        private ICommandDispatcher _commandDispatcher;

        public override void InstallBindings()
        {
            BindServices();
            BindCommands();
            MapWindows();
            _gameEntryPoint = CreateEntryPoint();
            _gameEntryPoint.Initialize();
        }

        private GameEntryPoint CreateEntryPoint() =>
            _gameEntryPoint = new GameEntryPoint(Container,
                _playerSpawn,
                _targetFrameRate, 
                _enableLogger);

        private void BindServices()
        {
            Container
                .BindInterfacesTo<GameFactory>()
                .FromNew()
                .AsSingle();
            
            Container
                .BindInterfacesTo<CameraTransitionService>()
                .FromNew()
                .AsSingle();
        }
        
        private void BindCommands()
        {
            _commandBinder = Container.Resolve<ICommandBinder>();
            _commandDispatcher = Container.Resolve<ICommandDispatcher>();

            if (_commandDispatcher.HasListener(typeof(SetupGameplaySignal)))
            {
                return;
            }

            _commandBinder
                .Bind<SetupGameplaySignal>()
                .To<SetupUICommand>()
                .To<SetupPlayerCommand>();
        }
        
        private void MapWindows()
        {
            Container
                .BindInterfacesAndSelfTo<HudMapper>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void OnApplicationQuit()
        {
            _gameEntryPoint.Dispose();
        }
    }
}
