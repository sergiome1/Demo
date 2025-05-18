using System.Collections;

namespace Assets.Scripts.GameProcessors
{
    public abstract class GameProcessor
    {
        public virtual void Process(string data) { }
        public virtual IEnumerator ProcessAsync(string data) { yield break; }
        public virtual void Process(object data) { }
        public virtual IEnumerator ProcessAsync(object data) { yield break; }
    }
}
