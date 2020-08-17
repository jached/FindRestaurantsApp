using System;
using System.Collections.Generic;
using System.Text;

namespace FindRestaurantsApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
