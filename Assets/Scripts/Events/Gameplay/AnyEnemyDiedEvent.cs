using System.Collections.Generic;
using CBA.Events.Core;
using GamePlay.Entity.Interfaces;
using ObjectPooler;
using UnityEngine;
using Zenject;

namespace Events.Gameplay
{
    public class AnyEnemyDiedEvent : MonoEvent
    {
        [Header("Preferences")]
        [SerializeField] private Pools[] _enemyPools;

        private ObjectPooler.ObjectPooler _objectPooler;

        private List<IKillable> _killables;

        [Inject]
        private void Construct(ObjectPooler.ObjectPooler objectPooler)
        {
            _objectPooler = objectPooler;
        }

        #region MonoBehaviour

        private void Start()
        {
            _killables = GetKillables();

            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        #endregion

        private List<IKillable> GetKillables()
        {
            List<IKillable> killables = new List<IKillable>(_objectPooler.GetPoolsSize(_enemyPools));

            foreach (var enemyPool in _enemyPools)
            {
                Transform enemyParent = _objectPooler.GetPoolParent(enemyPool);

                foreach (Transform enemy in enemyParent)
                {
                    if (enemy.TryGetComponent(out IKillable killable))
                    {
                        killables.Add(killable);
                    }
                }
            }

            return killables;
        }

        private void AddListeners()
        {
            foreach (var killable in _killables)
            {
                killable.onKilled += Invoke;
            }
        }

        private void RemoveListeners()
        {
            foreach (var killable in _killables)
            {
                killable.onKilled -= Invoke;
            }
        }
    }
}
