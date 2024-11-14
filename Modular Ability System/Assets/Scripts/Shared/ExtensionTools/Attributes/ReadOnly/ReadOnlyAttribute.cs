using System;
using Shared.Enums;
using UnityEngine;

namespace Shared.ExtensionTools.Attributes.ReadOnly
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public GUIColor GUIColor;
        public ReadOnlyAttribute(GUIColor guiColor = GUIColor.Standard)
        {
            GUIColor = guiColor;
        }
    }
}