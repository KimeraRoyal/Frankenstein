using System;
using UnityEngine;

namespace Bodybuilder.Util
{
    [Serializable]
    public abstract class SnappedVector<T>
    {
        [SerializeField] private T _value;
        [SerializeField] private int _pixelsPerUnit = 100;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public T SnappedValue => GetSnappedValue();

        public int PixelsPerUnit
        {
            get => _pixelsPerUnit;
            set => _pixelsPerUnit = value;
        }

        protected SnappedVector() { }

        protected SnappedVector(T value, int pixelsPerUnit)
        {
            _value = value;
            _pixelsPerUnit = pixelsPerUnit;
        }

        protected abstract T GetSnappedValue();

        protected float Snap(float value)
            => Mathf.Round(value * _pixelsPerUnit) / _pixelsPerUnit;
    }
    
    [Serializable]
    public class SnappedFloat : SnappedVector<float>
    {
        public SnappedFloat() { }
        public SnappedFloat(float value, int pixelsPerUnit) : base(value, pixelsPerUnit) { }
        
        protected override float GetSnappedValue()
            => Snap(Value);
    }
    
    [Serializable]
    public class SnappedVector2 : SnappedVector<Vector2>
    {
        public SnappedVector2() { }
        public SnappedVector2(Vector2 value, int pixelsPerUnit) : base(value, pixelsPerUnit) { }
        public SnappedVector2(float x, float y, int pixelsPerUnit) : base(new Vector2(x, y), pixelsPerUnit) { }
        
        public static implicit operator SnappedVector2(SnappedFloat value) => new(value.Value, 0.0f, value.PixelsPerUnit);
        public static implicit operator SnappedVector2(SnappedVector3 value) => new(value.Value, value.PixelsPerUnit);
        public static implicit operator SnappedVector2(SnappedVector4 value) => new(value.Value, value.PixelsPerUnit);

        protected override Vector2 GetSnappedValue()
            => new(Snap(Value.x), Snap(Value.y));
    }
    
    [Serializable]
    public class SnappedVector3 : SnappedVector<Vector3>
    {
        public SnappedVector3() { }
        public SnappedVector3(Vector3 value, int pixelsPerUnit) : base(value, pixelsPerUnit) { }
        public SnappedVector3(float x, float y, float z, int pixelsPerUnit) : base(new Vector3(x, y, z), pixelsPerUnit) { }
        
        public static implicit operator SnappedVector3(SnappedFloat value) => new(value.Value, 0.0f, 0.0f, value.PixelsPerUnit);
        public static implicit operator SnappedVector3(SnappedVector2 value) => new(value.Value, value.PixelsPerUnit);
        public static implicit operator SnappedVector3(SnappedVector4 value) => new(value.Value, value.PixelsPerUnit);

        protected override Vector3 GetSnappedValue()
            => new(Snap(Value.x), Snap(Value.y), Snap(Value.z));
    }
    
    [Serializable]
    public class SnappedVector4 : SnappedVector<Vector4>
    {
        public SnappedVector4() { }
        public SnappedVector4(Vector3 value, int pixelsPerUnit) : base(value, pixelsPerUnit) { }
        public SnappedVector4(float x, float y, float z, float w, int pixelsPerUnit) : base(new Vector4(x, y, z, w), pixelsPerUnit) { }
        
        public static implicit operator SnappedVector4(SnappedFloat value) => new(value.Value, 0.0f, 0.0f, 0.0f, value.PixelsPerUnit);
        public static implicit operator SnappedVector4(SnappedVector2 value) => new(value.Value, value.PixelsPerUnit);
        public static implicit operator SnappedVector4(SnappedVector3 value) => new(value.Value, value.PixelsPerUnit);

        protected override Vector4 GetSnappedValue()
            => new(Snap(Value.x), Snap(Value.y), Snap(Value.z), Snap(Value.w));
    }
}
