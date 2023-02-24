using CBA.Events.Core;
using UnityEngine.EventSystems;

namespace Events.UI
{
    public class OnPointerUpEvent : MonoEvent, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            onMonoCall?.Invoke();
        }
    }
}
