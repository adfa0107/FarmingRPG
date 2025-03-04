using UnityEngine;
using UnityEngine.Assertions;

namespace Gigachad.Utility
{
    public abstract class PooledComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Assert.IsTrue(GetComponents<PooledComponent>().Length == 1);
        }
    }
}
