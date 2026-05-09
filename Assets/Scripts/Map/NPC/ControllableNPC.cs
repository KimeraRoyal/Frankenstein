using Bodybuilder.Input;
using UnityEngine;

namespace Bodybuilder.Map.NPC
{
    [RequireComponent(typeof(NPC))]
    public class ControllableNPC : Clickable
    {
        private NPC _npc;
        
        protected override void Click()
        {
            Debug.Log("Click NPC");
        }

        private void OnValidate()
        {
            _npc = GetComponent<NPC>();
        }
    }
}
