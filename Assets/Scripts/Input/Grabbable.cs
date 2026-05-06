using UnityEngine;

namespace Input
{
    public interface Grabbable
    {
        public bool Grab();
        public void Release();

        public Vector2 Drag(Vector2 amount);
    }
}
