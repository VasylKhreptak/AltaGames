using CBA.Events.Core;
using GamePlay.ShootLogic;
using UnityEngine;

namespace Events.ShootLogic
{
    public class OnPreparedBallMonoEvent : MonoEvent
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
            _ballPreparer.onPrepared += Invoke;
        }

        private void OnDisable()
        {
            _ballPreparer.onPrepared -= Invoke;
        }

        #endregion

        private void Invoke(GameObject ball) => Invoke();
    }
}
