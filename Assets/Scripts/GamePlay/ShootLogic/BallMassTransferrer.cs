using System;
using DG.Tweening;
using UnityEngine;

namespace GamePlay.ShootLogic
{
    public class BallMassTransferrer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private BallPreparer _ballPreparer;

        [Header("Preferences")]
        [SerializeField] private float _transferDuration;
        [SerializeField] private AnimationCurve _transferCurve;
        [SerializeField] private float _minPlayerScaleX;

        private Tween _playerScaleTween, _ballScaleTween;

        public event Action onTransferedAllMass;

        #region MonoBehaviour

        private void OnValidate()
        {
            _ballPreparer ??= FindObjectOfType<BallPreparer>();
        }

        private void Awake()
        {
            _ballPreparer.onStartedPreparing += StartTransfering;
            _ballPreparer.onPrepared += StopTransfering;
        }

        private void OnDestroy()
        {
            KillTweens();

            _ballPreparer.onStartedPreparing -= StartTransfering;
            _ballPreparer.onPrepared -= StopTransfering;
        }

        #endregion

        private void StartTransfering(GameObject ball)
        {
            KillTweens();

            ball.transform.localScale = Vector3.zero;
            _playerScaleTween = _playerTransform.DOScale(Vector3.zero, _transferDuration).SetEase(_transferCurve).Play();
            _ballScaleTween = ball.transform.DOScale(_playerTransform.localScale, _transferDuration).SetEase(_transferCurve).Play();

            _playerScaleTween.OnUpdate(() =>
            {
                if (_playerTransform.localScale.x < _minPlayerScaleX)
                {
                    onTransferedAllMass?.Invoke();
                    KillTweens();
                }
            });
        }

        private void StopTransfering(GameObject ball) => StopTransfering();

        private void StopTransfering()
        {
            KillTweens();
        }

        private void KillTweens()
        {
            _playerScaleTween.Kill();
            _ballScaleTween.Kill();
        }
    }
}
