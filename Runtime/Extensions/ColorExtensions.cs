using System.Diagnostics.Contracts;
using UnityEngine;

namespace GizmosService {
    internal static class ColorExtensions {
        [Pure]
        public static Color SetA(this Color color, float a) {
            return new Color(color.r, color.g, color.b, a);
        }
    }
}