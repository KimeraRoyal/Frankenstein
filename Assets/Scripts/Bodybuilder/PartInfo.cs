using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    [CreateAssetMenu(fileName = "Body Part Info", menuName = "Bodybuilding/Body Part Info")]
    public class PartInfo : ScriptableObject
    {
        [SerializeField] private float _weight = 1.0f;

        public float Weight => _weight;
    }
}