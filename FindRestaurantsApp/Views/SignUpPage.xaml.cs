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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPageViewModel SignUpPageViewModel { get; set; }
        public SignUpPage(SignUpPageViewModel signUpPageViewModel)
        {
            SignUpPageViewModel = signUpPageViewModel;
            BindingContext = SignUpPageViewModel;
            ((SignUpPageViewModel)BindingContext).Navigation = this.Navigation;
            InitializeComponent();
        }
    }
}