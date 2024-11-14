using UnityEngine;

namespace Game.Level.Environment.Spawpoints
{
    public class SpawnPoint : MonoBehaviour
    {
        public virtual void SetSpawn(GameObject spawnable) 
        {
            spawnable.transform.SetParent(transform);
            spawnable.transform.localPosition = Vector3.zero;
            spawnable.transform.localRotation = spawnable.transform.localRotation;
            spawnable.transform.localScale = Vector3.one;
        }
        
        public virtual Vector3 GetSpawnPoint()
        {
            return transform.position;
        }
    }
}