using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Proxies
{
    public class CallbackProxy : MonoBehaviour
    {
        private Dictionary<string, Action<string>> _typedCallbacksByKey = new();
        private Dictionary<string, Action> _callbacksByKey = new();

        public void Add(string key, Action<string> typedCallback, Action callback)
        {
            _typedCallbacksByKey[key] = typedCallback;
            _callbacksByKey[key] = callback;
        }

        public void RaiseCallbacks(string key, string value)
        {
            _typedCallbacksByKey[key].Invoke(value);
            _callbacksByKey[key].Invoke();
        }
    }
}