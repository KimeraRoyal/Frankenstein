using System;
using Bodybuilding;
using UnityEngine;

namespace Bodybuilder
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private TileType _groundType;

        private void Start()
        {
            var layerTiles = new [,]
            {
                { null, _groundType, _groundType },
                { _groundType, _groundType, _groundType },
                { _groundType, null, null }
            };
            BuildLayer(layerTiles);
        }

        public void BuildLayer(TileType[,] layerTiles)
        {
            var tileString = "";
            for (var y = 0; y < layerTiles.GetLength(0); y++)
            {
                for (var x = 0; x < layerTiles.GetLength(1); x++)
                {
                    if (x > 0) { tileString += " "; }
                    tileString += layerTiles[y, x] ? layerTiles[y, x].name : "Air";
                }
                tileString += "\n";
            }
            Debug.Log(tileString);
        }
    }
}
