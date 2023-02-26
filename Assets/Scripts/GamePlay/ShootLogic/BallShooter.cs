using UnityEngine;

namespace GamePlay.ShootLogic
{
    public class BallShooter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BallPreparer _ballPreparer;

        [Header("Shoot Preferences")]
        [SerializeField] private float _speed;

        #region MonoBehaviour

        private void OnValidate()
        {
            _ballPreparer ??= FindObjectOfType<BallPreparer>();
        }

        private void Awake()
        {
            _ballPreparer.onPrepared += Shoot;
        }

        private void OnDestroy()
        {
            _ballPreparer.onPrepared -= Shoot;
        }

        #endregion

        private void Shoot(GameObject ball)
        {
            if (ball.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity = _speed * rigidbody.transform.forward;
            }
        }
    }
}
