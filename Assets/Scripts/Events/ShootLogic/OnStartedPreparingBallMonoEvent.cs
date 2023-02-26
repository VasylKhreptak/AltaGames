using CBA.Events.Core;
using GamePlay.ShootLogic;
using UnityEngine;

namespace Events.ShootLogic
{
    public class OnStartedPreparingBallMonoEvent : MonoEvent
    {
        [Header("References")]
        [SerializeField] private BallPreparer _ballPreparer;

        #region MonoBehaviour

        private void OnValidate()
        {
            _ballPreparer ??= FindObjectOfType<BallPreparer>();
        }

        private void OnEnable()
        {
            _ballPreparer.onStartedPreparing += Invoke;
        }

        private void OnDisable()
        {
            _ballPreparer.onStartedPreparing -= Invoke;
        }

        #endregion

        private void Invoke(GameObject ball) => Invoke();
    }
}
