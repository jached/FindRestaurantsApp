using Autofac;
using FindRestaurantsApp.Models;
using FindRestaurantsApp.Service;
using FindRestaurantsApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindRestaurantsApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IRestaurantService RestaurantService;
        private const int STANDARD_AREA = 500;
        public Command LogInCommand { get; set; }
        public Command SignUpCommand { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool WrongCredentials { get; set; }

        public LoginPageViewModel(IRestaurantService restaurantService)
        {
            LogInCommand = new Command(LogIn);
            SignUpCommand = new Command(SignUp);
            RestaurantService = restaurantService;
        }

        private async void SignUp()
        {
            SignUpPage signUpPage;
            using (var scope = App.Container.BeginLifetimeScope())
            {
                signUpPage = scope.Resolve<SignUpPage>();
                signUpPage.BackgroundColor = Color.FromHex("#2C2C2C");
            }
            await Navigation.PushAsync(signUpPage);
        }

        private async void LogIn()
        {
            try
            {
                var user = new User() { Username = Username, Password = Password };
                await RestaurantService.Login(user);
                App.MyArea = await GetLocation();

                HomePage homePage;
                using (var scope = App.Container.BeginLifetimeScope())
                {
                    homePage = scope.Resolve<HomePage>();
                }
                App.Current.MainPage = homePage;
            }
            catch (Exception)
            {
                WrongCredentials = true;
                OnPropertyChanged(nameof(WrongCredentials));
            }
        }

        private async Task<Area> GetLocation()
        {
            var locationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(locationRequest);
            var lat = location.Latitude.ToString();
            var lng = location.Longitude.ToString();
            var position = new Position() { Latitude = lat, Longitude = lng };
            return new Area() { Position = position, Radius = STANDARD_AREA };
        }
    }
}
