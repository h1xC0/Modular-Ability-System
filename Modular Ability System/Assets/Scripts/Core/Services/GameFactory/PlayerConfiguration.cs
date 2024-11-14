using Core.Services.ResourceProvider;
using UnityEngine;

namespace Core.Services.GameFactory
{
    [CreateAssetMenu(fileName = "Player Configuration", menuName = "Configurations/Player/Create Player Configuration")]
    public class PlayerConfiguration : ScriptableObject, IResource
    {
        
        [SerializeField] private float _rayDistance = 10f;
        public float RayDistance => _rayDistance;
        
        [SerializeField] private float _runSpeed;
        public float RunSpeed => _runSpeed;

        [SerializeField] private float _runAcceleration;
        public float RunAcceleration => _runAcceleration;
    }
}