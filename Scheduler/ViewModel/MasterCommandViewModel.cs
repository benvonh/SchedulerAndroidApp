using Microsoft.AspNetCore.SignalR.Client;
using Scheduler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class MasterCommandViewModel : BaseViewModel
    {
        private const string PASSWORD = "dev";
        public MasterCommandViewModel()
        {
            MessageLblIsVis = false;
            ResetBtnIsVis = false;
            DeleteBtnIsVis = false;
            ProceedBtnIsVis = false;
            OnConfirm = new Command(ConfirmPassword);
            OnReset = new Command(ResetData);
            OnDelete = new Command(DeleteSpecific);
            OnProceed = new Command(Proceed);

            Data.HUB.On<string>("UpdateClient", async serverResult => await DisplayMessage("Message", serverResult));
            Data.HUB.On<string[]>("ReturnUsers", serverResult => DisplayUsers(serverResult));
        }

        private string EntryTextValue;
        public string EntryText
        {
            get => EntryTextValue;
            set => SetProperty(ref EntryTextValue, value);
        }

        private bool MessageLblIsVisValue;
        public bool MessageLblIsVis
        {
            get => MessageLblIsVisValue;
            set => SetProperty(ref MessageLblIsVisValue, value);
        }

        private void ConfirmPassword()
        {
            if (EntryText == PASSWORD)
            {
                MessageLblIsVis = false;
                ResetBtnIsVis = true;
                DeleteBtnIsVis = true;
                ProceedBtnIsVis = true;
            }
            else
            {
                MessageLblIsVis = true;
                ResetBtnIsVis = false;
                DeleteBtnIsVis = false;
                ProceedBtnIsVis = false;
            }
        }
        public ICommand OnConfirm { get; }

        // Master Commands
        private async void ResetData()
        {
            try
            {
                await Data.HUB.InvokeAsync("ResetData");
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        public ICommand OnReset { get; }

        private bool ResetBtnIsVisValue;
        public bool ResetBtnIsVis
        {
            get => ResetBtnIsVisValue;
            set => SetProperty(ref ResetBtnIsVisValue, value);
        }

        private async void DeleteSpecific()
        {
            try
            {
                await Data.HUB.InvokeAsync("RequestUsers");
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        public ICommand OnDelete { get; }

        private bool DeleteBtnIsVisValue;
        public bool DeleteBtnIsVis
        {
            get => DeleteBtnIsVisValue;
            set => SetProperty(ref DeleteBtnIsVisValue, value);
        }

        private async void Proceed()
        {
            try
            {
                await Data.HUB.InvokeAsync("ProcessData");
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        public ICommand OnProceed { get; }

        private bool ProceedBtnIsVisValue;
        public bool ProceedBtnIsVis
        {
            get => ProceedBtnIsVisValue;
            set => SetProperty(ref ProceedBtnIsVisValue, value);
        }

        // Delete specific private helpers
        private async void DisplayUsers(string[] users)
        {
            try
            {
                string user = await DisplayActionSheet("Delete specific user", "Close", null, users);
                if (user != "Close") await Data.HUB.InvokeAsync("DeleteUser", user);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
