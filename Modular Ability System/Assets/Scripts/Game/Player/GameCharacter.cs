using Core.Services.CameraTransition;
using Core.Services.GameFactory;
using Game.Combat;
using Shared.Constants;
using UI.WindowSystem;
using UnityEngine;

namespace Game.Player
{
    public class GameCharacter : MonoBehaviour, IGameCharacter
    {
        public IHealthComponent HealthComponent => _healthComponent;
        private HealthComponent _healthComponent;
        private PlayerConfiguration _playerConfiguration;
        private LayerMaskConfiguration _layerMaskConfiguration;
        private ICameraTransitionService _cameraTransitionService;
        private PlayerInputMap _playerActions;
        private IWindowManager _windowManager;

        public void Initialize(PlayerConfiguration playerConfiguration,
            LayerMaskConfiguration layerMaskConfiguration,
            ICameraTransitionService cameraTransitionService,
            PlayerInputMap playerActions,
            IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _playerActions = playerActions;
            _cameraTransitionService = cameraTransitionService;
            _layerMaskConfiguration = layerMaskConfiguration;
            _playerConfiguration = playerConfiguration;
            
            
            _healthComponent = new HealthComponent();
            _playerActions.Enable();
        }
    }
}