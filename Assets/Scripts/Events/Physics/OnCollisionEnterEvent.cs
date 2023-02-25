using System;
using UnityEngine;

namespace Events.Physics
{
    public class OnCollisionEnterEvent : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LayerMask _layerMask;

        public event Action<Collision> onEnter;

        #region

        private void OnCollisionEnter(Collision collision)
        {
            if (Extensions.LayerMask.ContainsLayer(_layerMask, collision.gameObject.layer))
            {
                onEnter?.Invoke(collision);
            }
        }

        #endregion
    }
}
