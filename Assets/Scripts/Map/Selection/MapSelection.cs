using System;
using System.Linq;
using Bodybuilder.Input;
using Bodybuilder.Map.Layers;
using Bodybuilder.Map.Selection.Selectables;
using Bodybuilding.Map.Tiles;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map.Selection
{
    public class MapSelection : MonoBehaviour
    {
        private const int RaycastIterations = 4;
        
        private Mouse _mouse;
        private Camera _camera;
        
        [SerializeField] private LayerMask _mapMask;
        private readonly RaycastHit[] _raycastResults = new RaycastHit[RaycastIterations];
        private Vector2Int _lastTestedPosition;

        private Tile _hoveredTile;
        private Tile _selectedTile;

        public Tile HoveredTile => _hoveredTile;
        public Tile SelectedTile => _selectedTile;

        public Action<Tile> OnTileHovered;
        public Func<Tile, bool> OnTileSelected;

        public UnityEvent<TileFeature[]> OnTileFeaturesHovered;
        public UnityEvent<TileFeature[]> OnTileFeaturesSelected;

        private void Awake()
        {
            _mouse = FindAnyObjectByType<Mouse>();
            _camera = _mouse.GetComponent<Camera>();
            
            _mouse.OnMoved += MouseMoved;
            var lmb = _mouse.Buttons[0];
            lmb.OnPressed += MousePressed;
        }
        
        private void MouseMoved(Vector2 screenPos, Vector2 delta)
        {
            var tile = TestForTile(_camera.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, _camera.farClipPlane)));
            if(tile == _hoveredTile) { return; }

            _hoveredTile = tile;
            OnTileHovered?.Invoke(_hoveredTile);

            TileFeature[] selectableFeatures = null;
            if (_hoveredTile != null)
            {
                selectableFeatures = _hoveredTile.CurrentFeatures.Where(feature => feature is IMapSelectable).ToArray();
            }
            OnTileFeaturesHovered?.Invoke(selectableFeatures);
        }

        private void MousePressed(Vector2 screenPos)
        {
            var canSelect = true;
            if (OnTileSelected != null)
            {
                canSelect = OnTileSelected.Invoke(_hoveredTile);
            }
            if(!canSelect) { return; }
            _selectedTile = _hoveredTile;

            TileFeature[] selectableFeatures = null;
            if (_selectedTile != null)
            {
                selectableFeatures = _hoveredTile.CurrentFeatures.Where(feature => feature is IMapSelectable).ToArray();
                foreach (var feature in selectableFeatures)
                {
                    var selectable = feature as IMapSelectable;
                    selectable?.Select();
                }
            }
            OnTileFeaturesSelected?.Invoke(selectableFeatures);
        }

        private Tile TestForTile(Ray ray)
        {
            var size = Physics.RaycastNonAlloc(ray, _raycastResults, _camera.farClipPlane, _mapMask);
            if (size < 1)
            { 
                return null;
            }

            var tile = _hoveredTile;
            
            MapSelectableCollider closestElement = null;
            Layer closestElementLayer = null;
            var closestPoint = Vector3.zero;
            var closestDistance = float.MaxValue;
            for (var i = 0; i < size; i++)
            {
                var selectable = _raycastResults[i].collider.GetComponent<MapSelectableCollider>();
                var selectableLayer = selectable.Selectable as Layer;
                
                if(selectable == null || !selectableLayer || !selectableLayer.Info.Selectable || _raycastResults[i].distance > closestDistance) { continue; }

                closestElement = selectable;
                closestElementLayer = selectableLayer;
                closestPoint = _raycastResults[i].point;
                closestDistance = _raycastResults[i].distance;
            }
            if (closestElement != null) { tile = closestElementLayer.GetTileAt(closestPoint); }
            return tile;
        }
    }
}
