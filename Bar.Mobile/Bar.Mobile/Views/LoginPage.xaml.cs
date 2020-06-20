using Bar.Mobile.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bar.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            aiLayout.IsVisible = true;
            await viewModel.LoadData();
            IsBusy = false;
            aiLayout.IsVisible = false;
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            aiLayout.IsVisible = true;
            await viewModel.Login();
            MessagingCenter.Send(this, "loggedIn");
            IsBusy = false;
            aiLayout.IsVisible = false;
        }
    }
}