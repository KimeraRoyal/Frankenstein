using UnityEngine;

namespace Bodybuilder.Input
{
    public interface Grabbable
    {
        public bool Grab();
        public void Release();

        public Vector3 Drag(Vector3 amount);
        public void Rotate(Vector3 amount);
    }
}
