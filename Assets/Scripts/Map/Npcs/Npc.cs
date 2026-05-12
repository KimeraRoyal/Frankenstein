using Bodybuilder.Map.Selection.Selectables;
using Bodybuilding.Map.Tiles;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map.Npcs
{
    public class Npc : TileFeature, IMapSelectable
    {
        private Map _map;
        
        [SerializeField] private NpcInfo _info;

        public NpcInfo Info { get => _info; set => _info = value; }

        public UnityEvent OnSelected;

        private void Awake()
        {
            _map = GetComponent<Map>();
        }

        protected override bool IsShovable()
            => _info.IsShovable;

        public void Select()
        {
            OnSelected?.Invoke();
        }

        public IMapSelectable GetLayer()
            => _map.Layers[CurrentLayer];

        public override string GetName()
            => _info.name;
    }
}
