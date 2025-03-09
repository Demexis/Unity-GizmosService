#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GizmosService {
    internal static class GizmosUtility {
        public static float GetHandleSize(Vector3 position) {
#if UNITY_EDITOR
            return HandleUtility.GetHandleSize(position);
#endif
            return 0.5f;
        }
    }
}