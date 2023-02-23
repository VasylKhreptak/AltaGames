using CBA.Events.Core;
using UnityEngine;

namespace CBA.Events.General.Keyboard
{
    public class GetKeyEvent : MonoEvent
    {
        [Header("Preferences")]
        [SerializeField] private KeyCode _keyCode;

        #region MonoBehaviour

        private void Update()
        {
            if (Input.GetKey(_keyCode))
            {
                Invoke();
            }
        }

        #endregion
    }
}
