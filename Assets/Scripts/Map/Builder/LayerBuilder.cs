using Bodybuilding.Map.Tile;
using UnityEngine;

namespace Bodybuilder.Map.Builder
{
    public abstract class LayerBuilder : ScriptableObject
    {
        public abstract void BuildLayer(TileType[,] tiles);
    }
}