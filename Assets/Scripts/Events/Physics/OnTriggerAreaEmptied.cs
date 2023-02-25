using CBA.Events.Core;
using UnityEngine;

namespace Events.Physics
{
    public class OnTriggerAreaEmptied : MonoEvent
    {
        [Header("References")]
        [SerializeField] private TriggerArea _triggerArea;

        #region MonoBehaviour

        private void OnValidate()
        {
            _triggerArea ??= GetComponent<TriggerArea>();
        }

        private void Awake()
        {
            _triggerArea.onEmptied += Invoke;
        }

        private void OnDestroy()
        {
            _triggerArea.onEmptied -= Invoke;
        }

        #endregion
    }
}
