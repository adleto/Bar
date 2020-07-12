using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Mobile.Models
{
    public class MojaNarudzbaModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int? LokacijaId { get; set; }
    }
    public class NarudzbaItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MojaNarudzbaId { get; set; }
        public int ItemId { get; set; }
        public int Kolicina { get; set; }
        public string DodatniOpis { get; set; }
    }
}
