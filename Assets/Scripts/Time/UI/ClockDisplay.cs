using TMPro;
using UnityEngine;

namespace Bodybuilder.Time.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ClockDisplay : MonoBehaviour
    {
        private TMP_Text _text;
        
        private Clock _clock;

        [SerializeField] [TextArea(1, 5)] private string _format = "{0}{1}{2}";
        
        private void Awake()
        {
            _clock = FindAnyObjectByType<Clock>();
            _clock.OnIncrement.AddListener(IncrementClock);
        }

        private void IncrementClock(TimePeriod period)
        {
            _text.text = string.Format(_format, period.Days, period.Hours, period.Minutes);
        }

        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
        }
    }
}