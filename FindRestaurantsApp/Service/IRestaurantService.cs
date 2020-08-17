using FindRestaurantsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindRestaurantsApp.Service
{
    public interface IRestaurantService
    {
        Task CreateAccount(User user);
        Task Login(User user);
        Task<List<Restaurant>> FindRestaurants(Area area);
        Task SaveRestaurant(Restaurant restaurant);
        Task RemoveRestaurant(Restaurant restaurant);
        Task<List<Restaurant>> GetSavedRestaurants();
    }
}
