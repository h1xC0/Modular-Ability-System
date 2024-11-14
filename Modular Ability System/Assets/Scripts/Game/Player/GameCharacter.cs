using Core.Services.CameraTransition;
using Core.Services.GameFactory;
using Shared.Constants;
using UI.WindowSystem;
using UnityEngine;

namespace Game.Player
{
    public class GameCharacter : MonoBehaviour, IGameCharacter
    {
        public void Initialize(PlayerConfiguration playerConfiguration,
            LayerMaskConfiguration layerMaskConfiguration,
            ICameraTransitionService cameraTransitionService,
            PlayerInputMap playerActions,
            IWindowManager windowManager)
        {
            
        }
    }
}