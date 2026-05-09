using UnityEngine;

namespace Bodybuilder.Input
{
    public class Draggable : MonoBehaviour, Grabbable
    {
        public bool Grab() { return true; }

        public void Release() { }

        public Vector2 Drag(Vector2 amount)
        {
            transform.localPosition += (Vector3) amount;
            return amount;
        }
    }
}
