using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Proxies
{
    public class AwaitProxy : MonoBehaviour
    {
        private Dictionary<string, Func<bool>> _finishCallbacksByKey = new();

        public void Add(string key, Func<bool> finishCallback)
        {
            _finishCallbacksByKey[key] = finishCallback;
        }

        public Func<bool> GetFinishCallback(string key) => _finishCallbacksByKey[key];
    }
}