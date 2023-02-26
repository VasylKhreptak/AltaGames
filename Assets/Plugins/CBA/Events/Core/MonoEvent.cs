namespace CBA.Events.Core
{
    public class MonoEvent : UnityEngine.MonoBehaviour
    {
        public System.Action onMonoCall;

        public void Invoke() => onMonoCall?.Invoke();
    }
}
