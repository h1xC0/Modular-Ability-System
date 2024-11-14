using System;
using Core.CommandBindings.Payloads;
using Core.CommandBindings.Signals;
using Core.Services.GameFactory;
using Core.Services.SceneTransition;
using Core.Systems.CommandSystem;
using Game.Level.Environment.Spawpoints;
using Shared.Extensions;
using UI.WindowSystem;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [Serializable]
    public class GameEntryPoint
    {
        private readonly DiContainer _container;
        private IGameFactory _gameFactory;
        private IWindowManager _windowManager;
        private ISceneTransitionService _sceneTransition;
        private readonly SpawnPoint _playerSpawn;
        private readonly int _targetFrameRate;
        private readonly bool _enableLogger;

        private bool _canClosePauseMenu;
        private bool _wasCancelled;


        public GameEntryPoint(DiContainer container,
            SpawnPoint playerSpawn,
            int targetFrameRate,
            bool enableLogger)
        {
            _container = container;
            _playerSpawn = playerSpawn;
            _targetFrameRate = targetFrameRate;
            _enableLogger = enableLogger;
            _windowManager = _container.Resolve<IWindowManager>();
            _sceneTransition = _container.Resolve<ISceneTransitionService>();
        }

        public void Initialize()
        {
            SetGeneralSettings(_targetFrameRate, _enableLogger);
            CursorExtensions.AllowCursor(false);

            _sceneTransition.SceneLoadEvent += DispatchGameplayCommand;
        }

        private void SetGeneralSettings(int targetFrameRate, bool enableLogger)
        {
            Application.targetFrameRate = targetFrameRate;
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = enableLogger;
#else
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
#endif
        }

        private void DispatchGameplayCommand()
        {
            var commandDispatcher = _container.Resolve<ICommandDispatcher>();
            commandDispatcher.Dispatch<SetupGameplaySignal>(new GameplayPayload(_playerSpawn, Setup));
        }

        private void Setup(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Dispose()
        {
            if (_gameFactory.PlayerActions == null)
                return;
            
            _sceneTransition.SceneLoadEvent -= DispatchGameplayCommand;
            Debug.Log("<color=red>GameEntryPoint was disposed</color>");
        }
    }
}