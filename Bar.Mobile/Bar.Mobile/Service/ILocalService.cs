using Bar.Models;
using Bar.Models.Items;
using Bar.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Mobile.Service
{
    public interface ILocalService
    {
        Task<List<ItemApiModel>> GetItems();
        Task WipeItemsAndLocation();
        Task<List<Bar.Models.Location>> GetLocations();
        Task InsertOrder(int? lokacijaId, List<ItemOrderInsertModel> list);
        Task<List<MobileOrderModel>> Get(int take = 5);
        Task InsertLocationsAndItems(List<Bar.Models.Location> locations, List<ItemApiModel> items);

    }
}
