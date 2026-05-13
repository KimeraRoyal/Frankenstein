using System;
using UnityEngine;

namespace Bodybuilder
{
    [ExecuteInEditMode]
    public class LockPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 _lockedPosition;
        
        private void Awake()
        {
            _lockedPosition = transform.position;
        }

        private void Update()
        {
            transform.position = _lockedPosition;
        }
    }
}
