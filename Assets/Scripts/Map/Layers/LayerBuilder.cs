using Bodybuilding.Map.Tiles;
using UnityEngine;

namespace Bodybuilder.Map.Layers
{
    public abstract class LayerBuilder : ScriptableObject
    {
        public abstract void BuildLayer(Tile[,] tiles);
    }
}