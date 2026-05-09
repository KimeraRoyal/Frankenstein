using System.Collections.Generic;
using UnityEngine;
using Mouse = Bodybuilder.Input.Mouse;

namespace Bodybuilder.Map.Pathfinder
{
    public class PathDrawer : MonoBehaviour
    {
        private Map _map;

        private Mouse _mouse;
        private Camera _camera;

        [SerializeField] private LayerMask _mapMask;
        private Vector2Int _lastTile;

        private Path _path;
        private readonly List<Path.Waypoint> _points = new();

        private void Awake()
        {
            _map = FindAnyObjectByType<Map>();
            
            _mouse = FindAnyObjectByType<Mouse>();
            _camera = _mouse.GetComponent<Camera>();

            _path = GetComponentInChildren<Path>();
            _points.Add(new Path.Waypoint(Vector2Int.zero, 1));
            _points.Add(new Path.Waypoint(Vector2Int.zero, 1));
            
            _mouse.OnMoved += MouseMoved;
            _mouse.OnPressed += MousePressed;
        }

        private void MouseMoved(Vector2 screenPos, Vector2 delta)
        {
            var ray = _camera.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, _camera.farClipPlane));
            
            if(!Physics.Raycast(ray, out var hitInfo, _mapMask)) { return; }

            var tilePosition = _map.Layers[1].GetTileAt(hitInfo.point).Position;
            if(tilePosition == _lastTile) { return; }
            _lastTile = tilePosition;
            
            _points[^1] = new Path.Waypoint(tilePosition, 1);
            _path.SetPoints(_points.ToArray());
        }

        private void MousePressed(Vector2 screenPos)
        {
            if(_points == null || _points.Count < 1 || (_points.Count > 2 && _points[^1] == _points[^2])) { return; }
            _points.Add(_points[^1]);
        }
    }
}