using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models.Locations
{
    public class ApiGetLocationModel
    {
        public DateTime TimeStamp { get; set; }
        public List<Bar.Models.Location> LocationList { get; set; }
    }
}
