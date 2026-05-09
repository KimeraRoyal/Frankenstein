using Bodybuilder.Time;
using UnityEngine;

namespace Bodybuilder.Map.NPC
{
    [RequireComponent(typeof(NPC))]
    public class NPCMovement : MonoBehaviour
    {
        private NPC _npc;
        private Clock _clock;

        [SerializeField] private Vector2Int _targetPosition;
        [SerializeField] private int _targetLayer;
        private int _tickCounter;

        public bool AtTarget => _targetPosition == _npc.Position && _targetLayer == _npc.CurrentLayer;

        private void Awake()
        {
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

        private void Tick(int minutes)
        {
            if(AtTarget) { return; }
            
            _tickCounter++;
            if(_tickCounter < _npc.Info.MinutesToMoveTile) { return; }
            _tickCounter -= _npc.Info.MinutesToMoveTile;
            
            if(Move() || JumpLayer()) { return; }
            
            if(AtTarget) { return; }
            _tickCounter = 0;
        }

        private bool Move()
        {
            if(_npc.Position == _targetPosition) { return false; }

            var horizontal = _npc.Position.x != _targetPosition.x;
            _npc.Position += new Vector2Int(
                horizontal ? _npc.Position.x < _targetPosition.x ? 1 : -1 : 0,
                !horizontal ? _npc.Position.y < _targetPosition.y ? 1 : -1 : 0);
            
            return true;
        }

        private bool JumpLayer()
        {
            if(_npc.CurrentLayer == _targetLayer) { return false; }

            _npc.CurrentLayer += _npc.CurrentLayer < _targetLayer ? 1 : -1;

            return true;
        }

        private void OnValidate()
        {
            _npc = GetComponent<NPC>();
        }
    }
}