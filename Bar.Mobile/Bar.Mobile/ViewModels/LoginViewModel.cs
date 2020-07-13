using Bar.Mobile.Service;
using Bar.Models;
using Bar.Models.Account;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bar.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly APIService _service = null;

        private string serverUrl = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;

        public LoginViewModel()
        {
            _service = new APIService("Auth/AuthTest");
        }
        public string ServerUrl
        {
            get
            {
                return serverUrl;
            }
            set
            {
                serverUrl = value;
                OnPropertyChanged();
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadData()
        {
            try {
                if(Preferences.ContainsKey("username") &&
                    Preferences.ContainsKey("serverUrl"))
                {
                    Username = Preferences.Get("username", "");
                    ServerUrl = Preferences.Get("serverUrl", "");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task Login()
        {
            if ((!string.IsNullOrEmpty(Username)) && !string.IsNullOrEmpty(Password))
            {
                APIService.ApiUrl = ServerUrl;
                var upsertUser = new ApplicationUserGetRequestModel
                {
                    Password = this.Password,
                    Username = this.Username
                };
                try
                {
                    var result = await _service.Insert<TokenModel>(upsertUser);
                    if (result != null)
                    {
                        Preferences.Set("serverUrl", ServerUrl);
                        Preferences.Set("username", Username);
                        Preferences.Set("token", result.Token);
                        APIService.Token = result.Token;
                        //await LoadData();
                        await Application.Current.MainPage.DisplayAlert("", "Logged in.", "OK");
                    }
                }
                catch {
                    await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong.", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username/Password can not be empty.", "OK");
            }
        }
    }
}
