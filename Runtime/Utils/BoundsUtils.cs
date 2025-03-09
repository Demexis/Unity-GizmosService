using System.Diagnostics.Contracts;
using UnityEngine;

namespace GizmosService {
    public static class BoundsUtils {
        [Pure]
        public static CircleBounds CreateCircleBoundsFromLine(Vector2 pointA, Vector2 pointB) {
            var heading = pointB - pointA;
            var center = pointA + heading / 2f;
            var radius = heading.magnitude;

            return new CircleBounds(center, radius);
        }
        
        [Pure]
        public static bool TryMergeVectors(Vector3[] points, out Bounds bounds) {
            if (points == null) {
                bounds = default;
                return false;
            }
            
            if (points.Length == 0) {
                bounds = default;
                return false;
            }
            
            bounds = new Bounds(points[0], Vector3.zero);
            for (var i = 1; i < points.Length; i++) {
                bounds.Encapsulate(points[i]);
            }
            
            return true;
        }
    }
}