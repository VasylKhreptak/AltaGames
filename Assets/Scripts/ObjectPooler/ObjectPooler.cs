using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

namespace ObjectPooler
{
    public class ObjectPooler : MonoBehaviour
    {
        [Header("Pool")]
        private Dictionary<Pools, Queue<GameObject>> _poolDictionary;
        [SerializeField] private List<Pool> _pools;

        private DiContainer _diContainer;

        public event Action onInit;

        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        #region MonoBehaviour

        private void Awake()
        {
            Init();
        }

        #endregion

        private void Init()
        {
            CreatePoolFolders();

            FillPool();

            onInit?.Invoke();
        }

        private void CreatePoolFolders()
        {
            foreach (var pool in _pools)
            {
                pool.folder = new GameObject(pool.poolType.ToString());
                pool.folder.transform.parent = gameObject.transform;
            }
        }

        private void FillPool()
        {
            _poolDictionary = new Dictionary<Pools, Queue<GameObject>>();

            for (var i = 0; i < _pools.Count; i++)
            {
                var objectPool = new Queue<GameObject>();

                for (var j = 0; j < _pools[i].size; j++)
                {
                    GameObject obj = _diContainer.InstantiatePrefab(_pools[i].prefab);
                    obj.SetActive(false);

                    obj.transform.SetParent(_pools[i].folder.transform);

                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(_pools[i].poolType, objectPool);
            }
        }

        public GameObject Spawn(Pools pool, Vector3 position, Quaternion rotation)
        {
            if (_poolDictionary.ContainsKey(pool) == false)
            {
                Debug.LogWarning("Pool with name " + pool + "doesn't exist");
                return null;
            }

            GameObject objectFromPool = _poolDictionary[pool].Dequeue();

            if (objectFromPool.activeSelf)
            {
                objectFromPool.SetActive(false);
            }

            objectFromPool.transform.position = position;
            objectFromPool.transform.rotation = rotation;

            objectFromPool.SetActive(true);

            _poolDictionary[pool].Enqueue(objectFromPool);

            return objectFromPool;
        }

        public bool TrySpawnInactive(out GameObject poolObject, Pools pool, Vector3 position, Quaternion rotation)
        {
            if (_poolDictionary.ContainsKey(pool) == false)
            {
                Debug.LogWarning("Pool with name " + pool + "doesn't exist");
            }

            GameObject objectFromPool = _poolDictionary[pool].Dequeue();

            if (objectFromPool.activeSelf == false)
            {
                objectFromPool.transform.position = position;
                objectFromPool.transform.rotation = rotation;

                objectFromPool.SetActive(true);

                _poolDictionary[pool].Enqueue(objectFromPool);

                poolObject = objectFromPool;

                return true;
            }

            _poolDictionary[pool].Enqueue(objectFromPool);

            poolObject = null;

            return false;
        }

        public void DisablePool(Pools pool)
        {
            GameObject poolItem = _poolDictionary[pool].Dequeue();
            _poolDictionary[pool].Enqueue(poolItem);

            Transform poolTransform = poolItem.transform.parent;

            foreach (Transform child in poolTransform)
            {
                child.gameObject.SetActive(false);
            }
        }

        [Serializable]
        private class Pool
        {
            public Pools poolType;
            public GameObject prefab;
            public int size;
            [HideInInspector] public GameObject folder;
        }
    }
}
