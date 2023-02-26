using CBA.Events.Core;
using Extensions;
using NaughtyAttributes;
using ObjectPooler;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GamePlay.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EnemyPositionProvider _positionProvider;

        [Header("Events")]
        [SerializeField] private MonoEvent _respawnEvent;

        [Header("Preferences")]
        [SerializeField, MinMaxSlider(5, 40)] private Vector2Int _enemyCountSlider;
        [SerializeField] private Pools[] _enemies;

        private int EnemyCount => Random.Range(_enemyCountSlider.x, _enemyCountSlider.y);

        private ObjectPooler.ObjectPooler _objectPooler;

        [Inject]
        private void Construct(ObjectPooler.ObjectPooler objectPooler)
        {
            _objectPooler = objectPooler;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            _positionProvider ??= FindObjectOfType<EnemyPositionProvider>();
            _respawnEvent ??= GetComponent<MonoEvent>();

            if (_positionProvider != null && _enemyCountSlider.y > _positionProvider.PositionsCount)
            {
                _enemyCountSlider.y = _positionProvider.PositionsCount;
                Debug.Log("You do not have enough spawn positions!");
            }
        }

        private void Awake()
        {
            _respawnEvent.onMonoCall += RespawnEnemies;
        }

        private void OnDestroy()
        {
            _respawnEvent.onMonoCall -= RespawnEnemies;
        }

        #endregion

        private void RespawnEnemies()
        {
            ClearEnemies();
            
            SpawnEnemies();
        }

        private void ClearEnemies()
        {
            foreach (var enemy in _enemies)
            {
                _objectPooler.DisablePool(enemy);
            }
        }
        
        private void SpawnEnemies()
        {
            int enemyCount = EnemyCount;
            
            int i = 0;
            foreach (var position in _positionProvider.Generator())
            {
                SpawnEnemy(position);
                
                if (++i == enemyCount) break;
            }
        }

        private void SpawnEnemy(Vector3 position)
        {
            _objectPooler.Spawn(_enemies.Random(), position, GetRandomRotationAroundY());
        }
        
        private Quaternion GetRandomRotationAroundY()
        {
            float angle = Random.Range(0f, 360f);
            return Quaternion.Euler(0f, angle, 0f);
        }

    }
}
