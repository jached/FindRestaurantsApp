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
    public partial class HomePage : ContentPage
    {
        public HomePageViewModel HomePageViewModel { get; set; }
        public HomePage(HomePageViewModel homePageViewModel)
        {
            HomePageViewModel = homePageViewModel;
            BindingContext = HomePageViewModel;
            ((HomePageViewModel)BindingContext).Navigation = this.Navigation;
            InitializeComponent();
        }
    }
}