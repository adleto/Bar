using Bar.Mobile.Service;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bar.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            if (Preferences.ContainsKey("username"))
            {
                APIService.Token = Preferences.Get("token", "");
                APIService.ApiUrl = Preferences.Get("serverUrl", "");
            }
            DependencyService.Register<LocalService>();
            MainPage = new Views.MainPage();
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
