using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for AddRestaurantPage.xaml
    /// </summary>
    public partial class AddRestaurantPage : Page
    {
        private static HttpClient client;
        public AddRestaurantPage()
        {
            InitializeComponent();
            client = ClientClass.GetClient();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = tbRestaurantName.Text;
            string type = tbRestaurantTyppe.Text;

            if(name!="" && type!="")
            {
                CreateRestaurantDto createRestaurant = new CreateRestaurantDto();
                createRestaurant.RestaurantName = name;
                createRestaurant.RestaurantType = type;
                string json = JsonConvert.SerializeObject(createRestaurant);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("restaurants", content);
                response.EnsureSuccessStatusCode();

                RestaurantsPage restaurants = new RestaurantsPage();
                Application.Current.MainWindow.Content = restaurants;


            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RestaurantsPage restaurants = new RestaurantsPage();
            Application.Current.MainWindow.Content = restaurants;
        }
    }
}
