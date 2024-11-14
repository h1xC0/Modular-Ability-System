using Core.Services.CameraTransition;
using Core.Services.GameFactory;
using Core.Services.ResourceProvider;
using Shared.Constants;
using UI.WindowSystem;

namespace Game.Player
{
    public interface IGameCharacter : IResource
    {
        void Initialize(PlayerConfiguration playerConfiguration,
            LayerMaskConfiguration layerMaskConfiguration,
            ICameraTransitionService cameraTransitionService,
            PlayerInputMap playerActions,
            IWindowManager windowManager);
    }
}