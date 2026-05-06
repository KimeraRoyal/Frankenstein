using TMPro;
using UnityEngine;

namespace Bodybuilder
{
    [RequireComponent(typeof(TMP_Text))]
    public class SpeedLabel : MonoBehaviour
    {
        private TMP_Text _text;
        
        private Clock _clock;

        [SerializeField] [TextArea(1, 5)] private string _format = "{0}";

        private void Awake()
        {
            _clock = FindAnyObjectByType<Clock>();
            _clock.OnSpeedChanged.AddListener(SpeedChanged);
        }

        private void SpeedChanged(float speed)
        {
            _text.text = string.Format(_format, speed);
        }

        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
        }
    }
}