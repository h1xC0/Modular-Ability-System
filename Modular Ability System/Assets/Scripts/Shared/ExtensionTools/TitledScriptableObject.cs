using Shared.Enums;
using Shared.ExtensionTools.Attributes.ReadOnly;
using UnityEngine;

namespace Shared.ExtensionTools
{
    public abstract class TitledScriptableObject : UniqueScriptableObject
    {
        public string Title => _title;
        [ReadOnly(GUIColor.Yellow)] [SerializeField] protected string _title;

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(Title) || Title != name)
            {
                _title = name;
            }
        }
    }
}