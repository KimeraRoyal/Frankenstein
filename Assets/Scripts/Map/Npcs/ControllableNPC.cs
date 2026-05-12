using Bodybuilder.Input;
using UnityEngine;

namespace Bodybuilder.Map.Npcs
{
    [RequireComponent(typeof(Npc))]
    public class ControllableNPC : Clickable
    {
        private Npc _npc;
        
        protected override void Click()
        {
            Debug.Log("Click NPC");
        }

        private void OnValidate()
        {
            _npc = GetComponent<Npc>();
        }
    }
}
