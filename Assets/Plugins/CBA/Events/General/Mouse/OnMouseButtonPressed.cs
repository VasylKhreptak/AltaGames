using CBA.Events.Core;
using UnityEngine;

namespace CBA.Events.General.Mouse
{
    public class OnMouseButtonPressed : MonoEvent
    {
        [Header("Preferences")]
        [SerializeField] private int _mouseButtonID;

        #region MonoBehaviour

        private void Update()
        {
            if (Input.GetMouseButton(_mouseButtonID))
            {
                Invoke();
            }
        }

        #endregion
    }
}
