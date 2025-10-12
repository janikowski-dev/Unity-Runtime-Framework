using UnityEngine;

namespace Internal.Runtime.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start() => DontDestroyOnLoad(gameObject);
    }
}