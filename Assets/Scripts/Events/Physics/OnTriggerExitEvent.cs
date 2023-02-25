using System;
using UnityEngine;

namespace Events.Physics
{
    public class OnTriggerExitEvent : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private LayerMask _layerMask;

        public event Action<Collider> onExit;

        #region MonoBehaviour

        private void OnTriggerExit(Collider other)
        {
            if (Extensions.LayerMask.ContainsLayer(_layerMask, other.gameObject.layer))
            {
                onExit?.Invoke(other);
            }
        }

        #endregion
    }
}
