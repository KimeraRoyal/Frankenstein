using System;
using UnityEngine;

namespace Bodybuilder.Util.LinearAlgebra
{
    [Serializable]
    public struct Line
    {
        [SerializeField] private Vector2 _a, _b;
        
        public Vector2 A { get => _a; set => _a = value; }
        public Vector2 B { get => _b; set => _b = value; }
        
        public Vector2 Direction => (B - A).normalized;
        public float Magnitude => (B - A).magnitude;

        public Vector2 Up => Vector3.Cross(Direction, Vector3.forward);

        public Line(Vector2 a, Vector2 b)
        {
            _a = a;
            _b = b;
        }

        public Vector2 GetPointOnLine(float t)
            => Vector2.Lerp(A, B, t);

        public bool IsPointWithinLine(Vector2 point)
            => LineUtil.IsPointWithinLine(_a, _b, point);

        public bool IsPointToLeft(Vector2 point)
            => LineUtil.IsPointToLeftOfLine(_a, _b, point);

        public Vector2 GetClosestPoint(Vector2 point)
            => LineUtil.GetClosestPointOnLine(_a, _b, point);

        public Vector2 GetClampedPoint(Vector2 point)
            => LineUtil.ClampPointToLine(_a, _b, point);

        public float GetPointRatioOnLine(Vector2 point)
            => LineUtil.GetPointRatioOnLine(_a, _b, point);

        public bool Intersects(Line other)
            => LineUtil.DoLinesIntersect(_a, _b, other._a, other._b);
        
        public Vector2 GetDistancePastLine(Vector2 point)
        {
            var closest = LineUtil.GetClosestPointOnLine(_a, _b, point);
            return LineUtil.GetPointRatioOnLine(_a, _b, closest) switch
            {
                < 0.0f => closest - _a,
                > 1.0f => closest - _b,
                _ => Vector2.zero
            };
        }

        public Line Extend(Vector2 amount)
            => new(_a - amount, _b + amount);

        public Line GetLineFromPointToClosest(Vector3 point)
            => new(point, GetClosestPoint(point));

        public static Line operator +(Line a, Line b)
            => new(a._a + b._a, a._b + b._b);
        
        public static Line operator +(Line line, Vector2 vector)
            => new(line._a + vector, line._b + vector);
        
        public static Line operator +(Line line, Vector3 vector)
            => line + (Vector2) vector;
        
        public static Line operator +(Vector2 vector, Line line)
            => new(vector + line._a, vector + line._b);
        
        public static Line operator +(Vector3 vector, Line line)
            =>  (Vector2) vector + line;
        
        public static Line operator -(Line a, Line b)
            => new(a._a - b._a, a._b - b._b);
        
        public static Line operator -(Line line, Vector2 vector)
            => new(line._a - vector, line._b - vector);
        
        public static Line operator -(Vector3 vector, Line line)
            => line - (Vector2) vector;
        
        public static Line operator -(Vector2 vector, Line line)
            => new(vector - line._a, vector - line._b);
        
        public static Line operator -(Line line, Vector3 vector)
            => (Vector2) vector - line;

        public static Line operator *(Quaternion rotation, Line line)
            => new(rotation * line._a, rotation * line._b);
    }
}