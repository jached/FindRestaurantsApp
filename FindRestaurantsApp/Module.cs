using Autofac;
using FindRestaurantsApp.Service;
using FindRestaurantsApp.ViewModels;
using FindRestaurantsApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindRestaurantsApp
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginPageViewModel>();
            builder.RegisterType<SignUpPageViewModel>();
            builder.RegisterType<HomePageViewModel>();
            builder.RegisterType<MyRestaurantsPageViewModel>();

            builder.RegisterType<LoginPage>();
            builder.RegisterType<SignUpPage>();
            builder.RegisterType<HomePage>();
            builder.RegisterType<MyRestaurantsPage>();

            builder.RegisterType<RestaurantService>().As<IRestaurantService>().SingleInstance();
        }
    }
}
