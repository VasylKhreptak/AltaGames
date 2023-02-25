using System;
using CBA.Events.Core;
using UnityEngine;

namespace Events.Physics
{
    public class OnTriggerEnterMonoEvent : MonoEvent
    {
        [Header("References")]
        [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;

        #region MonoBehaviour

        private void OnValidate()
        {
            _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
        }

        private void OnEnable()
        {
            _triggerEnterEvent.onEnter += Invoke;
        }

        private void OnDisable()
        {
            _triggerEnterEvent.onEnter -= Invoke;
        }

        #endregion

        private void Invoke(Collider collider) => Invoke();
    }
}
