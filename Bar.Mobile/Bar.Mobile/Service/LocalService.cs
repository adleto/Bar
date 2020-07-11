using Bar.Mobile.Helpers;
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
            //_connection.CreateTableAsync<MobileOrderModel>().Wait();
            //_connection.CreateTableAsync<MobileOrderListingItemModel>().Wait();
        }
        ~LocalService()
        {
            _connection.CloseAsync().Wait();
        }

        public Task<List<MobileOrderModel>> Get(int take = 5)
        {
            throw new NotImplementedException();
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

        public Task InsertOrder(List<ItemOrderInsertModel> list)
        {
            throw new NotImplementedException();
        }

        public async Task WipeItemsAndLocation()
        {
            await _connection.DeleteAllAsync<Location>();
            await _connection.DeleteAllAsync<ItemApiModel>();
        }
    }
}
