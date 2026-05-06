using System;
using UnityEngine;

namespace Util
{
    [Serializable]
    public class RangedCurve
    {
        [SerializeField] private float _min = 0.0f, _max = 1.0f;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

        public float Evaluate(float t)
            => Mathf.Lerp(_min, _max, _curve.Evaluate(t));
    }
}