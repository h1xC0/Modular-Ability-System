using System;
using Core.CommandBindings.Payloads;
using Core.Services.CameraTransition;
using Core.Services.GameFactory;
using Core.Services.ResourceProvider;
using Core.Systems.CommandSystem;
using Game.Player;
using Shared.Constants;
using UI.WindowSystem;

namespace Core.CommandBindings.Commands
{
    public class SetupPlayerCommand : Command
    {
        private readonly IGameFactory _gameFactory;
        private readonly IResourceProviderService _resourceProviderService;
        private readonly ICameraTransitionService _cameraTransitionService;
        private readonly IWindowManager _windowManager;

        public SetupPlayerCommand(IGameFactory gameFactory,
            IResourceProviderService resourceProviderService,
            ICameraTransitionService cameraTransitionService,
            IWindowManager windowManager)
        {
            _gameFactory = gameFactory;
            _resourceProviderService = resourceProviderService;
            _cameraTransitionService = cameraTransitionService;
            _windowManager = windowManager;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            
            var gameplayPayload = (GameplayPayload)payload;
            var playerConfiguration = _resourceProviderService.LoadResource<PlayerConfiguration>();
            var layerMaskConfiguration = _resourceProviderService.LoadResource<LayerMaskConfiguration>();

            var player = _gameFactory.CreatePlayer() as GameCharacter;
            if (player == null)
                throw new InvalidCastException($"Can't be casted to type {typeof(GameCharacter)}");
            
            player.Initialize(playerConfiguration,
                layerMaskConfiguration,
                _cameraTransitionService,
                _gameFactory.CreatePlayerActions(),
                _windowManager);
            
            // SoundService.Instance.Initialize(Object.FindObjectOfType<AudioListener>());
            
            gameplayPayload.PlayerSpawn.SetSpawn(player.gameObject);
            gameplayPayload.Action.Invoke(_gameFactory);
            
            Release();
        }
    }
}