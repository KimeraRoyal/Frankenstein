using UnityEngine;

namespace Bodybuilder.Map.NPC
{
    [CreateAssetMenu(fileName = "NPC", menuName = "Bodybuilding/NPC")]
    public class NPCInfo : ScriptableObject
    {
        [SerializeField] private int _minutesToMoveTile = 1;

        public int MinutesToMoveTile
        {
            get => _minutesToMoveTile;
            set => _minutesToMoveTile = value;
        }
    }
}
