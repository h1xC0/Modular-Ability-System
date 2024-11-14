using System.Linq;
using Core.Services.CameraTransition.Configuration;
using Core.Services.ResourceProvider;
using DG.Tweening;
using Shared.Constants;
using Shared.Extensions;
using UnityEngine;

namespace Core.Services.CameraTransition
{
    public class CameraTransitionService : ICameraTransitionService
    {
        private Camera _mainCamera;
        private readonly CameraConfiguration _cameraConfiguration;

        private bool _onPlayer = true;
        private Transform _playerEye;

        public CameraTransitionService(IResourceProviderService resourceProvider)
        {
            _cameraConfiguration = resourceProvider.LoadResources<CameraConfiguration>().ToList()[0];
        }

        public void SetupCamera(Camera camera, Transform playerEye)
        {
            _mainCamera = camera;
            _playerEye = playerEye;
        }
        
        public Sequence To(Transform target, Camera camera)
        {
            var cameraToUse = camera == null ? _mainCamera : camera;
            var sequence = DOTween.Sequence(cameraToUse);
            _mainCamera.transform.SetParent(null);
            sequence.AppendCallback(() =>
            {
                _onPlayer = false;
                CursorExtensions.AllowCursor(true);
            });
            
            sequence
                .Append(cameraToUse.transform
                    .DOMove(target.position, _cameraConfiguration.MoveDuration)
                    .SetEase(_cameraConfiguration.PositionCurve));
            sequence
                .Join(cameraToUse.transform
                    .DORotateQuaternion(target.rotation, _cameraConfiguration.RotationDuration)
                    .SetEase(_cameraConfiguration.RotationCurve));

            return sequence;
        }

        public Sequence LookAt(Vector3 point, Camera camera)
        {
            var cameraToUse = camera == null ? _mainCamera : camera;
            var sequence = DOTween.Sequence(cameraToUse);

            sequence.AppendCallback(() =>
            {
                _onPlayer = false;
                CursorExtensions.AllowCursor(false);
            });

            sequence
                .Append(cameraToUse.transform
                    .DOLookAt(point + _cameraConfiguration.Offset, AnimationConstants.Quarter)
                    .SetEase(_cameraConfiguration.RotationCurve));
            
            return sequence;
        }
        
        public Sequence AddPositionNoise(Transform target, float duration, float strength)
        {
            var sequence = DOTween.Sequence(target);
            sequence.Join(target.DOShakePosition(duration, strength));
            return sequence;
        }

        public Sequence AddRotationNoise(Transform target, float duration, float strength)
        {
            var sequence = DOTween.Sequence(target);
            sequence.Join(target.DOShakeRotation(duration, strength, 50));
            return sequence;
        }

        public Sequence Out(Camera camera, Transform playerEye)
        {
            var cameraToUse = camera == null ? _mainCamera : camera;
            var sequence = DOTween.Sequence(cameraToUse);
            var playerEyeToUse = playerEye == null ? _playerEye : playerEye;

            _mainCamera.transform.SetParent(_playerEye);

            if (_onPlayer)
                return null;
            
            sequence.AppendCallback(() =>
            {
                _onPlayer = true;
                CursorExtensions.AllowCursor(false);
            });
            
            sequence
                .Append(cameraToUse.transform
                    .DOMove(playerEyeToUse.position, _cameraConfiguration.MoveDuration)
                    .SetEase(_cameraConfiguration.PositionCurve));
            sequence
                .Join(cameraToUse.transform
                    .DORotateQuaternion(playerEyeToUse.rotation, _cameraConfiguration.RotationDuration)
                    .SetEase(_cameraConfiguration.RotationCurve));
            
            return sequence;
        }

        public Sequence JumpTo(Transform target, Camera camera)
        {
            var cameraToUse = camera == null ? _mainCamera : camera;
            var sequence = DOTween.Sequence(cameraToUse);

            sequence
                .Append(cameraToUse.transform
                    .DOMove(target.position, _cameraConfiguration.MoveDuration)
                    .SetEase(_cameraConfiguration.PositionCurve));
            sequence
                .Join(cameraToUse.transform
                    .DORotateQuaternion(target.rotation, _cameraConfiguration.RotationDuration)
                    .SetEase(_cameraConfiguration.RotationCurve));

            return sequence;
        }
    }
}