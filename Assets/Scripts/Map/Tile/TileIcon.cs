using Bodybuilder.Map;
using Bodybuilding.Map.Tile;
using UnityEngine;

namespace Bodybuilding
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TileIcon : MonoBehaviour
    {
        private static MaterialPropertyBlock _propertyBlock;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        [SerializeField] private Tile _parent;
        [SerializeField] private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer.enabled = false;
            _propertyBlock ??= new MaterialPropertyBlock();
            
            _parent.OnAssignedType.AddListener(UpdateType);
        }

        private void UpdateType(TileType type)
        {
            _renderer.enabled = type && type.Icon;
            if(!_renderer.enabled) { return; }
            
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetTexture(MainTex, type.Icon);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
