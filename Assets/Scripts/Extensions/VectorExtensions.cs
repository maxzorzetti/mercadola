using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        const int DefaultPixelsPerUnit = 32;

        public static Vector2 Pixelized(this Vector2 vector, int pixelsPerUnit = DefaultPixelsPerUnit)
        {
            return new Vector2(
                Mathf.Round(vector.x * pixelsPerUnit) / pixelsPerUnit,
                Mathf.Round(vector.y * pixelsPerUnit) / pixelsPerUnit
            );
        }
    }
}