using UnityEngine;

namespace Bodybuilder
{
    [ExecuteInEditMode]
    public class LockRotation : MonoBehaviour
    {
        private void Update()
        {
            var eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0.0f;
            transform.eulerAngles = eulerAngles;
        }
    }
}
