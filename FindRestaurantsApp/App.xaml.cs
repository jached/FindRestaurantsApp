using Autofac;
using FindRestaurantsApp.Models;
using FindRestaurantsApp.ViewModels;
using FindRestaurantsApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindRestaurantsApp
{
    public partial class App : Application
    {
        public static string JWTToken { get; set; }
        public static User LoggedInUser { get; set; }
        public static IContainer Container { get; set; }
        public static Area MyArea { get; set; }

        public App()
        {
            InitializeComponent();

            LoginPage loginPage;

            using (var scope = App.Container.BeginLifetimeScope())
            {
                loginPage = scope.Resolve<LoginPage>();
            }
            //MainPage = new NavigationPage(loginPage);
            MainPage = new NavigationPage(loginPage)
            {
                BarBackgroundColor = Color.FromHex("#2C2C2C")
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
