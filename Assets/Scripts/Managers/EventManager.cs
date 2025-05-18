using UnityEngine;

namespace Assets.Scripts.Managers
{
    public abstract class EventManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RemoveEvents();
            AddEvents();
        }

        protected virtual void OnDestroy()
        {
            RemoveEvents();
        }

        protected abstract void AddEvents();
        protected abstract void RemoveEvents();
    }
}
