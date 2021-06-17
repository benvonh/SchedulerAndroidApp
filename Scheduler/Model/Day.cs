using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Model
{
    public class Day
    {
        public Day() { }

        public Day(DateTime date)
        {
            Date = date;
            StartTime = new TimeSpan(0, 0, 0);
            EndTime = new TimeSpan(0, 0, 0);
        }

        public Day(DayConvertible day_convert)
        {
            Date = new DateTime(day_convert.Date);
            StartTime = new TimeSpan(day_convert.StartTime);
            EndTime = new TimeSpan(day_convert.EndTime);
        }

        // Properties
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // String getters
        public string DateStr
        {
            get => Date.ToString(@"dd/MM");
        }
        public string StartTimeStr
        {
            get => StartTime.ToString(@"h\:mm");
        }
        public string EndTimeStr
        {
            get => EndTime.ToString(@"h\:mm");
        }
    }

    public class DayConvertible
    {
        public DayConvertible() { }

        public DayConvertible(Day day)
        {
            Date = day.Date.Ticks;
            StartTime = day.StartTime.Ticks;
            EndTime = day.EndTime.Ticks;
        }

        public long Date { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
    }
}
