using Bar.Mobile.Models;
using Bar.Mobile.Service;
using Bar.Models;
using Bar.Models.DatabaseTimeStamp;
using Bar.Models.Items;
using Bar.Models.Locations;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bar.Mobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly string serverUrl;
        public ILocalService LocalService => DependencyService.Get<ILocalService>();
        private readonly APIService _itemsService = new APIService("Item");
        private readonly APIService _orderSpecificService = new APIService("OrderSpecific");
        private readonly APIService _locationService = new APIService("Location");
        private readonly APIService _timeStampService = new APIService("DatabaseTimeStamp");
        public ObservableCollection<ItemListingModel> ItemsList { get; set; } = new ObservableCollection<ItemListingModel>();
        private ObservableCollection<Bar.Models.Location> _LocationList = new ObservableCollection<Bar.Models.Location>();
        public ObservableCollection<Bar.Models.Location> LocationList { get { return _LocationList; } set { SetProperty(ref _LocationList, value); } }
        private Bar.Models.Location location;

        public MainPageViewModel()
        {
            serverUrl = Preferences.Get("serverUrl", "");
        }

        public Bar.Models.Location Location { get { return location; } set { SetProperty(ref location, value); } }
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
            try
            {
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
                            Quantity = i.Quantity,
                            DodatniOpis = i.DodatniOpis
                        });
                        i.Quantity = 0;
                        i.DodatniOpis = string.Empty;
                    }
                }
                if (notAllZero)
                {
                    var model = new OrderInsertModel
                    {
                        List = list,
                        LocationId = null
                    };
                    int? loc = null;
                    if (Location != null && Location.Id != 0)
                    {
                        model.LocationId = Location.Id;
                        loc = Location.Id;
                    }
                    var taskExternalInsert = _orderSpecificService.Insert<OrderInsertModel>(model);
                    var taskLocalInsert = LocalService.InsertOrder(loc, model.List);
                    await taskExternalInsert;
                    await taskLocalInsert;
                    //var serverUrl = Preferences.Get("serverUrl", "");
                    HubConnection con = new HubConnectionBuilder().WithUrl($"{serverUrl}/myHub").Build();

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
            try
            {
                DateTime MyTimeStamp = Preferences.Get("myTimeStamp", DateTime.MinValue);
                var serverTimeStamp = (await _timeStampService.Get<TimeStampModel>()).TimeStamp;

                if (MyTimeStamp != serverTimeStamp)
                {
                    var locationGetModel = await _locationService.Get<ApiGetLocationModel>(null);
                    var itemGetModel = await _itemsService.Get<ApiGetItemModel>(null);
                    await LocalService.InsertLocationsAndItems(locationGetModel.LocationList, itemGetModel.ItemList);
                    Preferences.Set("myTimeStamp", itemGetModel.TimeStamp);
                }
                var locationList = await LocalService.GetLocations();
                var list = await LocalService.GetItems();


                ItemsList.Clear();
                LocationList.Clear();
                locationList.Insert(0, new Bar.Models.Location
                {
                    Description = "",
                    Id = 0
                });
                LocationList = new ObservableCollection<Bar.Models.Location>(locationList);

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
            catch { }
        }
    }
}
