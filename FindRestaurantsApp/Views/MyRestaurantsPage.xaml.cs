using FindRestaurantsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindRestaurantsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRestaurantsPage : ContentPage
    {
        public MyRestaurantsPageViewModel MyRestaurantsPageViewModel { get; set; }

        public MyRestaurantsPage(MyRestaurantsPageViewModel myRestaurantsPageViewModel)
        {
            MyRestaurantsPageViewModel = myRestaurantsPageViewModel;
            BindingContext = MyRestaurantsPageViewModel;
            ((MyRestaurantsPageViewModel)BindingContext).Navigation = this.Navigation;

            InitializeComponent();
        }
    }
}