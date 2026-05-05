using UnityEngine;

namespace Bodybuilding.Map.Tile
{   
    [CreateAssetMenu(fileName = "Tile Type", menuName = "Bodybuilding/Tile Type")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private Color _color = Color.white;

        public Color Color => _color;
    }
}
