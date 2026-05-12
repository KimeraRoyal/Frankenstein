using Bodybuilder.Map.Selection;
using Bodybuilder.Map.Selection.Selectables;
using Bodybuilding.Map.Tiles;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map.Layers
{
    public class Layer : MonoBehaviour, IMapSelectable
    {
        private LayerInfo _info;
        
        [SerializeField] private Vector2Int _size;
        private Vector3 _offset;
        private Vector3 _topLeft;

        private int _index;
        private Tile[,] _tiles;

        public LayerInfo Info => _info;

        public Vector2Int Size => _size;

        public float Elevation => _info.Elevation;

        public Vector3 Offset => _offset;

        public int Index => _index;

        public Tile[,] Tiles => _tiles;

        public UnityEvent<Tile[,]> OnBuilt;

        public void Build(int index, LayerInfo info, Vector2Int size)
        {
            _index = index;
            _info = info;
            
            _size = size;
            _offset = new Vector3(_size.x, 0, _size.y) / -2.0f;
            
            _tiles = new Tile[_size.y, _size.x];
            var position = Vector2Int.zero;
            for (position.y = 0; position.y < _size.y; position.y++)
            {
                for (position.x = 0; position.x < _size.x; position.x++)
                {
                    _tiles[position.y, position.x] = new Tile(null, _index, position);
                    _tiles[position.y, position.x].FindAdjacentTile += GetAdjacentTile;
                }
            }
            
            info.Build(_tiles);
            _topLeft = GetTilePosition(Vector2Int.zero);

            transform.localPosition = Vector3.up * Elevation;
            OnBuilt?.Invoke(_tiles);
        }

        public Tile GetAdjacentTile(Tile tile, Direction direction)
            => GetTile(tile.Position + direction.ToVector(), false);

        public Tile GetTile(Vector2Int index, bool clamp = false)
        {
            if (clamp)
            {
                index.x = Mathf.Clamp(index.x, 0, _size.x - 1);
                index.y = Mathf.Clamp(index.y, 0, _size.y - 1);
            }
            else if (index.x < 0 || index.y < 0 || index.x >= _size.x || index.y >= _size.y)
            {
                return null;
            }
            return _tiles[index.y, index.x];
        }

        public Tile GetTileAt(Vector3 position, bool clamp = false)
        {
            var relativePosition = position - _topLeft;
            
            var tile = new Vector2Int(Mathf.RoundToInt(relativePosition.x), Mathf.RoundToInt(relativePosition.z));
            return GetTile(tile, clamp);
        }
        
        public Vector3 GetTilePosition(Vector2Int tile)
        {
            tile.x = Mathf.Clamp(tile.x, 0, _size.x - 1);
            tile.y = Mathf.Clamp(tile.y, 0, _size.y - 1);
            return new Vector3(_tiles[tile.y, tile.x].Position.x, GetTileElevation(tile), _tiles[tile.y, tile.x].Position.y) + Offset;
        }

        public float GetTileElevation(Vector2Int tile)
            => _tiles[tile.y, tile.x].Elevation + Elevation;

        public void Select() { }

        public IMapSelectable GetLayer()
            => this;
    }
}
