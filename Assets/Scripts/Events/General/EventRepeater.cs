using CBA.EventListeners;

namespace Events.General
{
    public class EventRepeater : MonoEventListener
    {
        protected override void OnEventFired()
        {
            onMonoCall?.Invoke();
        }
    }
}
