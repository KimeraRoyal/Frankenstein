using Bodybuilding.Map.Tile;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map
{
    public class Tile : MonoBehaviour
    {
        private TileType _type;

        private Vector2Int _position;
        private float _elevation;

        public UnityEvent<TileType> OnAssignedType;

        public TileType Type
        {
            get => _type;
            set
            {
                if(_type == value) { return; }
                _type = value;
                OnAssignedType?.Invoke(_type);
            }
        }

        public Vector2Int Position
        {
            get => _position;
            set
            {
                _position = value;
                AdjustPosition();
            }
        }

        public float Elevation
        {
            get => _elevation;
            set
            {
                _elevation = value;
                AdjustPosition();
            }
        }
        
        private void AdjustPosition()
            => transform.localPosition = new Vector3(_position.x, _elevation, -_position.y);
    }
}
