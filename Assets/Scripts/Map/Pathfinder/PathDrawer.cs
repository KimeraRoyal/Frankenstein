using System;
using System.Collections.Generic;
using Bodybuilder.Map.Selection;
using Bodybuilding.Map.Tiles;
using UnityEngine;

namespace Bodybuilder.Map.Pathfinder
{
    public class PathDrawer : MonoBehaviour
    {
        private MapSelection _selection;

        [SerializeField] private PathVisualizer _pathVisualizerPrefab;

        private Path _path;
        private PathVisualizer _pathVisualizer;
        private Action<Path> OnPathCreated;
        private readonly List<Path.Waypoint> _points = new();
        private Path.Waypoint _lastSelectedPoint;

        private void Awake()
        {
            _selection = FindAnyObjectByType<MapSelection>();

            _selection.OnTileHovered += OnHoverTile;
            _selection.OnTileSelected += OnTileSelected;
        }

        public bool RequestPath(Vector2Int startingPosition, Action<Path> pathCreatedCallback)
        {
            if (_path) { return false; }

            _path = new Path();
            _pathVisualizer = Instantiate(_pathVisualizerPrefab, transform);
            _pathVisualizer.Path = _path;
            
            _points.Clear();
            _lastSelectedPoint = new Path.Waypoint(startingPosition, 1);
            _points.Add(_lastSelectedPoint);
            _points.Add(_lastSelectedPoint);
            
            OnPathCreated = pathCreatedCallback;
            return true;
        }

        public void ReturnPath()
        {
            if(!_path) { return; }
            
            // TODO: If path is empty
            OnPathCreated?.Invoke(_path);
            OnPathCreated = null;
            
            _path = null;
            Destroy(_pathVisualizer.gameObject);
        }

        public void Extend()
        {
            if(!_path) { return; }
            _points.Add(_points[^1]);
            _path.SetPoints(_points.ToArray());
        }

        private void OnHoverTile(Tile tile)
        {
            if (!_path) { return; }
            
            _points[^1] = tile != null && tile.Type ? new Path.Waypoint(tile.Position, tile.LayerIndex) : _points[^2];
            _path.SetPoints(_points.ToArray());
        }

        private bool OnTileSelected(Tile tile)
        {
            if(!_path || tile == null || !tile.Type) { return true; }
            
            var waypoint = new Path.Waypoint(tile.Position, tile.LayerIndex);
            if (waypoint == _lastSelectedPoint)
            {
                ReturnPath();
                return true;
            }
            _points[^1] = waypoint;
            _lastSelectedPoint = waypoint;
            Extend();
            return false;
        }
    }
}