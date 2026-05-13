using Bodybuilder.Bodybuilder;
using TMPro;
using UnityEngine;

namespace Bodybuilder
{
    public class BodyUI : MonoBehaviour
    {
        private TMP_Text _text;
        private BuiltBody _body;

        [SerializeField] [TextArea(3, 5)] private string _format = "{0}";

        [SerializeField] private string _footFormat = "{0}";
        [SerializeField] private string _noFeet = "No Feet";

        [SerializeField] private string _handFormat = "{0}";
        [SerializeField] private string _noHands = "No Hands";

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            
            _body = FindAnyObjectByType<BuiltBody>();
            _body.OnStatisticsUpdated += StatisticsUpdated;
        }

        private void StatisticsUpdated()
        {
            var footString = _body.HasFeet ? string.Format(_footFormat, _body.FootCount, _body.MovementSpeed) : _noFeet;
            var handString = _body.HasHands ? string.Format(_handFormat, _body.HandCount, _body.ClimbingSpeed) : _noHands;

            var featureString = "";
            if (_body.Features.HasFlag(PartFeatures.Walking))
            {
                featureString += "Walking";
            }
            if (_body.Features.HasFlag(PartFeatures.Grabbing))
            {
                if (featureString.Length > 0) { featureString += ", "; }
                featureString += "Grabbing";
            }
            if (_body.Features.HasFlag(PartFeatures.Climbing))
            {
                if (featureString.Length > 0) { featureString += ", "; }
                featureString += "Climbing";
            }
            if (_body.Features.HasFlag(PartFeatures.Head))
            {
                if (featureString.Length > 0) { featureString += ", "; }
                featureString += "Head";
            }
            if (_body.Features.HasFlag(PartFeatures.Swimming))
            {
                if (featureString.Length > 0) { featureString += ", "; }
                featureString += "Swimming";
            }
            if (_body.Features.HasFlag(PartFeatures.Flying))
            {
                if (featureString.Length > 0) { featureString += ", "; }
                featureString += "Flight";
            }
            
            _text.text = string.Format(_format, _body.Cost, _body.Weight, footString, handString, featureString);
        }
    }
}
