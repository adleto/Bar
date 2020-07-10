using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Mobile.Models
{
    public class ItemListingModel : MvxNotifyPropertyChanged
    {
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        private int quantity = 0;
        public int Quantity
        {
            get { return quantity; }
            set {
                if (quantity+value>-1) {
                    quantity = value; RaisePropertyChanged(() => Quantity);
                }
            } 
        }
        private string dodatniOpis = string.Empty;
        public string DodatniOpis { get { return dodatniOpis; } set { SetProperty(ref dodatniOpis, value); } }
    }
}
