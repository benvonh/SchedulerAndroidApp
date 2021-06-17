using Microsoft.AspNetCore.SignalR.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scheduler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Data.Setup();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
