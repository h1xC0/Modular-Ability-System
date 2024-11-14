using UnityEngine;

namespace Game.Level.Environment.Spawpoints
{
    public class SpawnPointArea : SpawnPoint
    {
        [SerializeField] private float _radius;
        public override void SetSpawn(GameObject spawnable)
        {
            base.SetSpawn(spawnable);

            var additivePosition = Random.insideUnitSphere;
            additivePosition.y = 0;
            additivePosition.Normalize();

            additivePosition *= _radius;
            spawnable.transform.position += additivePosition;
        }

        public override Vector3 GetSpawnPoint()
        {
            var additivePosition = Random.insideUnitSphere;
            additivePosition.y = 0;
            additivePosition.Normalize();

            additivePosition *= _radius;
            return additivePosition;
        }
    }
}