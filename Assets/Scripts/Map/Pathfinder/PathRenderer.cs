using System.Collections.Generic;
using UnityEngine;

namespace Bodybuilder.Map.Pathfinder
{
    [RequireComponent(typeof(Path), typeof(LineRenderer))]
    public class PathRenderer : MonoBehaviour
    {
        private Map _map;

        [SerializeField] private Path _path;
        [SerializeField] private LineRenderer _line;
        
        [SerializeField] private Vector3 _offset;

        private readonly List<Vector3> _positions = new ();

        private void Awake()
        {
            _map = FindAnyObjectByType<Map>();
            _map.OnBuilt.AddListener(InitialLine);
            
            _path.OnChangePoints.AddListener(UpdateLine);
        }

        private void InitialLine()
        {
            UpdateLine(_path.GetPoints());
        }

        private void UpdateLine(Path.Waypoint[] points)
        {
            _positions.Clear();
            for (var i = 0; i < points.Length; i++)
            {
                var position = points[i].Position;

                if (i >= points.Length - 1)
                {
                    _positions.Add(_map.Layers[points[i].Layer].GetTilePosition(position) + _offset);
                    break;
                }

                TracePath(0, points[i + 1].Position.x);
                TracePath(1, points[i + 1].Position.y);
                continue;

                void TracePath(int axis, int target)
                {
                    if (position[axis] == target) { return; }
                    var movement = position[axis] < target ? 1 : -1;

                    for (; position[axis] != target; position[axis] += movement)
                    {
                        var tilePosition = _map.Layers[points[i].Layer].GetTilePosition(position);
                        _positions.Add(tilePosition + _offset);
                    }
                }
            }
            _line.positionCount = _positions.Count;
            _line.SetPositions(_positions.ToArray());
        }

        private void OnValidate()
        {
            _path = GetComponent<Path>();
            _line = GetComponent<LineRenderer>();
        }
    }
}