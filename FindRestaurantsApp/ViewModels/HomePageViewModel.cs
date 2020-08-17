using Autofac;
using FindRestaurantsApp.Models;
using FindRestaurantsApp.Service;
using FindRestaurantsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindRestaurantsApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IRestaurantService RestaurantService;
        public Area MyArea { get; set; }
        public ObservableCollection<Restaurant> Restaurants { get; set; }
        public Command MyRestaurantsCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command ChangeRadiusCommand { get; set; }
        public bool IsRefreshing { get; set; }
        public int Radius { get; set; }
        public string RadiusText { get; set; }

        public HomePageViewModel(IRestaurantService restaurantService)
        {
            RestaurantService = restaurantService;
            MyArea = App.MyArea;
            Radius = 500;
            RadiusText = $"Radius = {Radius}m";
            Task.Run(async () => await FindRestaurants());
            MyRestaurantsCommand = new Command(async () => await MyRestaurantsTapped());
            RefreshCommand = new Command(async () => await RefreshRestaurants());
            ChangeRadiusCommand = new Command(async () => await ChangeRadius());
        }

        private async Task ChangeRadius()
        {
            RadiusText = $"Radius = {Radius}m";
            OnPropertyChanged(nameof(RadiusText));
            await RefreshRestaurants();
        }

        private async Task RefreshRestaurants()
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            await GetLocation();
            await FindRestaurants();
            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

        private async Task MyRestaurantsTapped()
        {
            MyRestaurantsPage myRestaurantsPage;
            using (var scope = App.Container.BeginLifetimeScope())
            {
                myRestaurantsPage = scope.Resolve<MyRestaurantsPage>();
            }
            await Navigation.PushModalAsync(myRestaurantsPage);
        }

        private async Task GetLocation()
        {
            try
            {
                var locationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(locationRequest);
                var lat = location.Latitude.ToString();
                var lng = location.Longitude.ToString();
                var position = new Position() { Latitude = lat, Longitude = lng };
                MyArea = new Area() { Position = position, Radius = Radius };
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get location");
            }
        }

        private async Task FindRestaurants()
        {
            try
            {
                var restaurants = await RestaurantService.FindRestaurants(MyArea);
                Restaurants = new ObservableCollection<Restaurant>(restaurants);
                OnPropertyChanged(nameof(Restaurants));

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to find restaurants");
                Restaurants = new ObservableCollection<Restaurant>();
            }
        }


        public async Task SaveRestaurant(Restaurant restaurant)
        {
            try
            {
                await RestaurantService.SaveRestaurant(restaurant);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to save restaurant");
            }
        }
    }
}
