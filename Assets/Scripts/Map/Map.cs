using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size = new(20, 20);
        
        [SerializeField] private Layer _layerPrefab;

        [SerializeField] private LayerInfo[] _layerInfo;
        private Layer[] _layers;

        public Layer[] Layers => _layers;

        public UnityEvent<Layer> OnLayerBuilt;
        public UnityEvent OnBuilt;
        
        private void Start()
        {
            _layers = new Layer[_layerInfo.Length];
            for(var i = 0; i < _layerInfo.Length; i++)
            {
                _layers[i] = Instantiate(_layerPrefab, Vector3.up * _layerInfo[i].Elevation, Quaternion.identity, transform);
                _layers[i].Build(_layerInfo[i], _size);
                OnLayerBuilt?.Invoke(_layers[i]);
            }
            OnBuilt?.Invoke();
        }
    }
}
