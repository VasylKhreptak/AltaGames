using System;
using CBA.Events.Core;
using UnityEngine;

namespace Events.Physics
{
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private LayerMask _layerMask;

        public event Action<Collider> onEnter;

        #region MonoBehaviour

        private void OnTriggerEnter(Collider other)
        {
            if (Extensions.LayerMask.ContainsLayer(_layerMask, other.gameObject.layer))
            {
                onEnter?.Invoke(other);
            }
        }

        #endregion
    }
}
