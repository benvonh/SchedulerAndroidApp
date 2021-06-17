using Scheduler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class EditViewModel : BaseViewModel
    {
        private readonly int dayIndex;
        private readonly Day dayItem;

        public EditViewModel(int dayIndex, Day dayItem)
        {
            this.dayIndex = dayIndex;
            this.dayItem = dayItem;
            DateFancyStr = dayItem.Date.ToString("D") + "\n" + dayItem.DateStr;
            StartTimeValue = dayItem.StartTime;
            EndTimeValue = dayItem.EndTime;
            SaveChange = new Command(SaveChangeCommand);
            CancelChange = new Command(CancelChangeCommand);
        }

        public string DateFancyStr { get; }

        private TimeSpan StartTimeValue;
        public TimeSpan StartTimeTemp
        {
            get => StartTimeValue;
            set => SetProperty(ref StartTimeValue, value);
        }

        private TimeSpan EndTimeValue;
        public TimeSpan EndTimeTemp
        {
            get => EndTimeValue;
            set => SetProperty(ref EndTimeValue, value);
        }

        private async void SaveChangeCommand()
        {
            dayItem.StartTime = StartTimeValue;
            dayItem.EndTime = EndTimeValue;
            Data.DAYS[dayIndex] = dayItem;
            await OnNavigationBackAsync();
        }
        public ICommand SaveChange { get; }

        private async void CancelChangeCommand()
        {
            await OnNavigationBackAsync();
        }
        public ICommand CancelChange { get; }
    }
}
