using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Database.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
