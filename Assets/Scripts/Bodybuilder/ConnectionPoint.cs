using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    public class ConnectionPoint : MonoBehaviour
    {
        private ConnectionPoint connectedTo;

        public void Connect(ConnectionPoint other)
        {
            if(connectedTo) { return; }
            connectedTo = other;
            // TODO: Move to piece
        }

        public void Disconnect()
        {
            if(!connectedTo) { return; }
            connectedTo = null;
        }
    }
}
