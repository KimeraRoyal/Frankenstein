using System;
using UnityEngine;

namespace Bodybuilder
{
    [Serializable]
    public struct TimePeriod
    {
        public const int MinutesInHour = 60;
        public const int HoursInDay = 24;

        [SerializeField] private int _days;
        [SerializeField] [Range(0, HoursInDay - 1)] private int _hours;
        [SerializeField] [Range(0, MinutesInHour - 1)] private int _minutes;
        
        public int Days
        {
            get => _days;
            set
            {
                if (value < 0)
                {
                    value = 0;
                    Minutes = 0;
                    Hours = 0;
                }
                _days = value;
            }
        }
        
        public int Hours
        {
            get => _hours;
            set
            {
                if (value < 0)
                {
                    Days += Mathf.Abs(value) / HoursInDay - 1;
                    value = HoursInDay + value % HoursInDay;
                }
                else if (value >= HoursInDay)
                {
                    Days += value / HoursInDay;
                    value %= HoursInDay;
                }
                _hours = value;
            }
        }
        
        public int Minutes
        {
            get => _minutes;
            set
            {
                if (value < 0)
                {
                    Hours += Mathf.Abs(value) / MinutesInHour - 1;
                    value = MinutesInHour + value % MinutesInHour;
                }
                else if (value >= MinutesInHour)
                {
                    Hours += value / MinutesInHour;
                    value %= MinutesInHour;
                }
                _minutes = value;
            }
        }

        public TimePeriod(int days, int hours, int minutes)
        {
            _minutes = 0;
            _hours = 0;
            _days = 0;

            Days += days;
            Hours += hours;
            Minutes += minutes;
        }

        public static TimePeriod operator +(TimePeriod a, TimePeriod b)
            => new(a.Days + b.Days, a.Hours + b.Hours, a.Minutes + b.Minutes);

        public static TimePeriod operator -(TimePeriod a, TimePeriod b)
            => new(a.Days - b.Days, a.Hours - b.Hours, a.Minutes - b.Minutes);

        public static TimePeriod operator *(TimePeriod a, int b)
            => new(a.Days * b, a.Hours * b, a.Minutes * b);

        public static TimePeriod operator /(TimePeriod a, int b)
            => new(a.Days / b, a.Hours / b, a.Minutes / b);

        public override string ToString()
            => $"{_days}D:{_hours}H:{_minutes}:M";
    }
}