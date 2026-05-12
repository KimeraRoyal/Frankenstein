using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map.Pathfinder
{
    public class Path
    {
        public struct Waypoint : IEquatable<Waypoint>
        {
            private Vector2Int _position;
            private int _layer;

            public Vector2Int Position { get => _position; set => _position = value; }
            public int Layer { get => _layer; set => _layer = value; }

            public Waypoint(Vector2Int position, int layer)
            {
                _position = position;
                _layer = layer;
            }

            public static bool operator ==(Waypoint a, Waypoint b)
                => a._position == b._position && a._layer == b._layer;

            public static bool operator !=(Waypoint a, Waypoint b)
                => !(a == b);

            public bool Equals(Waypoint other)
                => _position.Equals(other._position) && _layer == other._layer;

            public override bool Equals(object obj)
                => obj is Waypoint other && Equals(other);

            public override int GetHashCode()
                => HashCode.Combine(_position, _layer);
        }
        
        private Waypoint[] _points;
        private int _distance;

        public int Distance => _distance;
        
        public int PointCount => _points.Length;

        public Action<Waypoint[]> OnChangePoints;

        public Waypoint[] GetPoints()
            => _points;

        public void SetPoints(Waypoint[] points)
        {
            _points = points;
            for (var i = 1; i < _points.Length; i++)
            {
                var xDistance = Mathf.Abs(_points[i].Position.x - _points[i - 1].Position.x);
                var yDistance = Mathf.Abs(_points[i].Position.y - _points[i - 1].Position.y);
                _distance += xDistance + yDistance;
            }
            OnChangePoints?.Invoke(_points);
        }

        public static implicit operator bool(Path path)
            => path != null;
    }
}