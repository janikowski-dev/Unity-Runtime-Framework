using UnityEngine;

namespace Internal.Runtime.Utilities
{
    public static class MethodExtensions
    {
        public static void DestroySelf(this GameObject gameObject, float delay) => Object.Destroy(gameObject, delay);
        
        public static void DestroySelf(this GameObject gameObject) => Object.Destroy(gameObject);
        
        public static void DestroySelf(this Component component) => Object.Destroy(component);
        
        public static float AbsoluteValue(this float value) => Mathf.Abs(value);
        
        public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);
        
        public static int Clamp(this int value, int min, int max) => Mathf.Clamp(value, min, max);

        public static Vector3 ProjectOnPlane(this Vector3 vector, Vector3 planeNormal) => Vector3.ProjectOnPlane(vector, planeNormal);

        public static Quaternion ToQuaternion(this Vector3 euler) => Quaternion.Euler(euler);
        
        public static Vector3 Slerp(this Vector3 vector, Vector3 target, float delta) => Vector3.Slerp(vector, target, delta);
        
        public static float Lerp(this float value, float target, float delta) => Mathf.Lerp(value, target, delta);

        public static float Dot(this Vector3 from, Vector3 to) => Vector3.Dot(from, to);
    }
}