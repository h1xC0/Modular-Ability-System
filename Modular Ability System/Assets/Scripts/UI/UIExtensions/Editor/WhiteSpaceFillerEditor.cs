using UnityEditor;
using UnityEngine;

namespace UI.UIExtensions.Editor
{
    [CustomEditor(typeof(WhiteSpaceFiller))]
    public class WhiteSpaceFillerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var filler = (WhiteSpaceFiller)target;

            if (GUILayout.Button("Fill White Space"))
            {
                filler.FillSpace(true);
            }
        }
    }
}