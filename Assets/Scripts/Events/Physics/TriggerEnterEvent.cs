using System;
using CBA.Events.Core;
using UnityEngine;

namespace Events.Physics
{
    public class TriggerEnterEvent : MonoEvent
    {
        [Header("Preferences")]
        [SerializeField] private LayerMask _layerMask;

        #region MonoBehaviour

        private void OnTriggerEnter(Collider other)
        {
            if (Extensions.LayerMask.ContainsLayer(_layerMask, other.gameObject.layer))
            {
                onMonoCall?.Invoke();
            }
        }

        #endregion
    }
}
