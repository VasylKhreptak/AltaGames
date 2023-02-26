using UnityEngine;

namespace Graphics
{
    public class GameFramerate : MonoBehaviour
    {
        [Header("Target Framerate")]
        [SerializeField] private int _targetFramerate = 60;

        #region MonoBehaviour

        private void OnValidate()
        {
            Set(_targetFramerate);
        }

        private void Start()
        {
            Set(_targetFramerate);
        }

        #endregion

        private void Set(int framerate)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = framerate;
        }
    }
}
