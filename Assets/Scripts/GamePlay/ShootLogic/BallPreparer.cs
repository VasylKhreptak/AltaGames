using System;
using CBA.Events.Core;
using DG.Tweening;
using ObjectPooler;
using UnityEngine;
using Zenject;

namespace GamePlay.ShootLogic
{
    public class BallPreparer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _playerTransform;

        [Header("Move Preferences")]
        [SerializeField] private Transform _moveFrom;
        [SerializeField] private Transform _moveTo;
        [SerializeField] private AnimationCurve _moveCurve;
        [SerializeField] private float _moveDuration;

        [Header("Events")]
        [SerializeField] private MonoEvent _pointerDownEvent;
        [SerializeField] private MonoEvent _pointerUpEvent;

        [Header("Spawn Preferences")]
        [SerializeField] private Pools _ballPool;

        private bool _isPreparing;

        private ObjectPooler.ObjectPooler _objectPooler;

        private GameObject _currentBall;

        private Tween _moveTween;
        private Tween _waitTween;

        public event Action<GameObject> onStartedPreparing;
        public event Action<GameObject> onPrepared;

        [Inject]
        private void Construct(ObjectPooler.ObjectPooler objectPooler)
        {
            _objectPooler = objectPooler;
        }

        #region MonoBehaviour

        private void Start()
        {
            _pointerDownEvent.onMonoCall += TryStartPreparing;
            _pointerUpEvent.onMonoCall += TryStopPreparing;
        }

        private void OnDestroy()
        {
            _moveTween.Kill();

            _pointerDownEvent.onMonoCall -= TryStartPreparing;
            _pointerUpEvent.onMonoCall -= TryStopPreparing;
        }

        #endregion

        private void TryStartPreparing()
        {
            if (CanStartPreparing())
            {
                StartPreparing();
            }
        }

        private void TryStopPreparing()
        {
            if (CanStopPreparing())
            {
                StopPreparing();
            }
        }

        private bool CanStartPreparing()
        {
            return _isPreparing == false;
        }

        private bool CanStopPreparing()
        {
            return _isPreparing;
        }

        private void StartPreparing()
        {
            _currentBall = _objectPooler.Spawn(_ballPool, _moveFrom.position, _playerTransform.rotation);

            _moveTween.Kill();
            _moveTween = _currentBall.transform.DOMove(_moveTo.position, _moveDuration).SetEase(_moveCurve).Play();

            _isPreparing = true;
            onStartedPreparing?.Invoke(_currentBall);
        }

        private void StopPreparing()
        {
            _moveTween.Kill();

            _isPreparing = false;
            onPrepared?.Invoke(_currentBall);
            _currentBall = null;
        }
    }
}
