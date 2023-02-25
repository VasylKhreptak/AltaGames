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

        private void OnEnable()
        {
            _enemy.onKilled += Invoke;
        }

        private void OnDisable()
        {
            _enemy.onKilled -= Invoke;
        }

        #endregion
    }
}
