using Bar.Mobile.Service;
using Bar.Models.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bar.Mobile.ViewModels
{
    public class MojeNarudzbeViewModel : BaseViewModel
    {
        public ILocalService LocalService => DependencyService.Get<ILocalService>();
        private ObservableCollection<MobileOrderModel> _MyList = new ObservableCollection<MobileOrderModel>();
        public ObservableCollection<MobileOrderModel> MyList { get { return _MyList; } set { SetProperty(ref _MyList, value); } }
        public async Task Init()
        {
            MyList = new ObservableCollection<MobileOrderModel>(await LocalService.Get(5));
        }
    }
}
