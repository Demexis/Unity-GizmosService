#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GizmosService {
    public static class CustomGizmos {
        public static bool AllowDraw { get; set; } = true;
        public static bool UseCameraOcclusion { get; set; } = false;

        private static Color color;
        
        public static Color Color {
            get => color;
            set {
                color = value;
#if UNITY_EDITOR
                Gizmos.color = Handles.color = color;
#endif
            }
        }

        private static bool CanDraw {
            get {
#if UNITY_EDITOR
                if (Application.isEditor && !Application.isPlaying && Handles.ShouldRenderGizmos()) {
                    return true;
                }
                
                return AllowDraw && Handles.ShouldRenderGizmos();
#endif
                // TODO: This is sad that Unity doesn't provide Gizmos and Handles functionality for builds
                return false;
            }
        }

        public static void DrawWireCube(Vector3 center, Vector3 size) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new Bounds(center, size))) {
                Handles.DrawWireCube(center, size);
            }
#endif
        }

        public static void DrawCircle2D(Vector3 center, float radius) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            DrawWireDisc(center, Vector3.back, radius);
#endif
        }

        public static void DrawWireDisc(Vector3 center, Vector3 normal, float radius) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new CircleBounds(center, radius))) {
                Handles.DrawWireDisc(center, normal, radius);
            }
#endif
        }
        
        public static void DrawWireDisc(Vector3 center, Vector3 normal, float radius, float thickness) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new CircleBounds(center, radius))) {
                Handles.DrawWireDisc(center, normal, radius, thickness);
            }
#endif
        }
        
        public static void DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new CircleBounds(center, radius))) {
                Handles.DrawWireArc(center, normal, from, angle, radius);
            }
#endif
        }

        public static void DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new CircleBounds(center, radius))) {
                Handles.DrawWireArc(center, normal, from, angle, radius, thickness);
            }
#endif
        }

        public static void DrawLine(Vector3 p1, Vector3 p2) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(BoundsUtils.CreateCircleBoundsFromLine(p1, p2))) {
                Handles.DrawLine(p1, p2);
            }
#endif
        }
        
        public static void DrawLine(Vector3 p1, Vector3 p2, float thickness) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(BoundsUtils.CreateCircleBoundsFromLine(p1, p2))) {
                Handles.DrawLine(p1, p2, thickness);
            }
#endif
        }
        
        public static void DrawLines(Vector3[] lineSegments) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!BoundsUtils.TryMergeVectors(lineSegments, out var bounds)) {
                return;
            }
            
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(bounds)) {
                Handles.DrawLines(lineSegments);
            }
#endif
        }
        
        public static void DrawDottedLine(Vector3 p1, Vector3 p2, float screenSpaceSize) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(BoundsUtils.CreateCircleBoundsFromLine(p1, p2))) {
                Handles.DrawDottedLine(p1, p2, screenSpaceSize);
            }
#endif
        }
        
        public static void DrawDottedLines(Vector3[] lineSegments, float screenSpaceSize) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!BoundsUtils.TryMergeVectors(lineSegments, out var bounds)) {
                return;
            }
            
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(bounds)) {
                Handles.DrawDottedLines(lineSegments, screenSpaceSize);
            }
#endif
        }
        
        public static void DrawPolyLine(params Vector3[] points) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!BoundsUtils.TryMergeVectors(points, out var bounds)) {
                return;
            }
            
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(bounds)) {
                Handles.DrawPolyLine(points);
            }
#endif
        }
        
        public static void DrawSolidDisc(Vector3 center, Vector3 normal, float radius) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new CircleBounds(center, radius))) {
                Handles.DrawSolidDisc(center, normal, radius);
            }
#endif
        }
        
        public static void DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(new CircleBounds(center, radius))) {
                Handles.DrawSolidArc(center, normal, from, angle, radius);
            }
#endif
        }

        public static void DrawAAConvexPolygon(params Vector3[] points) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!BoundsUtils.TryMergeVectors(points, out var bounds)) {
                return;
            }
            
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(bounds)) {
                Handles.DrawAAConvexPolygon(points);
            }
#endif
        }
        
        public static void DrawAAPolyLine(params Vector3[] points) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!BoundsUtils.TryMergeVectors(points, out var bounds)) {
                return;
            }

            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(bounds)) {
                Handles.DrawAAPolyLine(points);
            }
#endif
        }
        
        public static void DrawAAPolyLine(float width, params Vector3[] points) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            if (!BoundsUtils.TryMergeVectors(points, out var bounds)) {
                return;
            }
            
            if (!UseCameraOcclusion || CameraOcclusion.CamerasSeeGraphics(bounds)) {
                Handles.DrawAAPolyLine(width, points);
            }
#endif
        }

        public static void Label(Vector3 position, GUIContent content) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            Handles.Label(position, content);
#endif
        }
        
        public static void Label(Vector3 position, GUIContent content, GUIStyle style) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            Handles.Label(position, content, style);
#endif
        }
        
        public static void Label(Vector3 position, Texture image) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            Handles.Label(position, image);
#endif
        }
        
        public static void Label(Vector3 position, string text) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            Handles.Label(position, text);
#endif
        }
        
        public static void Label(Vector3 position, string text, GUIStyle style) {
            if (!CanDraw) {
                return;
            }
#if UNITY_EDITOR
            Handles.Label(position, text, style);
#endif
        }
    }
}