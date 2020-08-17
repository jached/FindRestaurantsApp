using Autofac;
using FindRestaurantsApp.Models;
using FindRestaurantsApp.Service;
using FindRestaurantsApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FindRestaurantsApp.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly IRestaurantService RestaurantService;

        public Command SignUpCommand { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public SignUpPageViewModel(IRestaurantService restaurantService)
        {
            SignUpCommand = new Command(SignUp);
            RestaurantService = restaurantService;
        }

        private async void SignUp()
        {
            var user = new User() { Username = Username, Password = Password };
            try
            {
                await RestaurantService.CreateAccount(user);
                HomePage homePage;
                using (var scope = App.Container.BeginLifetimeScope())
                {
                    homePage = scope.Resolve<HomePage>();
                }
                App.Current.MainPage = homePage;

            }
            catch (Exception)
            {
            }
        }
    }
}
