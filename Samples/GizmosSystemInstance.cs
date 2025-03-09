using UnityEngine;

namespace GizmosService.Samples {
    public sealed class GizmosSystemInstance : MonoBehaviour {
        public GizmosSystem system;

        private void Awake() {
            system = new GizmosSystem();
        }

        private void Update() {
            system.Update(Time.deltaTime);
        }
        
        private void OnDrawGizmos() {
            if (system == null) {
                return;
            }
            
            if (!system.ShowGizmos) {
                return;
            }
            system?.Render();
        }

        public void SetShowGizmos(bool showGizmos) {
            system.ShowGizmos = showGizmos;
        }
    }
}