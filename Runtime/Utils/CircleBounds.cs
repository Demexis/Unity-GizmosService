using UnityEngine;

namespace GizmosService {
    public readonly struct CircleBounds {
        public readonly Vector2 center;
        public readonly float radius;
        public readonly float sqrRadius;

        public CircleBounds(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
            sqrRadius = radius * radius;
        }

        public bool Intersects(CircleBounds circleBounds) {
            var sqrDistance = (circleBounds.center - center).sqrMagnitude;
            var sqrThreshold = sqrRadius + circleBounds.sqrRadius;
            
            if (sqrDistance > sqrThreshold) {
                return false;
            }

            return true;
        }
    }
}