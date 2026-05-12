using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilding.Map.Tiles
{
    public abstract class TileFeature : MonoBehaviour
    {
        private Tile _currentTile;

        public Tile CurrentTile => _currentTile;
        public Vector2Int Position => _currentTile.Position;
        public int CurrentLayer => _currentTile.LayerIndex;

        public UnityEvent<Vector2Int> OnMove;
        public UnityEvent<int> OnChangeLayer;

        public bool MoveToTile(Tile tile)
        {
            if (tile == _currentTile || tile == null || !tile.Type || !tile.AddFeature(this)) { return false; }

            _currentTile?.RemoveFeature(this);
            _currentTile = tile;
            OnMove?.Invoke(Position);
            OnChangeLayer?.Invoke(CurrentLayer);

            return true;
        }

        public bool Move(Direction direction, bool shove = false)
        {
            var tile = _currentTile.GetAdjacentTile(direction);
            if (tile == null) { return false; }
            
            if (tile.CurrentFeatureCount >= tile.MaxFeatureCount)
            {
                var shovableFeatures = tile.CurrentFeatures.Where(feature => feature.IsShovable());
                foreach (var feature in shovableFeatures)
                {
                    if(feature.Shove(direction)) { break; }
                }
            }
            return MoveToTile(_currentTile.GetAdjacentTile(direction));
        }

        public int Move(Direction direction, int distance, bool shove = false)
        {
            for (var i = 0; i < distance; i++)
            {
                if (!Move(direction, shove)) { return i; }
            }
            return distance;
        }
        
        public bool Shove(Direction from)
        {
            if (!IsShovable()) { return false; }

            var direction = from.Invert();
            for (var i = 0; i < 4; i++)
            {
                if(direction == from) { continue; }
                
                var tile = _currentTile.GetAdjacentTile(direction);
                if (MoveToTile(tile)) { return true; }
                direction.Rotate(1);
            }
            return false;
        }

        protected abstract bool IsShovable();

        public abstract string GetName();
    }
}