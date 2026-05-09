using UnityEngine;

namespace Bodybuilder.Map.NPC
{
    public class NPCPosition : MonoBehaviour
    {
        private Map _map;
        private NPC _npc;

        private void Awake()
        {
            _map = FindAnyObjectByType<Map>();
            _map.OnBuilt.AddListener(MapBuilt);
            
            _npc = GetComponentInParent<NPC>();
            _npc.OnMove.AddListener(MoveTo);
        }

        private void MapBuilt()
        {
            MoveTo(_npc.Position);
        }

        private void MoveTo(Vector2Int tile)
        {
            transform.localPosition = _map.Layers[_npc.CurrentLayer].GetTilePosition(tile);
        }
    }
}
