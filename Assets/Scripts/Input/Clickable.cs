using UnityEngine;

namespace Bodybuilder.Input
{
    public abstract class Clickable : MonoBehaviour, Grabbable
    {
        public bool Grab()
        {
            Click();
            return false;
        }

        public void Release()
        {
            
        }

        public Vector3 Drag(Vector3 amount)
            => amount;

        protected abstract void Click();
    }
}
