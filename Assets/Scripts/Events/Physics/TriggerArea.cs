using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events.Physics
{
    public class TriggerArea : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;
        [SerializeField] private OnTriggerExitEvent _triggerExitEvent;

        protected List<Transform> _affectedObjects = new List<Transform>();
        
        public Action onFilled;
        public Action onEmptied;

        public bool IsEmpty => _affectedObjects.Count == 0;

        #region MonoBehaviour

        private void OnValidate()
        {
            _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
            _triggerExitEvent ??= GetComponent<OnTriggerExitEvent>();
        }

        private void OnEnable()
        {
            _triggerEnterEvent.onEnter += OnEnter;
            _triggerExitEvent.onExit += OnExit;
        }

        private void OnDisable()
        {
            _triggerEnterEvent.onEnter -= OnEnter;
            _triggerExitEvent.onExit -= OnExit;
        }

        #endregion

        private void OnEnter(Collider collider)
        {
            _affectedObjects.Add(collider.transform);

            if (_affectedObjects.Count == 1)
            {
                onFilled?.Invoke();
            }
        }

        private void OnExit(Collider collider)
        {
            _affectedObjects.Remove(collider.transform);

            if (_affectedObjects.Count == 0)
            {
                onEmptied?.Invoke();
            }
        }

        public void Clear()
        {
            _affectedObjects.Clear();
        }
    }
}
