using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilding.Map.Tile
{
    public class Tile
    {
        private TileType _type;

        private Vector2Int _position;
        private float _elevation;

        public TileType Type
        {
            get => _type;
            set => _type = value;
        }

        public Vector2Int Position
        {
            get => _position;
            set => _position = value;
        }

        public float Elevation
        {
            get => _elevation;
            set => _elevation = value;
        }

        public Tile(TileType type, Vector2Int position, float elevation = 0.0f)
        {
            _type = type;
            _position = position;
            _elevation = elevation;
        }
    }
}
