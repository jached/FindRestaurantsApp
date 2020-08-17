using System;
using System.Collections.Generic;
using System.Text;

namespace FindRestaurantsApp.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
        public Position Position { get; set; }
        public bool IsSaved { get; set; }
    }
}
