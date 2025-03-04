using UnityEngine;

namespace Gigachad.Utility
{
    public static class YieldInstructionCache
    {
        public static readonly YieldInstruction WaitForEndOfFrame = new WaitForEndOfFrame();
        public static readonly YieldInstruction WaitForFixedUpdate = new WaitForFixedUpdate();
    }
}
