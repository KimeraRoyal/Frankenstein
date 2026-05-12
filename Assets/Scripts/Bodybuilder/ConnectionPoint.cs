using System;
using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    public class ConnectionPoint : MonoBehaviour
    {
        public enum ConnectionType
        {
            Extension,
            Connection
        }
        
        [SerializeField] private ConnectionType _type;

        private BodyPart _part;
        private Vector3 _offset;
        
        private ConnectionPoint _connectedTo;

        [SerializeField] private LayerMask _connectionPointMask;
        [SerializeField] private float _searchRadius = 1.0f;
        private readonly Collider[] _searchResults = new Collider[4];

        public ConnectionType Type => _type;

        public BodyPart Part => _part;

        public Vector3 Offset => transform.rotation * _offset;

        public ConnectionPoint ConnectedTo
        {
            get => _connectedTo;
            private set
            {
                _connectedTo = value;
                OnConnectedToPoint?.Invoke(_connectedTo);
            }
        }

        public Action<ConnectionPoint> OnConnectedToPoint;

        private void Awake()
        {
            _part = GetComponentInParent<BodyPart>();

            _offset = transform.position - _part.transform.position;
        }

        public bool Connect(ConnectionPoint other)
        {
            if(!CanConnectTo(other) || !other.CanConnectTo(this)) { return false; }
            ConnectedTo = other;
            other.ConnectedTo = this;
            return true;
        }

        public bool Disconnect()
        {
            if(!_connectedTo) { return false; }
            ConnectedTo.ConnectedTo = null;
            ConnectedTo = null;
            return true;
        }

        public ConnectionPoint FindClosestConnectionPoint(Vector3 position)
        {
            if (_type == ConnectionType.Extension || _connectedTo) { return null; }
            var count = Physics.OverlapSphereNonAlloc(position + Offset, _searchRadius, _searchResults, _connectionPointMask);
            if (count < 1) { return null; }
            
            ConnectionPoint result = null;
            var closestDistance = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                var otherPoint = _searchResults[i].GetComponent<ConnectionPoint>();
                if (!CanConnectTo(otherPoint)) { continue; }

                var distance = Vector3.Distance(position + Offset, otherPoint.transform.position);
                if(distance > closestDistance) { continue; }

                result = otherPoint;
                closestDistance = distance;
            }
            
            return result;
        }

        private bool CanConnectTo(ConnectionPoint other)
            => other && other._part != _part && other._type != _type && !other._connectedTo;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _type == ConnectionType.Connection ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}
