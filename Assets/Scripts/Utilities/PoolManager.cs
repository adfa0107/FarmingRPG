using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

namespace FarmingRPG.Utilities
{
    public class PoolManager : MonoBehaviour
    {
        [Serializable]
        public struct PoolingData
        {
            public GameObject prefab;
            public int defaultCapacity;
            public int maxSize;
        }
        
        [SerializeField, Tooltip("풀에 넣을 Prefab이랑 오브젝트를 처음에 생성할 개수(defaultCapacity), 최대로 유지할 개수(maxSize)를 설정\n최대로 유지할 개수를 넘어가면 돌려줄 때 삭제")]
        private PoolingData[] poolingObjects;
        
        private readonly Dictionary<Type, IObjectPool<PooledComponent>> _poolDictionary = new Dictionary<Type, IObjectPool<PooledComponent>>();

        private void Start()
        {
            if (poolingObjects is { Length: > 0 })
            {
                foreach (PoolingData poolingObject in poolingObjects)
                {
                    RegisterPrefab(poolingObject.prefab, poolingObject.defaultCapacity, poolingObject.maxSize);
                }
            }
        }

        public void RegisterPrefab(GameObject prefab, int defaultCapacity, int maxSize)
        {
            Assert.IsNotNull(prefab);
            PooledComponent pooledComponent = prefab.GetComponent<PooledComponent>();
            Assert.IsNotNull(pooledComponent);
            Assert.IsFalse(_poolDictionary.ContainsKey(pooledComponent.GetType()));

            ObjectPool<PooledComponent> pool = new ObjectPool<PooledComponent>(
                () => CreateObject(prefab),
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyObject,
                true, defaultCapacity, maxSize);
            _poolDictionary.Add(pooledComponent.GetType(), pool);

            for (int i = 0; i < defaultCapacity; i++)
            {
                pool.Release(CreateObject(prefab));
            }
        }

        public T GetPoolObject<T>() where T : PooledComponent
        {
            Type componentType = typeof(T);
            Assert.IsTrue(_poolDictionary.ContainsKey(componentType));
            
            return (T)_poolDictionary[componentType].Get();
        }

        public void ReleasePoolObject<T>(T pooledComponent) where T : PooledComponent
        {
            Type componentType = typeof(T);
            Assert.IsTrue(_poolDictionary.ContainsKey(componentType));
            
            _poolDictionary[componentType].Release(pooledComponent);
        }

        private PooledComponent CreateObject(GameObject prefab)
        {
            PooledComponent pooledComponent = Instantiate(prefab).GetComponent<PooledComponent>();
            pooledComponent.gameObject.SetActive(false);
            
            return pooledComponent;
        }

        private void OnGetFromPool(PooledComponent pooledComponent)
        {
            pooledComponent.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(PooledComponent pooledComponent)
        {
            pooledComponent.gameObject.SetActive(false);
        }

        private void OnDestroyObject(PooledComponent pooledComponent)
        {
            Destroy(pooledComponent.gameObject);
        }
    }
}
