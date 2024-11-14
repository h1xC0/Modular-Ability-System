using UnityEditor;
using UnityEngine;

namespace Game.Level.Environment.Spawpoints
{
    [CustomEditor(typeof(SpawnPointArea))]
    public class SpawnPointAreaEditor : Editor
    {
        private SerializedProperty _radiusProperty;

        public void OnSceneGUI() 
        {
            var spawnArea = (SpawnPointArea)target;
            _radiusProperty = serializedObject.FindProperty("_radius");

            Handles.color = Color.red;
            Handles.DrawWireDisc(spawnArea.transform.position,
                spawnArea.transform.up,
                _radiusProperty.floatValue);
        }
    }
}