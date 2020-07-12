using Bar.Mobile.Helpers;
using Bar.Mobile.Models;
using Bar.Models;
using Bar.Models.Items;
using Bar.Models.Order;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Mobile.Service
{
    public class LocalService : ILocalService
    {
        private readonly SQLiteAsyncConnection _connection;
        private string _dbPath = FileAccessHelper.GetLocalFilePath("dbLite.db3");
        public LocalService()
        {
            _connection = new SQLiteAsyncConnection(_dbPath);
            _connection.CreateTableAsync<ItemApiModel>().Wait();
            _connection.CreateTableAsync<Location>().Wait();
            _connection.CreateTableAsync<MojaNarudzbaModel>().Wait();
            _connection.CreateTableAsync<NarudzbaItemModel>().Wait();
        }
        ~LocalService()
        {
            _connection.CloseAsync().Wait();
        }

        public async Task<List<MobileOrderModel>> Get(int take = 5)
        {
            var returnList = new List<MobileOrderModel>();
            var listNarudzbe = await _connection
                .Table<MojaNarudzbaModel>()
                .OrderByDescending(o => o.Id)
                .Take(take)
                .ToArrayAsync();
            foreach (var narudzba in listNarudzbe)
            {
                string lokacija = "";
                if (narudzba.LokacijaId != null)
                {
                    lokacija = (await _connection
                    .Table<Location>()
                    .FirstAsync(l => l.Id == narudzba.LokacijaId))
                    .Description;
                }
                var insertModel = new MobileOrderModel
                {
                    OrderList = new List<MobileOrderListingItemModel>(),
                    Lokacija = lokacija
                };
                var listItemNarudzba = await _connection
                    .Table<NarudzbaItemModel>()
                    .Where(n => n.MojaNarudzbaId == narudzba.Id)
                    .ToListAsync();
                foreach(var x in listItemNarudzba)
                {
                    string naziv = (await _connection.Table<ItemApiModel>()
                        .FirstAsync(i => i.Id == x.ItemId))
                        .Naziv;
                    insertModel.OrderList.Add(new MobileOrderListingItemModel
                    {
                        DodatniOpis = x.DodatniOpis,
                        Kolicina = x.Kolicina,
                        Naziv = naziv
                    });
                }
                returnList.Add(insertModel);
            }
            return returnList;
        }
        public async Task InsertOrder(int? lokacijaId, List<ItemOrderInsertModel> list)
        {
            await _connection.InsertAsync(new MojaNarudzbaModel { LokacijaId = lokacijaId });
            var listNarudzba = await _connection.Table<MojaNarudzbaModel>()
                .OrderByDescending(n => n.Id)
                .Take(1)
                .ToListAsync();
            var insertList = new List<NarudzbaItemModel>();
            foreach (var x in list)
            {
                insertList.Add(new NarudzbaItemModel
                {
                    ItemId = x.ItemId,
                    Kolicina = x.Quantity,
                    MojaNarudzbaId = listNarudzba[0].Id,
                    DodatniOpis = x.DodatniOpis
                });
            }
            await _connection.InsertAllAsync(insertList);
        }

        public async Task<List<ItemApiModel>> GetItems()
        {
            return await _connection.Table<ItemApiModel>().ToListAsync();
        }

        public async Task<List<Location>> GetLocations()
        {
            return await _connection.Table<Location>().ToListAsync();
        }

        public async Task InsertLocationsAndItems(List<Location> locations, List<ItemApiModel> items)
        {
            await WipeItemsAndLocation();
            await _connection.InsertAllAsync(locations);
            await _connection.InsertAllAsync(items);
        }
        public async Task WipeItemsAndLocation()
        {
            await _connection.DeleteAllAsync<Location>();
            await _connection.DeleteAllAsync<ItemApiModel>();
        }
    }
}
