using UnityEngine;

namespace Bodybuilder.Map.Npcs
{
    [CreateAssetMenu(fileName = "NPC", menuName = "Bodybuilding/NPC")]
    public class NpcInfo : ScriptableObject
    {
        [SerializeField] private int _minutesToMoveTile = 1;
        [SerializeField] private NpcProperty _properties;
        
        [SerializeField] private bool _isShovable = true;
        [SerializeField] private bool _canShove = false;

        public int MinutesToMoveTile
        {
            get => _minutesToMoveTile;
            set => _minutesToMoveTile = value;
        }

        public bool IsShovable => _isShovable;
        public bool CanShove => _canShove;
    }
}
