using UnityEngine;

namespace Internal.Runtime.Utilities
{
    public static class ValueExtensions
    {
        public static void OverrideLayer(this GameObject gameObject, int layer) => gameObject.layer = layer;

        public static void Disable(this SpriteRenderer renderer) => renderer.color = Color.clear;
        
        public static Vector3 JustNormalize(this Vector3 vector) => vector.normalized;

        public static void Face(this Transform transform, Vector3 forward) => transform.forward = forward;
    }
}