using FindRestaurantsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FindRestaurantsApp.Service
{
    public class RestaurantService : IRestaurantService
    {
        private HttpClient client;
        private const string LoginUrl = "https://my-account.azurewebsites.net/api/Login?code=bbEyqx8j4Ca2Pz22E99/ax3q/2M/pvxhr9IpsT9CWji8Tb4OcyZELQ==";
        private const string CreateAccountUrl = "https://my-account.azurewebsites.net/api/CreateAccount?code=LYs/3Qqs5dZfJAVhaPFgfEFduJueIS2BBtGBWiG67FsBMyypBe3KZw==";
        private const string FindRestaurantsUrl = "https://find-restaurants.azurewebsites.net/api/GetRestaurants?code=7eXtz3Lvk17dttqcBG6Ze7EOCThBY745udxKT7edpnAWqz4wuGFMQQ==";
        private const string GetMyRestaurantsUrl = "https://my-restaurants.azurewebsites.net/api/GetMyRestaurants?code=SuviW3U5cxoH0Nks00DkOl58Yhc0wYShnZatTlPXbTLOg5tmMoapQw==";
        private const string SaveRestaurantUrl = "https://my-restaurants.azurewebsites.net/api/SaveRestaurant?code=oKhVbs5PEd7Jyr3/PpGoFxCKOWrgYWdqTBl6uUEy8/sDWG22fSEUjQ==";
        private const string RemoveRestaurantUrl = "https://my-restaurants.azurewebsites.net/api/RemoveRestaurant?code=eGkE6piLeJd4kfSvDd7BqEJ1t8skRx2Lo6jsUCxpnOBhSnMJejOkwg==";

        public RestaurantService()
        {
            client?.Dispose();
            client = new HttpClient();
        }

        public async Task CreateAccount(User user)
        {
            App.LoggedInUser = await Send<User>(CreateAccountUrl, "POST", user, true, true);
        }

        public async Task<List<Restaurant>> FindRestaurants(Area area)
        {
            return await Send<List<Restaurant>>(FindRestaurantsUrl, "GET", area);
        }

        public async Task<List<Restaurant>> GetSavedRestaurants()
        {
            return await Send<List<Restaurant>>(GetMyRestaurantsUrl, "GET");
        }

        public async Task Login(User user)
        {
            App.LoggedInUser = await Send<User>(LoginUrl, "GET", user, true, true);
        }

        public async Task RemoveRestaurant(Restaurant restaurant)
        {
            await Send(RemoveRestaurantUrl, "POST", restaurant);
        }

        public async Task SaveRestaurant(Restaurant restaurant)
        {
            await Send(SaveRestaurantUrl, "POST", restaurant);
        }

        private async Task<T> Send<T>(string url, string verb, object data = null, bool anonymous = false, bool saveToken = false)
        {
            try
            {
                using (var httpMessage = new HttpRequestMessage())
                {
                    if (!anonymous)
                        httpMessage.Headers.Add("JWTToken", App.JWTToken);

                    httpMessage.RequestUri = new Uri(url);
                    httpMessage.Method = new HttpMethod(verb);

                    if (data != null)
                    {
                        var requestContent = JsonConvert.SerializeObject(data);
                        httpMessage.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
                    }

                    using (var respons = await client.SendAsync(httpMessage))
                    {
                        respons.EnsureSuccessStatusCode();

                        if (saveToken)
                            App.JWTToken = respons.Content.Headers.GetValues("JWTToken").First();

                        var json = await respons.Content.ReadAsStringAsync();
                        var resp = JsonConvert.DeserializeObject<T>(json);
                        return resp;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Send(string url, string verb, object data = null, bool anonymous = false)
        {
            try
            {
                using (var httpMessage = new HttpRequestMessage())
                {
                    if (!anonymous)
                        httpMessage.Headers.Add("JWTToken", App.JWTToken);

                    httpMessage.RequestUri = new Uri(url);
                    httpMessage.Method = new HttpMethod(verb);

                    if (data != null)
                    {
                        var requestContent = JsonConvert.SerializeObject(data);
                        httpMessage.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
                    }

                    using (var respons = await client.SendAsync(httpMessage))
                    {
                        respons.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
