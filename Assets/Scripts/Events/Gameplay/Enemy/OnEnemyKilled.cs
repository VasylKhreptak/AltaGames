using CBA.Events.Core;
using UnityEngine;

namespace Events.Gameplay.Enemy
{
    public class OnEnemyKilled : MonoEvent
    {
        [Header("References")]
        [SerializeField] private GamePlay.Enemy.Enemy _enemy;

        #region MonoBehaviour

        private void OnValidate()
        {
            _enemy ??= GetComponent<GamePlay.Enemy.Enemy>();
        }

        private void Awake()
        {
            _enemy.onKilled += Invoke;
        }

        private void OnDestroy()
        {
            _enemy.onKilled -= Invoke;
        }

        #endregion
    }
}
