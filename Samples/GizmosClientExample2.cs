using UnityEngine;

namespace GizmosService.Samples {
    public sealed class GizmosClientExample2 : MonoBehaviour {
        public GizmosSystemInstance systemInstance;
        
        private void Start() {
            systemInstance.system.RegisterGizmoTemp("Templates", () => {
                CustomGizmos.Color = Color.red;
                GizmosTemplates.DrawArrowLine(new Vector3(-1, 0.5f), new Vector3(-1.5f, 0.75f), 0.2f);
                GizmosTemplates.DrawOutlinedArc(new Vector3(-1, -0.5f), 180f, 45, 0.5f, Color.yellow);
                
                GizmosTemplates.LabelWithBackground(Vector3.zero, "Sample Text", Color.green, new Color(0, 1, 0, 0.1f), 10, TextAnchor.MiddleCenter);
                GizmosTemplates.DrawOutlinedBox(new Bounds(new Vector3(1, 0.5f), new Vector3(0.5f, 0.5f)), Color.magenta);
                GizmosTemplates.DrawOutlinedDisc(new Vector3(1, -0.5f), 0.25f, Color.cyan);
                return true;
            }, 10f);
        }
    }
}