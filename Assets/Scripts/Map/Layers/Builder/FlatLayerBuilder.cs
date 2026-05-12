using Bodybuilding.Map.Tiles;
using UnityEngine;

namespace Bodybuilder.Map.Layers.Builder
{
    [CreateAssetMenu(fileName = "Flat Layer Builder", menuName = "Bodybuilding/Flat Layer Builder")]
    public class FlatLayerBuilder : LayerBuilder
    {
        [SerializeField] private TileType _tile;
        
        public override void BuildLayer(Tile[,] tiles)
        {
            var size = new Vector2Int(tiles.GetLength(0), tiles.GetLength(1));
            var position = Vector2Int.zero;
            for (position.y = 0; position.y < size.y; position.y++)
            {
                for (position.x = 0; position.x < size.x; position.x++)
                {
                    tiles[position.y, position.x].Type = _tile;
                }
            }
        }
    }
}