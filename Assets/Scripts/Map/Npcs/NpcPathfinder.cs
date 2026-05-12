using Bodybuilder.Map.Npcs;
using Bodybuilder.Map.Pathfinder;
using UnityEngine;

namespace Bodybuilder
{
    [RequireComponent(typeof(Npc), typeof(NpcMovement))]
    public class NpcPathfinder : MonoBehaviour
    {
        private PathDrawer _pathDrawer;

        private Npc _npc;
        private NpcMovement _movement;

        private void Awake()
        {
            _pathDrawer = FindAnyObjectByType<PathDrawer>();
            
            _npc = GetComponent<Npc>();
            _movement = GetComponent<NpcMovement>();
            
            _npc.OnSelected.AddListener(CreatePath);
        }

        private void CreatePath()
        {
            _movement.SetPath(null);
            _pathDrawer.RequestPath(_npc.Position, path => _movement.SetPath(path));
        }
    }
}
