using UnityEngine;
using UnityEngine.Assertions;

namespace FarmingRPG.Utilities
{
    public abstract class PooledComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Assert.IsTrue(GetComponents<PooledComponent>().Length == 1);
        }
    }
}
