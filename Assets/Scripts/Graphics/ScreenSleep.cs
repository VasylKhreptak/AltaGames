using System;
using UnityEngine;

namespace Graphics
{
    public class ScreenSleep : MonoBehaviour
    {
        #region MonoBehaviour

        private void Start()
        {
            SetTimeout(SleepTimeout.NeverSleep);
        }

        #endregion

        private void SetTimeout(Int32 sleepTimeout)
        {
            Screen.sleepTimeout = sleepTimeout;
        }
    }
}
