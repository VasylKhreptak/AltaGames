using CBA.Events.Core;
using UnityEngine.EventSystems;

namespace Events.UI
{
    public class OnPointerDownEvent : MonoEvent, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            onMonoCall?.Invoke();
        }
    }
}
