using UnityEngine;

namespace Shared.Extensions
{
    public class CursorExtensions
    {
        public static void AllowCursor(bool flag)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Cursor.visible = flag;
            Cursor.lockState = flag ? CursorLockMode.None : CursorLockMode.Locked;
#endif
        }
    }
}