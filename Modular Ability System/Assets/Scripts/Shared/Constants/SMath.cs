using System;
using UnityEngine;

namespace Shared.Constants
{
    public static class SMath
    {
        public static float Normalize(this float x, float min, float max)
        {
            return (x - min) / (max - min);
        }

        public static bool CompareXZ(this Vector3 current, Vector3 comparable)
        {
            return Math.Abs(current.x - comparable.x) < 0.01f && Math.Abs(current.z - comparable.z) < 0.01f;
        }

        public static bool TrySnap(this ref float value, float snapValue, float snapTolerance)
        {
            var multiplier = Mathf.CeilToInt(value / snapValue);
            if (Mathf.Abs(value - snapValue * multiplier) <= snapTolerance)
            {
                Debug.Log($"Value: {value}, SnapValue: {snapValue}, Multiplier: {multiplier}, Tolerance: {snapTolerance}");
            
                value = snapValue * multiplier;
                Debug.Log("<color=red>Snap</color>");
                return true;
            }

            return false;
        }
    }
}