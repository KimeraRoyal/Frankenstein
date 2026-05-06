using UnityEngine;

namespace Input
{
    public class Draggable : MonoBehaviour, Grabbable
    {
        public bool Grab() { return true; }

        public void Release() { }

        public Vector2 Drag(Vector2 amount)
        {
            transform.localPosition += (Vector3) amount;
            Debug.Log($"Draggable Delta: {amount}");
            return amount;
        }
    }
}
