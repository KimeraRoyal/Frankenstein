using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bodybuilder.Bodybuilder
{
    public class Inventory : MonoBehaviour
    {
        private HorizontalLayoutGroup[] _rows;
        [SerializeField] private int _buttonsPerRow = 4;
        [SerializeField] private int _rowCount = 3;

        [SerializeField] private SpawnPart _buttonPrefab;
        private readonly List<SpawnPart> _buttons = new();

        [SerializeField] private List<PartInfo> _inventory;

        [SerializeField] private PartInfo[] _parts;

        private void Awake()
        {
            _rows = GetComponentsInChildren<HorizontalLayoutGroup>();
        }

        private void Start()
        {
            UpdateRows();
        }

        private void UpdateRows()
        {
            if (_inventory.Count < 1)
            {
                for (var i = 0; i < _buttonsPerRow * _rowCount; i++)
                {
                    _inventory.Add(_parts[Random.Range(0, _parts.Length)]);
                }
            }
            
            for (var i = 0; i < _inventory.Count; i++)
            {
                var row = i / _buttonsPerRow;
                var button = GetButton(i);
                button.Index = i;
                button.Part = _inventory[i];
                button.transform.parent = _rows[row].transform;
            }

            for (var i = _inventory.Count; i < _buttons.Count; i++)
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }

        private SpawnPart GetButton(int index)
        {
            SpawnPart button;
            if (index >= _buttons.Count)
            {
                button = Instantiate(_buttonPrefab, transform);
                button.OnPartSpawned += OnPartSpawned;
                _buttons.Add(button);
            }
            else
            {
                button = _buttons[index];
                button.gameObject.SetActive(true);
            }
            return button;
        }

        private void OnPartSpawned(int partIndex)
        {
            _inventory.RemoveAt(partIndex);
            UpdateRows();
        }
    }
}