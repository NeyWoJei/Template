using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Systems;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Core
{
    public class PoolSystem : IInitializable
    {
        private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
        private readonly EventBus _eventBus;
        private Transform _poolRoot;

        [Inject]
        public PoolSystem(EventBus eventBus)
        {
            _eventBus = eventBus;
            Debug.Log("PoolSystem запущен.");
        }

        public void Initialize()
        {
            _poolRoot = new GameObject("[PoolRoot]").transform;
            Object.DontDestroyOnLoad(_poolRoot);
            _eventBus.GetSubject<PoolObjectRequest>().Subscribe(OnPoolObjectRequest);
            Debug.Log("PoolSystem инициализирован.");
        }

        public void CreatePool(string poolId, GameObject prefab, int initialSize, Transform parent = null)
        {
            if (_pools.ContainsKey(poolId))
            {
                Debug.LogWarning($"Pool с ID '{poolId}' уже существует.");
                return;
            }

            var pool = new Pool(prefab, initialSize, parent ?? _poolRoot);
            _pools.Add(poolId, pool);
            Debug.Log($"Создан Pool с ID '{poolId}' с размером {initialSize} объектов.");
        }

        public async UniTask<GameObject> GetObjectAsync(string poolId, Vector3 position, Quaternion rotation)
        {
            if (!_pools.TryGetValue(poolId, out var pool))
            {
                Debug.LogError($"Pool с ID '{poolId}' не найден.");
                return null;
            }

            var obj = await pool.GetObjectAsync();
            if (obj != null)
            {
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
                Debug.Log($"Получен объект '{obj.name}' из Pool с ID '{poolId}'.");
            }
            return obj;
        }

        public void ReturnObject(string poolId, GameObject obj)
        {
            if (!_pools.TryGetValue(poolId, out var pool))
            {
                Debug.LogError($"Pool с ID '{poolId}' не найден.");
                Object.Destroy(obj);
                return;
            }

            pool.ReturnObject(obj);
            Debug.Log($"Возвращен объект '{obj.name}' в Pool с ID '{poolId}'.");
        }

        private void OnPoolObjectRequest(PoolObjectRequest request)
        {
            GetObjectAsync(request.PoolId, request.Position, request.Rotation)
                .ContinueWith(obj =>
                {
                    if (obj != null)
                        _eventBus.TriggerEventWithId($"pool_object_spawned_{request.PoolId}");
                })
                .Forget();
        }
    }

    public class Pool
    {
        private readonly Queue<GameObject> _availableObjects = new Queue<GameObject>();
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public Pool(GameObject prefab, int initialSize, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                var obj = Object.Instantiate(prefab, parent);
                obj.SetActive(false);
                _availableObjects.Enqueue(obj);
            }
        }

        public async UniTask<GameObject> GetObjectAsync()
        {
            if (_availableObjects.Count > 0)
            {
                return _availableObjects.Dequeue();
            }

            var newObj = Object.Instantiate(_prefab, _parent);
            await UniTask.Yield();
            return newObj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_parent);
            _availableObjects.Enqueue(obj);
        }
    }

    public readonly struct PoolObjectRequest
    {
        public readonly string PoolId;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public PoolObjectRequest(string poolId, Vector3 position, Quaternion rotation)
        {
            PoolId = poolId;
            Position = position;
            Rotation = rotation;
        }
    }
}