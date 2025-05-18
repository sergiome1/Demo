using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Coroutines
{
    public class CoroutineManager : MonoBehaviour
    {
        private Dictionary<Guid, Coroutine> coroutines = new Dictionary<Guid, Coroutine>();

        private static CoroutineManager _instance;

        public static CoroutineManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void RunCoroutine(IEnumerator enumerator)
        {
            Guid guid = Guid.NewGuid();

            while (coroutines.ContainsKey(guid))
            {
                guid = Guid.NewGuid();
            }

            coroutines.Add(guid, StartCoroutine(CoroutineInternal(enumerator, () =>
            {
                coroutines[guid] = null;
                coroutines.Remove(guid);
            })));
        }

        private IEnumerator CoroutineInternal(IEnumerator enumerator, Action callback)
        {
            yield return enumerator;

            callback?.Invoke();
        }
    }
}
