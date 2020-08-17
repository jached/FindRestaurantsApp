using FindRestaurantsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace FindRestaurantsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPageViewModel LoginPageViewModel { get; set; }

        public LoginPage(LoginPageViewModel loginPageViewModel)
        {
            LoginPageViewModel = loginPageViewModel;
            BindingContext = LoginPageViewModel;
            ((LoginPageViewModel)BindingContext).Navigation = this.Navigation;

            InitializeComponent();
        }
    }
}