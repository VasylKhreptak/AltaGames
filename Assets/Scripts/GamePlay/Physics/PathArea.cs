using System.Collections.Generic;
using Events.Gameplay.Enemy;
using Events.Physics;
using UnityEngine;

namespace GamePlay.Physics
{
    public class PathArea : TriggerArea
    {
        [Header("Enemy Events")]
        [SerializeField] private AnyEnemyDiedEvent _anyEnemyDiedEvent;

        #region MonoBehaviour

        private void OnValidate()
        {
            _anyEnemyDiedEvent ??= FindObjectOfType<AnyEnemyDiedEvent>();
        }

        private void Awake()
        {
            _anyEnemyDiedEvent.onMonoCall += RemoveInactiveTargets;
        }

        private void OnDestroy()
        {
            _anyEnemyDiedEvent.onMonoCall -= RemoveInactiveTargets;
        }

        #endregion

        private void RemoveInactiveTargets()
        {
            foreach (var affectedObject in new List<Transform>(_affectedObjects))
            {
                if (affectedObject.gameObject.activeSelf == false)
                {
                    _affectedObjects.Remove(affectedObject);

                    if (IsEmpty)
                    {
                        onEmptied?.Invoke();
                    }
                }
            }
        }
    }
}
