using UnityEngine;

namespace Gigachad.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (T)FindAnyObjectByType(typeof(T));
                    if (_instance is null)
                    {
                        GameObject gameObject = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        gameObject.AddComponent<T>();
                    }
                }
            
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
        
        /// <summary>
        /// Awake를 대신하는 함수<br /><br />
        /// Singleton의 Awake에서 자기가 _instance가 아니면 스스로를 파괴하는 Destroy(this)를 사용하는데
        /// virtual override로 Awake를 구성하면 Destroy(this)를 했을 때도 override한 Awake가 실행될 수 있어서 대신 만든 함수
        /// </summary>
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
        
        /// <summary>
        /// Component를 가져올 때 사용할 함수<br /><br />
        /// 미리 제작된 Singleton이 존재하지 않을 경우 새 오브젝트를 생성해서 거기에 Singleton을 붙여서 다른 Component가 존재하지 않음
        /// 그래서 그 때는 AddComponent를 사용해야해서 Component가 있으면 가져오고 없으면 추가하는 GetOrAddComponent 함수를 만듦
        /// </summary>
        protected TComponent GetOrAddComponent<TComponent>() where TComponent : Component
        {
            TComponent component = GetComponent<TComponent>();
            if (!component)
            {
                component = gameObject.AddComponent<TComponent>();
            }
            
            return component;
        }
    }
}
