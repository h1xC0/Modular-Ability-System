using Core.Services.ResourceProvider;
using UnityEngine;

namespace Core.Services.CameraTransition.Configuration
{
    [CreateAssetMenu(fileName = "Camera Settings Profile" , menuName = "Configurations/Camera Settings Profile")]
    public class CameraConfiguration : ScriptableObject, IResource
    {
        [Header("Transition Ease")] 
        [SerializeField]
        private AnimationCurve _positionCurve;
        public AnimationCurve PositionCurve => _positionCurve;
        
        [SerializeField]
        private AnimationCurve _rotationCurve;
        public AnimationCurve RotationCurve => _rotationCurve;

        [Header("Transition Properties")]
        [SerializeField] 
        private float moveDuration;
        public float MoveDuration => moveDuration;

        [SerializeField] 
        private float rotationDuration;
        public float RotationDuration => rotationDuration;
        
        [SerializeField] 
        private Vector3 _offset;
        public Vector3 Offset => _offset;
    }
}