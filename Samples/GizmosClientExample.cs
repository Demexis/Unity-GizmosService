using UnityEngine;

namespace GizmosService.Samples {
    public sealed class GizmosClientExample : MonoBehaviour {
        public GizmosSystemInstance systemInstance;
        
        public float speed = 1f;
        public float radius = 1f;
        public float radians;

        private void Start() {
            systemInstance.system.RegisterGizmo("Lines", () => {
                CustomGizmos.Color = Color.green;
                var pointA = Vector3.zero;
                var pointB = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));
                CustomGizmos.DrawCircle2D(pointA, radius);
                CustomGizmos.DrawAAPolyLine(pointA, pointB);
                CustomGizmos.DrawCircle2D(pointB, radius / 10f);
                return true;
            });

            systemInstance.system.RegisterGizmo("Labels", () => {
                CustomGizmos.Color = Color.red;
                GizmosTemplates.LabelWithBackground(Vector3.zero, $"Speed: {speed}\nRadius:{radius}\nRadians:{radians:F2}",
                    Color.green, new Color(0, 1, 0, 0.1f), 10, TextAnchor.MiddleCenter);
                return true;
            });

            systemInstance.system.RegisterGizmo("Failed", () => {
                Debug.Log("This message will be printed only once.");
                return false;
            });
        }

        private void Update() {
            radians += Time.deltaTime;
        }

        public void SetLinesCategory(bool active) {
            systemInstance.system.registrationsEnabled["Lines"] = active;
        }
        
        public void SetLabelsCategory(bool active) {
            systemInstance.system.registrationsEnabled["Labels"] = active;
        }
    }
}