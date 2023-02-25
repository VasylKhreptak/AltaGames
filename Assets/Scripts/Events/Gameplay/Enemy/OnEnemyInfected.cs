using CBA.Events.Core;
using UnityEngine;

namespace Events.Gameplay.Enemy
{
    public class OnEnemyInfected : MonoEvent
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
            _enemy.onInfected += Invoke;
        }

        private void OnDisable()
        {
            _enemy.onInfected -= Invoke;
        }

        #endregion
    }
}
