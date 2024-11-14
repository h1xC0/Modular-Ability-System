using Core.Services.ResourceProvider;
using Game.Player;
using Shared.Factories;
using UnityEngine;
using Zenject;

namespace Core.Services.GameFactory
{
    public class GameFactory : AbstractFactory, IGameFactory
    {
        private readonly IResourceProviderService _resourceProviderService;
        private GameCharacter _gameCharacterFacade;
        
        public PlayerInputMap PlayerActions { get; private set; }
        public IGameCharacter GameCharacter => _gameCharacterFacade;
        
        public GameFactory(DiContainer diContainer, IResourceProviderService resourceProviderService) : base(diContainer)
        {
            _resourceProviderService = resourceProviderService;
        }

        public IGameCharacter CreatePlayer()
        {
            var resource = _resourceProviderService.LoadResource<GameCharacter>();
            _gameCharacterFacade = CreateObject<GameCharacter>(resource.gameObject);
            return _gameCharacterFacade;
        }

        public PlayerInputMap CreatePlayerActions()
        {
            PlayerActions = new PlayerInputMap();
            DiContainer.Inject(PlayerActions);
            return PlayerActions;
        }

        public void Dispose()
        {
            Debug.Log("<color=red>Gamefactory was disposed</color>");
        }
    }
}