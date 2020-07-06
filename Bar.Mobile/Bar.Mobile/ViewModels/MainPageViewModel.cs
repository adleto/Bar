using Bar.Mobile.Models;
using Bar.Mobile.Service;
using Bar.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bar.Mobile.ViewModels
{
    public class MainPageViewModel : /*INotifyPropertyChanged*/BaseViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        private readonly APIService _itemsService = new APIService("Item");
        private readonly APIService _orderSpecificService = new APIService("OrderSpecific");
        public ObservableCollection<ItemListingModel> ItemsList { get; set; } = new ObservableCollection<ItemListingModel>();
        //public ICommand InitCommand { get; set; }
        //public ICommand RaiseQuantity { private set; get; }
        public MainPageViewModel()
        {
            //InitCommand = new Command(async () => await Init());
            //RaiseQuantity = new Command((id) => DoRaise(id));
        }
        public void DoRaise(int? id)
        {
            var choosen = ItemsList.Where(i => i.ItemId == id).FirstOrDefault();
            choosen.Quantity++;
        }
        public void DoLower(int? id)
        {
            var choosen = ItemsList.Where(i => i.ItemId == id).FirstOrDefault();
            choosen.Quantity--;
        }
        public async Task CreateOrder()
        {
            try {
                bool notAllZero = false;
                var list = new List<ItemOrderInsertModel>();
                foreach (var i in ItemsList)
                {
                    if (i.Quantity > 0)
                    {
                        notAllZero = true;
                        list.Add(new ItemOrderInsertModel
                        {
                            ItemId = i.ItemId,
                            Quantity = i.Quantity
                        });
                        i.Quantity = 0;
                    }
                }
                if (notAllZero)
                {
                    await _orderSpecificService.Insert<List<ItemOrderInsertModel>>(list);
                    //HubConnection con = new HubConnectionBuilder().WithUrl($"http://10.0.2.2:52768/myHub").Build();
                    var serverUrl = Preferences.Get("serverUrl", "");
                    HubConnection con = new HubConnectionBuilder().WithUrl($"{serverUrl}/myHub").Build();
                    
                    //await con.SendAsync("RefreshMessage");
                    await con.StartAsync();
                    await con.InvokeAsync("SendMessage");
                    await con.StopAsync();
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong.", "OK");
            }
        }

        public async Task Init()
        {
            try {
                var list = await _itemsService.Get<List<Bar.Models.Item>>(null);
                ItemsList.Clear();
                foreach (var item in list)
                {
                    ItemsList.Add(new ItemListingModel
                    {
                        ItemId = item.Id,
                        ItemName = item.Naziv,
                        Quantity = 0
                    });
                }
            }
            catch
            {
                
            }
        }
    }
}
