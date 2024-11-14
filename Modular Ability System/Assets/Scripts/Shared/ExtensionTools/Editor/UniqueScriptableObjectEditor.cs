using Core.Services.ResourceProvider;
using UnityEditor;
using UnityEngine;

namespace Shared.ExtensionTools.Editor
{
    [CustomEditor(typeof(UniqueScriptableObject), true)]
    public class UniqueScriptableObjectEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var obj = (UniqueScriptableObject)target;
            if (Application.isPlaying == false)
            {
                SetID(obj);
            }
            if (obj.Identifier.Equals(obj.GetHashCode()))
                return;

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Space(180);

            if (GUILayout.Button("Generate ID", GUILayout.Height(25), GUILayout.Width(200)))
            {
                GenerateID(obj);
            }
            GUILayout.EndHorizontal();
        }

        private void SetID(UniqueScriptableObject obj)
        {
            var resourceProvider = new ResourceProviderService();

            if (obj.Identifier == 0)
            {
                GenerateID(obj);
            }
            // else
            // {
            //     var objectsWithId = resourceProvider.LoadResources<UniqueScriptableObject>(true).ToArray();
            //     if (objectsWithId.Any(x => x != obj && x.Identifier == obj.Identifier))
            //     {
            //         GenerateID(obj);
            //     }
            // }
        }

        private void GenerateID(UniqueScriptableObject obj)
        {
            obj.Identifier = obj.GetHashCode();
            EditorUtility.SetDirty(obj);
        }
    }
}