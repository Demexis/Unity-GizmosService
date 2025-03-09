using System.Diagnostics.Contracts;
using UnityEngine;

namespace GizmosService {
    internal static class VectorExtensions {
        [Pure]
        public static Vector3 SetX(this Vector3 vector, float x) => new(x, vector.y, vector.z);
        [Pure]
        public static Vector3 SetY(this Vector3 vector, float y) => new(vector.x, y, vector.z);
        [Pure]
        public static Vector3 SetZ(this Vector3 vector, float z) => new(vector.x, vector.y, z);
    }
}