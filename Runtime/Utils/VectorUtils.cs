using System.Diagnostics.Contracts;
using UnityEngine;

namespace GizmosService {
    public static class VectorUtils {
        [Pure]
        public static Vector3 GetVectorFromAngle(float angle) {
            // angle = 0 -> 360
            var angleRad = angle * Mathf.Deg2Rad;

            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
        
        [Pure]
        public static float GetAngleFromVectorFloat(Vector3 dir) {
            dir.Normalize();
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) {
                n += 360;
            }
            return n;
        }
    }
}