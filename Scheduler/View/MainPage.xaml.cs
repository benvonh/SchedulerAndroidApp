using Scheduler.View;
using Scheduler.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler
{
    [DesignTimeVisible(true)]
    public partial class MainPage : BasePage
    {
        private readonly MainViewModel vm;

        public MainPage()
        {
            InitializeComponent();
            vm = new MainViewModel();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.UpdateListView();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            vm.ItemTapped(e);
        }

        private void OnSave(object sender, EventArgs e)
        {
            vm.SaveAvailability();
        }

        private async void OnReset(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Reset Availability", "Are you sure you wish to reset your availability (00:00, 00:00)? Unsaved data will be lost.", "No", "Yes");
            if (!response) vm.ResetAvailability();
        }

        private void OnSend(object sender, EventArgs e)
        {
            vm.SendData();
        }

        private void OnMasterCommand(object sender, EventArgs e)
        {
            vm.OpenMasterCommandPage();
        }
    }
}
