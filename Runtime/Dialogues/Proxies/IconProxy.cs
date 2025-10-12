using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Proxies
{
    public class IconProxy : MonoBehaviour
    {
        [Serializable]
        private class IconData
        {
            [field: SerializeField] public Sprite Icon { get; set; }
            [field: SerializeField] public string Key { get; set; }
        }

        [SerializeField] private IconData[] iconsData;
        
        private Dictionary<string, Sprite> _iconsByKey = new();

        private void Awake()
        {
            _iconsByKey = iconsData.ToDictionary(data => data.Key, data => data.Icon);
        }

        public Sprite GetIcon(string key) => _iconsByKey[key];
    }
}