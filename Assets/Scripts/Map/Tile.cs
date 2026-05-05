using Bodybuilding.Map.Tile;
using UnityEngine;

namespace Bodybuilder.Map
{
    public class Tile : MonoBehaviour
    {
        private TileType _type;

        [SerializeField] [HideInInspector] private MeshRenderer _mesh;
        
        private Vector2Int _position;
        private float _elevation;

        public TileType Type
        {
            get => _type;
            set
            {
                if(_type == value) { return; }
                _type = value;
                SetMeshColor();
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

        private void SetMeshColor()
        {
            var propertyBlock = new MaterialPropertyBlock();
            _mesh.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_BaseColor", _type.Color);
            _mesh.SetPropertyBlock(propertyBlock);
        }

        private void OnValidate()
        {
            _mesh = GetComponentInChildren<MeshRenderer>();
        }
    }
}
