using System.Collections;
using UnityEngine;

namespace FarmingRPG.Utilities
{
    public class CooldownCoroutineCache
    {
        public float Cooldown
        {
            get => _cooldown;
            set
            {
                _cooldown = value;
                _waitForCooldown = new WaitForSeconds(_cooldown);
            }
        }
        public YieldInstruction WaitForCooldown => _waitForCooldown;
        public IEnumerator coroutine;
        
        private float _cooldown;
        private YieldInstruction _waitForCooldown;
    }
}