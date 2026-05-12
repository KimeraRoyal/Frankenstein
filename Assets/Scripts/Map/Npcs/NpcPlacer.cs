using System;
using UnityEngine;

namespace Bodybuilder.Map.Npcs
{
    public class NpcPlacer : MonoBehaviour
    {
        [Serializable]
        public class NpcLayout
        {
            public NpcInfo _info;
            public Vector2Int _position;
        }

        [SerializeField] private Npc _npcPrefab;

        private Map _map;
        [SerializeField] private NpcLayout[] _layout;

        private void Awake()
        {
            _map = GetComponent<Map>();
            _map.OnBuilt.AddListener(MapBuilt);
        }

        private void MapBuilt()
        {
            var layer = _map.Layers[1];
            
            foreach (var npcLayout in _layout)
            {
                var npc = Instantiate(_npcPrefab, transform);
                npc.Info = npcLayout._info;
                npc.MoveToTile(layer.GetTile(npcLayout._position));
            }
        }
    }
}