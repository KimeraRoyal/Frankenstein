using UnityEngine;

namespace Bodybuilding.Map.Tiles
{   
    [CreateAssetMenu(fileName = "Tile Type", menuName = "Bodybuilding/Tile Type")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Material _iconMaterial;

        [SerializeField] private int _maxFeatures = 1;

        [SerializeField] private Terrain _terrain;
        [SerializeField] private int _movementPenalty;

        public Color Color => _color;
        public Material IconMaterial => _iconMaterial;

        public int MaxFeatures => _maxFeatures;

        public Terrain Terrain => _terrain;
        public int MovementPenalty => _movementPenalty;
    }
}
