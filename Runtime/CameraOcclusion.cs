#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Diagnostics.Contracts;
using UnityEngine;

namespace GizmosService {
    internal static class CameraOcclusion {
        public static bool CamerasSeeGraphics(Bounds graphicsWorldBounds) {
#if UNITY_EDITOR
            var sceneView = SceneView.currentDrawingSceneView;
            if (sceneView != null) {
                if (CameraSeesGraphics(sceneView.camera, graphicsWorldBounds)) {
                    return true;
                }
            }
#endif
            
            foreach (var camera in Camera.allCameras) {
                if (CameraSeesGraphics(camera, graphicsWorldBounds)) {
                    return true;
                }
            }

            return false;
        }
        
        public static bool CamerasSeeGraphics(CircleBounds graphicsWorldBounds) {
#if UNITY_EDITOR
            var sceneView = SceneView.currentDrawingSceneView;
            if (sceneView != null) {
                if (CameraSeesGraphics(sceneView.camera, graphicsWorldBounds)) {
                    return true;
                }
            }
#endif
            
            foreach (var camera in Camera.allCameras) {
                if (CameraSeesGraphics(camera, graphicsWorldBounds)) {
                    return true;
                }
            }

            return false;
        }
        
        private static bool CameraSeesGraphics(Camera camera, Bounds graphicsWorldBounds) {
            if (camera == null) {
                return false;
            }
            
            var camMin = (Vector2)camera.ScreenToWorldPoint(Vector3.zero);
            var camMax = (Vector2)camera.ScreenToWorldPoint(GetScreenSize());

            var cameraBoundsSize = camMax - camMin;
            var cameraWorldBounds = new Bounds(camMin + cameraBoundsSize / 2f, cameraBoundsSize);

            return cameraWorldBounds.Intersects(graphicsWorldBounds);
        }
        
        private static bool CameraSeesGraphics(Camera camera, CircleBounds graphicsWorldBounds) {
            if (camera == null) {
                return false;
            }
            
            var camMin = (Vector2)camera.ScreenToWorldPoint(Vector3.zero);
            var camMax = (Vector2)camera.ScreenToWorldPoint(GetScreenSize());

            var cameraBoundsSize = camMax - camMin;

            var cameraWorldBounds = new CircleBounds(camMin + cameraBoundsSize / 2f, cameraBoundsSize.magnitude);
            
            return cameraWorldBounds.Intersects(graphicsWorldBounds);
        }
        
        [Pure]
        private static Vector2 GetScreenSize() {
            return new Vector2(Screen.width, Screen.height);
        }
    }
}