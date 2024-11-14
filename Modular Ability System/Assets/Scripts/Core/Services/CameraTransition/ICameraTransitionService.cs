using DG.Tweening;
using UnityEngine;

namespace Core.Services.CameraTransition
{
    public interface ICameraTransitionService
    {
        void SetupCamera(Camera camera, Transform playerEye);
        Sequence To(Transform target, Camera camera = null);
        Sequence Out(Camera camera = null, Transform playerEye = null);
        Sequence LookAt(Vector3 point, Camera camera = null);
        Sequence AddPositionNoise(Transform target, float duration, float strength);
        Sequence AddRotationNoise(Transform target, float duration, float strength);
        Sequence JumpTo(Transform target, Camera camera = null);
    }
}