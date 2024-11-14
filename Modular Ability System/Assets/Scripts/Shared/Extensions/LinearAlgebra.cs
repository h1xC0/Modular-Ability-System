using UnityEngine;

namespace Shared.Extensions
{
    public struct TRSMatrix
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        public TRSMatrix(Vector3 position, Vector3 rotation = default, Vector3 scale = default)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }


    public static class AlgebraExt
    {
        public static Vector3 Interpolate(this Vector3 current, Vector3 end, float t)
        {
            return new Vector3(current.x.Interpolate(end.x, t), current.y.Interpolate(end.y, t),
                current.z.Interpolate(end.z, t));
        }

        public static float Interpolate(this float current, float end, float t)
        {
            return current + (end - current) * t;
        }
    }
}