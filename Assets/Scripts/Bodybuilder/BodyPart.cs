using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    public class BodyPart : MonoBehaviour
    {
        private BuiltBody _body;

        [SerializeField] private PartInfo _info;
        
        private ConnectionPoint _connectionPoint;
        private ConnectionPoint[] _extensionPoints;
        private readonly List<ConnectionPoint> _childPoints = new();
        
        public BuiltBody Body => _body ? _body : Parent?.Body;

        public PartInfo Info => _info;

        public int TotalCost => _info.PointCost + _childPoints.Sum(child => child.Part.TotalCost);
        
        public float TotalWeight => _info.Weight + _childPoints.Sum(child => child.Part.TotalWeight);

        public PartFeatures TotalFeatures => _childPoints.Aggregate(_info.Features, (current, child) => current | child.Part.TotalFeatures);

        public int TotalFootCount => (_info.IsFoot ? 1 : 0) + _childPoints.Sum(child => child.Part.TotalFootCount);

        public float TotalMovementSpeed
        {
            get
            {
                var movementSpeed = 0.0f;
                if (TotalFootCount > 0)
                {
                    movementSpeed += _info.MovementSpeed + _childPoints.Sum(child => child.Part.TotalMovementSpeed) * _info.ValueMultiplier;
                }
                return movementSpeed;
            }
        }
        
        public int TotalHandCount => (_info.IsHand ? 1 : 0) + _childPoints.Sum(child => child.Part.TotalHandCount);
        public float TotalClimbingSpeed
        {
            get
            {
                var climbingSpeed = 0.0f;
                if (TotalHandCount > 0)
                {
                    climbingSpeed += _info.ClimbingSpeed + _childPoints.Sum(child => child.Part.TotalClimbingSpeed * _info.ValueMultiplier);
                }
                return climbingSpeed;
            }
        }

        public ConnectionPoint ConnectionPoint => _connectionPoint;
        public ConnectionPoint[] ExtensionPoints => _extensionPoints;
        public IReadOnlyList<ConnectionPoint> ChildPoints => _childPoints;
        
        public ConnectionPoint ConnectedTo => _connectionPoint?.ConnectedTo;
        public BodyPart Parent => ConnectedTo?.Part;

        public Vector3 ConnectionPointOffset => _connectionPoint ? _connectionPoint.Offset : Vector3.zero;

        private void Awake()
        {
            _body = GetComponent<BuiltBody>();
            
            var points = GetComponentsInChildren<ConnectionPoint>();

            _connectionPoint = points.FirstOrDefault(point => point.Type == ConnectionPoint.ConnectionType.Connection);
            _extensionPoints = points.Where(point => point.Type == ConnectionPoint.ConnectionType.Extension).ToArray();
            foreach (var extensionPoint in _extensionPoints)
            {
                extensionPoint.OnConnectedToPoint += _ => UpdateChildren();
            }
        }

        public bool Connect(ConnectionPoint point)
        {
            var body = point?.Part?.Body;
            if ((body && !body.CanAddPoints(_info.PointCost)) || !_connectionPoint || !_connectionPoint.Connect(point)) { return false; }
            transform.parent = point.transform;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            transform.localPosition = -ConnectionPointOffset;
            return true;
        }

        public bool Disconnect()
        {
            if (!_connectionPoint || !_connectionPoint.Disconnect()) { return false; }
            transform.parent = null;
            return true;
        }

        public ConnectionPoint FindClosestConnectionPoint()
            => FindClosestConnectionPoint(transform.position);

        public ConnectionPoint FindClosestConnectionPoint(Vector3 position)
            => _connectionPoint?.FindClosestConnectionPoint(position);

        private void UpdateChildren()
        {
            _childPoints.Clear();
            foreach (var extensionPoint in _extensionPoints)
            {
                if(extensionPoint.ConnectedTo == null) { continue; }
                _childPoints.Add(extensionPoint.ConnectedTo);
            }
            Body?.UpdateStatistics();
        }
    }
}
