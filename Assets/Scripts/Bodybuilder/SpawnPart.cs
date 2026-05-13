using System;
using Bodybuilder.Bodybuilder;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bodybuilder
{
    public class SpawnPart : MonoBehaviour
    {
        private Button _button;

        private int _index;

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private string _costFormat = "{0}";

        [SerializeField] private PartInfo _part;
        [SerializeField] private Vector3 _spawnPosition;

        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public PartInfo Part
        {
            get => _part;
            set
            {
                _part = value;
                _image.sprite = _part.Icon;
                _cost.text = string.Format(_costFormat, _part.PointCost);
            }
        }

        public Action<int> OnPartSpawned;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            Instantiate(_part.PartPrefab, _spawnPosition, Quaternion.identity);
            OnPartSpawned?.Invoke(Index);
        }
    }
}
