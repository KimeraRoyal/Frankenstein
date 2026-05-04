using UnityEngine;

namespace Bodybuilder
{
    public class Float : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField] private float _amount = 1.0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rigidbody.AddForce(Vector3.up * _amount, ForceMode.Acceleration);
        }
    }
}
