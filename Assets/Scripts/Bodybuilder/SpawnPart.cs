using Bodybuilder.Bodybuilder;
using UnityEngine;
using UnityEngine.UI;

namespace Bodybuilder
{
    public class SpawnPart : MonoBehaviour
    {
        private Button _button;

        [SerializeField] private BodyPart _part;
        [SerializeField] private Vector3 _spawnPosition;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            Instantiate(_part, _spawnPosition, Quaternion.identity);
        }
    }
}
