using UnityEngine;

namespace Bodybuilding.Map.Tile
{   
    [CreateAssetMenu(fileName = "Tile Type", menuName = "Bodybuilding/Tile Type")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Material _iconMaterial;

        public Color Color => _color;
        public Material IconMaterial => _iconMaterial;
    }
}
