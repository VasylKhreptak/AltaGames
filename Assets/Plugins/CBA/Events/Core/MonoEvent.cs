namespace CBA.Events.Core
{
    public class MonoEvent : UnityEngine.MonoBehaviour
    {
        public System.Action onMonoCall;

        protected virtual void Invoke() => onMonoCall?.Invoke();
    }
}
