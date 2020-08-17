using FindRestaurantsApp.Models;
using FindRestaurantsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindRestaurantsApp.Views.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestaurantCell : ViewCell
    {
        private Restaurant restaurant;
        private HomePageViewModel homePageViewModel;
        public RestaurantCell()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                var parent = Parent;
                while (true)
                {
                    if (parent is ListView)
                    {
                        homePageViewModel = Parent.BindingContext as HomePageViewModel;
                        break;
                    }
                    else
                    {
                        parent = parent.Parent;
                    }
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var rest = ((Restaurant)BindingContext);

            if (rest == null)
            {
                restaurant = null;
                return;
            }

            restaurant = rest;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            await homePageViewModel.SaveRestaurant(restaurant);
        }
    }
}