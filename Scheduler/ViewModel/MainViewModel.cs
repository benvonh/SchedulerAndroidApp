using Microsoft.AspNetCore.SignalR.Client;
using Scheduler.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Reconnect = new Command(ReconnectCommand);
            UpdateListView();
            ReconnectCommand();
        }

        private string NameValue;
        public string Name
        {
            get => NameValue;
            set => SetProperty(ref NameValue, value);
        }

        private async void ReconnectCommand()
        {
            try
            {
                if (Data.HUB != null) await Data.HUB.DisposeAsync();
                Data.HUB = new HubConnectionBuilder().WithUrl(Data.Address).Build();
                Data.HUB.On<string>("UpdateClient", async serverResult => await DisplayMessage("Message", serverResult));
                await Data.HUB.StartAsync();
                await Data.HUB.InvokeAsync("CheckUp");
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        public ICommand Reconnect { get; }

        private ObservableCollection<Day> DayListValue;
        public ObservableCollection<Day> DayList
        {
            get => DayListValue;
            set => SetProperty(ref DayListValue, value);
        }

        // External commands
        public void UpdateListView()
        {
            DayList = new ObservableCollection<Day>(Data.DAYS);
        }

        public async void ItemTapped(ItemTappedEventArgs e)
        {
            await OnNavigationForwardAsync(new NavigationPage(new EditPage(e.ItemIndex, (Day)e.Item)));
        }

        public async void SaveAvailability()
        {
            await Data.Save();
            await DisplayMessage("Saved", "Availability times has been saved. These times will now load automatically when re-opening the app.");
        }

        public void ResetAvailability()
        {
            Data.CreateNew();
            UpdateListView();
        }

        private const string confirmMsg = "Yes, send my availability";
        private const string cancelMsg = "No, do not send";
        public async void SendData()
        {
            try
            {
                await DisplayMessage("Notification", "Are you sure you wish to send this data?\n\n" +
                    "Once sent, your availability cannot be re-uploaded." +
                    "If you do wish to re-upload your avilability later, please contact admin.",
                    "Next");
                string response = await DisplayActionSheet("Confirmation", "Close", null, confirmMsg, cancelMsg);
                if (response == "Close" || response == cancelMsg) return;
                if (Name == null || Name == string.Empty) throw new Exception("Please enter a valid name.");
                if (Data.DAYS[0].Date.Date < DateTime.Now.Date) throw new Exception("Your availability dates must start from today.\n" +
                    "Please reset it from the options menu.");
                List<DayConvertible> days_convert = new List<DayConvertible>();
                foreach (Day day in Data.DAYS)
                {
                    days_convert.Add(new DayConvertible(day));
                }
                await Data.HUB.InvokeAsync("UploadData", days_convert, Name);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public async void OpenMasterCommandPage()
        {
            await OnNavigationForwardAsync(new NavigationPage(new MasterCommandPage()));
        }
    }
}
