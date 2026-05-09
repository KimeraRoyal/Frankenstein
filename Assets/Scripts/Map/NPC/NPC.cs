using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map.NPC
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private NPCInfo _info;

        [SerializeField] private Vector2Int _position;
        [SerializeField] private int _currentLayer;

        public NPCInfo Info => _info;

        public Vector2Int Position
        {
            get => _position;
            set
            {
                if(_position == value) { return; }
                _position = value;
                OnMove?.Invoke(_position);
            }
        }

        public int CurrentLayer
        {
            get => _currentLayer;
            set
            {
                if(_currentLayer == value) { return; }
                _currentLayer = value;
                OnChangeLayer?.Invoke(_currentLayer);
            }
        }

        public UnityEvent<Vector2Int> OnMove;
        public UnityEvent<int> OnChangeLayer;
    }
}
