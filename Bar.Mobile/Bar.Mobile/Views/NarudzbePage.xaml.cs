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
    public partial class NarudzbePage : ContentPage
    {
        ViewModels.MainPageViewModel model = null;
        public NarudzbePage()
        {
            InitializeComponent();
            BindingContext = model = new ViewModels.MainPageViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        public void Raise_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var id = button.CommandParameter as int?;
            if (id != null)
            {
                model.DoRaise(id);
            }
        }
        public void Lower_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var id = button.CommandParameter as int?;
            if (id != null)
            {
                model.DoLower(id);
            }
        }
        public async void Order_Clicked(object sender, EventArgs e)
        {
            await model.CreateOrder();
        }
    }
}