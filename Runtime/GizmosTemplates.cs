using UnityEngine;

namespace GizmosService {
    public static class GizmosTemplates {
        public static void DrawOutlinedBox(Bounds bounds, Color color, float fillAlpha = 0.05f, float outlineAlpha = 0.5f) {
            var leftBottom = bounds.min;
            var leftTop = bounds.min.SetY(0) + bounds.max.SetX(0);
            var rightTop = bounds.max;
            var rightBottom = bounds.min.SetX(0) + bounds.max.SetY(0);

            CustomGizmos.Color = color.SetA(fillAlpha);
            CustomGizmos.DrawAAConvexPolygon(leftBottom, leftTop, rightTop, rightBottom);
            CustomGizmos.Color = color.SetA(outlineAlpha);
            CustomGizmos.DrawAAPolyLine(leftBottom, leftTop, rightTop, rightBottom, leftBottom);
        }

        public static void DrawOutlinedDisc(Vector3 center, float radius, Color color, float fillAlpha = 0.05f, float outlineAlpha = 0.5f, float outlineThickness = 0f) {
            CustomGizmos.Color = color.SetA(fillAlpha);
            CustomGizmos.DrawSolidDisc(center, Vector3.forward, radius);
            CustomGizmos.Color = color.SetA(outlineAlpha);
            CustomGizmos.DrawWireDisc(center, Vector3.forward, radius, outlineThickness);
        }
        
        public static void DrawOutlinedArc(Vector3 center, float startPoint, float angle, float radius, Color color, float fillAlpha = 0.05f, float outlineAlpha = 0.5f) {
            CustomGizmos.Color = color.SetA(fillAlpha);
            CustomGizmos.DrawSolidArc(center, Vector3.forward, VectorUtils.GetVectorFromAngle(startPoint), angle, radius);
            CustomGizmos.Color = color.SetA(outlineAlpha);
            CustomGizmos.DrawWireArc(center, Vector3.forward, VectorUtils.GetVectorFromAngle(startPoint), angle, radius);
        }

        public static void LabelWithBackground(Vector3 pos, string text, Color textColor, Color backgroundColor, int fontSize = 8, TextAnchor textAnchor = TextAnchor.UpperLeft) {
            var zoom = GizmosUtility.GetHandleSize(pos.SetZ(-0.5f));
            // var guiPos = HandleUtility.WorldToGUIPoint(pos);
            
            // the style object allows you to control font size, among many other settings
            var style = new GUIStyle {
                normal = new GUIStyleState {
                    textColor = textColor,
                    background = GuiUtils.MakeTex(2, 2, backgroundColor),
                },
                
                alignment = textAnchor,
                
                // as you zoom out, the ortho size actually increases, 
                // so dividing by it makes the font smaller which is exactly what we need
                fontSize = Mathf.FloorToInt(fontSize / zoom),
            };
            
            CustomGizmos.Label(pos, text, style);
            // var size = style.CalcSize(new GUIContent(text)) * zoom;
            // Handles.BeginGUI();
            // GUI.Label(new Rect(guiPos, size), text, style);
            // Handles.EndGUI();
        }

        public static void DrawArrowLine(Vector3 p1, Vector3 p2, float arrowRadius) {
            CustomGizmos.DrawLine(p1, p2);
            var heading = p2 - p1;
            var headingAngle = VectorUtils.GetAngleFromVectorFloat(heading);

            var arrowLeft = VectorUtils.GetVectorFromAngle(headingAngle + 135) * arrowRadius;
            var arrowRight = VectorUtils.GetVectorFromAngle(headingAngle - 135) * arrowRadius;
            
            CustomGizmos.DrawLine(p2, p2 + arrowLeft);
            CustomGizmos.DrawLine(p2, p2 + arrowRight);
        }
        
        public static void DrawArrowAAPolyLine(Vector3 p1, Vector3 p2, float arrowRadius) {
            CustomGizmos.DrawAAPolyLine(p1, p2);
            var heading = p2 - p1;
            var headingAngle = VectorUtils.GetAngleFromVectorFloat(heading);

            var arrowLeft = VectorUtils.GetVectorFromAngle(headingAngle + 135) * arrowRadius;
            var arrowRight = VectorUtils.GetVectorFromAngle(headingAngle - 135) * arrowRadius;
            
            CustomGizmos.DrawAAPolyLine(p2, p2 + arrowLeft);
            CustomGizmos.DrawAAPolyLine(p2, p2 + arrowRight);
        }
    }
}