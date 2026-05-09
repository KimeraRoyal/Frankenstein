using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map.Pathfinder
{
    public class Path : MonoBehaviour
    {
        [Serializable]
        public struct Waypoint
        {
            [SerializeField] private Vector2Int _position;
            [SerializeField] private int _layer;

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
        }
        
        [SerializeField] private Waypoint[] _points;

        public UnityEvent<Waypoint[]> OnChangePoints;

        public Waypoint[] GetPoints()
            => _points;

        public void SetPoints(Waypoint[] points)
        {
            _points = points;
            OnChangePoints?.Invoke(_points);
        }
    }
}