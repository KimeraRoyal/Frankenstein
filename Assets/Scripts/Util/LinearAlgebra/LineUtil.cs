using UnityEngine;

namespace Bodybuilder.Util.LinearAlgebra
{
    public static class LineUtil
    {
        public static bool IsPointWithinLine(Vector2 l1, Vector2 l2, Vector2 p)
            =>  GetPointRatioOnLine(l1, l2, p) is >= 0.0f and <= 1.0f;
        
        public static bool IsPointToLeftOfLine(Vector2 l1, Vector2 l2, Vector2 p)
            => (l2.x - l1.x) * (p.y - l1.y) - (l2.y - l1.y) * (p.x - l1.x) < 0.0f;

        public static Vector2 GetClosestPointOnLine(Vector2 l1, Vector2 l2, Vector2 p)
        {
            var dir = (l2 - l1).normalized;
            var distance = Vector2.Dot(p - l1, dir);
            return l1 + dir * distance;
        }
        
        public static Vector2 ClampPointToLine(Vector2 l1, Vector2 l2, Vector2 p)
        {
            p.x = Mathf.Clamp(p.x, l1.x < l2.x ? l1.x : l2.x, l1.x < l2.x ? l2.x : l1.x);
            p.y = Mathf.Clamp(p.y, l1.y < l2.y ? l1.y : l2.y, l1.y < l2.y ? l2.y : l1.y);
            return p;
        }
        
        public static float GetPointRatioOnLine(Vector2 l1, Vector2 l2, Vector2 p)
        {
            var ab = l2 - l1;
            var ac = p - l1;
            return Vector2.Dot(ac, ab) / Vector2.Dot(ab, ab);
        }

        public static bool DoLinesIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
            => CCW(a1, b1, b2) != CCW(a2, b1, b2) && CCW(a1, a2, b1) != CCW(a1, a2, b2);

        private static bool CCW(Vector2 a, Vector2 b, Vector2 c)
            => (c.y - a.y) * (b.x - a.x) > (b.y - a.y) * (c.x - a.x);
    }
}