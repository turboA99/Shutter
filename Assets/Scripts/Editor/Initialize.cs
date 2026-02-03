using Input;
using UnityEditor;
using UnityEngine.InputSystem;

namespace Editor
{
    [InitializeOnLoad]
    internal class Initialize
    {
        static Initialize()
        {
            InputSystem.RegisterLayout<VirtualCursor>("VirtualCursor");
        }
    }
}
