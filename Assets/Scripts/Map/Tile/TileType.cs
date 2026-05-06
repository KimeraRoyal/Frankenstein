using UnityEngine;

namespace Bodybuilding.Map.Tile
{   
    [CreateAssetMenu(fileName = "Tile Type", menuName = "Bodybuilding/Tile Type")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Texture2D _icon;

        public Color Color => _color;
        public Texture2D Icon => _icon;
    }
}
