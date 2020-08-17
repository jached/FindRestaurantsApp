using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

namespace FindRestaurantsApp.Droid
{
    public class DiContainer
    {
        public DiContainer()
        {

        }

        public void Init()
        {
            var nativeContainer = new ContainerBuilder();
            nativeContainer.RegisterModule<Module>();
            App.Container = nativeContainer.Build();
        }
    }
}