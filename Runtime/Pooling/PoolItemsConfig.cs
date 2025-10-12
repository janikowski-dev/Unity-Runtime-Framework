using UnityEngine;

namespace Internal.Runtime.Pooling
{
    [CreateAssetMenu(menuName = "just Adam/Pooled Items")]
    public class PoolItemsConfig : ScriptableObject
    {
        [field: SerializeField] public PoolItem[] Prefabs { get; set; }
    }
}