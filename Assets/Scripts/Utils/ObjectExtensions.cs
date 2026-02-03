using UnityEngine;

namespace Utils
{
    public static class ObjectExtensions
    {
        public static T GetOrCreateComponent<T>(this MonoBehaviour monoBehaviour)
            where T : Component
        {
            return monoBehaviour.GetComponent<T>() ?? monoBehaviour.gameObject.AddComponent<T>();
        }
    }
}
