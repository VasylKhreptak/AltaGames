using CBA.Events.Core;
using UnityEngine;

namespace Events.Physics
{
    public class OnTriggerAreaFilled : MonoEvent
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
            _triggerArea.onFilled += Invoke;
        }

        private void OnDestroy()
        {
            _triggerArea.onFilled -= Invoke;
        }

        #endregion
    }
}
