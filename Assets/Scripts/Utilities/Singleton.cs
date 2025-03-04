using UnityEngine;
using UnityEngine.Assertions;

namespace FarmingRPG.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                Assert.IsNotNull(_instance);
            
                return _instance;
            }
        }
        
        protected void Awake()
        {
            if (_instance is null)
            {
                _instance = this as T;
                Initialize();
            }
            else
            {
                Destroy(this);
            }
        }
        
        protected virtual void Initialize()
        {
            
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}
