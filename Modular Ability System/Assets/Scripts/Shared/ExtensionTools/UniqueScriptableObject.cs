using Core.Services.ResourceProvider;
using Shared.ExtensionTools.Attributes.ReadOnly;
using UnityEngine;

namespace Shared.ExtensionTools
{
    public abstract class UniqueScriptableObject : ScriptableObject, IResource
    {
        [ReadOnly] public int Identifier;
    }
}