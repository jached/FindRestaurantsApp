using FindRestaurantsApp.Models;
using FindRestaurantsApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FindRestaurantsApp.ViewModels
{
    public class MyRestaurantsPageViewModel : ViewModelBase
    {
        private readonly IRestaurantService RestaurantService;
        public ObservableCollection<Restaurant> SavedRestaurants { get; set; }

        public MyRestaurantsPageViewModel(IRestaurantService restaurantService)
        {
            RestaurantService = restaurantService;
            var savedRestaurants = new List<Restaurant>();
            Task.Run(async () => savedRestaurants = await restaurantService.GetSavedRestaurants());
            SavedRestaurants = new ObservableCollection<Restaurant>(savedRestaurants);
        }

        public async Task RemoveRestaurant(Restaurant restaurant)
        {
            try
            {
                await RestaurantService.RemoveRestaurant(restaurant);
                SavedRestaurants.Remove(restaurant);
                OnPropertyChanged(nameof(SavedRestaurants));
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to remove restaurant");
            }
        }
    }
}
