using Bar.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bar.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MojeNarudzbePage : ContentPage
    {
        MojeNarudzbeViewModel viewModel;
        public MojeNarudzbePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MojeNarudzbeViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Init();
        }
    }
}