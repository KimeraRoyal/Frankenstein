using Bodybuilder.Map.Pathfinder;
using Bodybuilder.Time;
using Bodybuilding.Map.Tiles;
using UnityEngine;

namespace Bodybuilder.Map.Npcs
{
    [RequireComponent(typeof(Npc))]
    public class NpcMovement : MonoBehaviour
    {
        private Npc _npc;
        private Clock _clock;

        private Path _path;
        private int _currentWaypoint;
        
        private int _tickCounter;

        public bool AtPoint
        {
            get
            {
                if (AtTarget) { return true; }

                var targetPoint = _path.GetPoints()[_currentWaypoint + 1];
                return _npc.Position == targetPoint.Position/* && _npc.CurrentLayer == targetPoint.Layer*/;
            }
        }
        public bool AtTarget => _path == null || _currentWaypoint + 1 >= _path.PointCount;

        private void Awake()
        {
            _npc = GetComponent<Npc>();
            _clock = FindAnyObjectByType<Clock>();
        }

        private void OnEnable()
        {
            _clock.OnMinute.AddListener(Tick);
        }

        private void OnDisable()
        {
            _clock.OnMinute.RemoveListener(Tick);
        }

        public void SetPath(Path path)
        {
            if(path == _path) { return; }
            
            if (path && path.Distance > 0)
            {
                var points = path.GetPoints();
                if (points[0].Position != _npc.Position || points[0].Layer != _npc.CurrentLayer)
                {
                    points[0].Position = _npc.Position;
                    points[0].Layer = _npc.CurrentLayer;
                    path.SetPoints(points);
                }
            }

            _path = path;
            _currentWaypoint = 0;
        }

        private void Tick(int minutes)
        {
            if(AtTarget) { return; }
            
            _tickCounter++;
            if(_tickCounter < _npc.Info.MinutesToMoveTile + _npc.CurrentTile.Type.MovementPenalty) { return; }
            _tickCounter -= _npc.Info.MinutesToMoveTile;
            
            if(Move() || JumpLayer()) { return; }
            if (AtPoint) { _currentWaypoint++; }

            if (AtTarget)
            {
                _path = null;
                return;
            }
            _tickCounter = 0;
        }

        private bool Move()
        {
            if (AtTarget) { return false; }

            var targetPosition = _path.GetPoints()[_currentWaypoint + 1].Position;
            if(_npc.Position == targetPosition) { return false; }
            _npc.Move((targetPosition - _npc.Position).ToDirection(), _npc.Info.CanShove);
            return true;
        }

        private bool JumpLayer()
        {
            if (AtTarget) { return false; }
            
            var targetLayer = _path.GetPoints()[_currentWaypoint + 1].Layer;
            if(_npc.CurrentLayer == targetLayer) { return false; }

            // TODO: Moving up and down layers
            // _npc.CurrentLayer += _npc.CurrentLayer < targetLayer ? 1 : -1;

            return true;
        }
    }
}