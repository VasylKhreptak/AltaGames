using GamePlay.Enemy;
using GamePlay.Entity;
using UnityEngine;
using UnityEngine.Serialization;
using Action = CBA.Actions.Core.Action;

namespace Actions.Entity
{
    public class KillEnemy : Action
    {
        [Header("References")]
        [SerializeField] private Enemy _enemy;

        #region MonoBehaviour

        private void OnValidate()
        {
            _enemy ??= GetComponent<Enemy>();
        }

        #endregion

        public override void Do()
        {
            _enemy.Kill();
        }
    }
}
