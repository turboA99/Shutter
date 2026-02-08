using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime
{
    internal static class Initialize
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            InputSystem.RegisterLayout<VirtualCursor>("VirtualCursor");
            InputSystem.RegisterPrecompiledLayout<FastVirtualCursor>(FastVirtualCursor.metadata);
        } 
    }
}