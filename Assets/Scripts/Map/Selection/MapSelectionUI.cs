using System;
using Bodybuilding.Map.Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Terrain = Bodybuilding.Map.Tiles.Terrain;

namespace Bodybuilder.Map.Selection
{
    public class MapSelectionUI : MonoBehaviour
    {
        private enum Mode
        {
            Hovered,
            Selected
        }

        private MapSelection _selection;

        private LayoutElement _layoutElement;
        private CanvasGroup _canvasGroup;

        [SerializeField] private Mode _mode;
        
        [SerializeField] private TMP_Text _tileText;
        [SerializeField] private TMP_Text _featureText;
        
        [SerializeField] [TextArea(3, 5)] private string _format = "{0}\nL{1} (X: {2}, Y: {3})\nMOV: {4}\n{5}";
        
        private void Awake()
        {
            _selection = FindAnyObjectByType<MapSelection>();
            
            _selection.OnTileHovered += TileHovered;
            _selection.OnTileFeaturesHovered.AddListener(FeaturesHovered);
            
            _selection.OnTileSelected += TileSelected;
            _selection.OnTileFeaturesSelected.AddListener(FeaturesSelected);

            _layoutElement = GetComponent<LayoutElement>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            FormatTile(null);
        }

        private void TileHovered(Tile tile)
        {
            if(_mode != Mode.Hovered) { return; }
            FormatTile(tile);
        }

        private void FeaturesHovered(TileFeature[] features)
        {
            if(_mode != Mode.Hovered) { return; }
            FormatFeature(features is { Length: > 0 } ? features[0] : null);
        }

        private bool TileSelected(Tile tile)
        {
            if(_mode == Mode.Selected) { FormatTile(tile); }
            return true;
        }

        private void FeaturesSelected(TileFeature[] features)
        {
            if(_mode != Mode.Selected) { return; }
            FormatFeature(features is { Length: > 0 } ? features[0] : null);
        }

        private void FormatTile(Tile tile)
        {
            var hasTile = tile is { LayerIndex: >= 0 } && tile.Type;
            
            _layoutElement.ignoreLayout = !hasTile;
            _canvasGroup.alpha = hasTile ? 1.0f : 0.0f;
            if(!hasTile) { return; }
            var properties = "";
            if (tile.Type.Terrain.HasFlag(Terrain.Water))
            {
                properties += "Water";
            }
            if (tile.Type.Terrain.HasFlag(Terrain.Hill))
            {
                if (properties.Length > 0) { properties += ", "; }
                properties += "Hill";
            }
            if (tile.Type.Terrain.HasFlag(Terrain.Wall))
            {
                if (properties.Length > 0) { properties += ", "; }
                properties += "Wall";
            }
            _tileText.text = string.Format(_format, tile.Type.name, tile.LayerIndex, tile.Position.x, tile.Position.y, tile.Type.MovementPenalty, properties);
        }

        private void FormatFeature(TileFeature feature)
        {
            var text = "";
            if (feature != null)
            {
                text = feature.GetName();
            }
            _featureText.text = text;
        }
    }
}
