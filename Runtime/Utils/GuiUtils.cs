using System.Diagnostics.Contracts;
using UnityEngine;

namespace GizmosService {
    public static class GuiUtils {
        [Pure]
        public static Texture2D MakeTex(int width, int height, Color col) {
            var pix = new Color[width * height];
            for (var i = 0; i < pix.Length; ++i) {
                pix[i] = col;
            }
            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}