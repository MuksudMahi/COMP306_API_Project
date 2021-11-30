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
    /// Interaction logic for UpdateRestaurantPage.xaml
    /// </summary>
    public partial class UpdateRestaurantPage : Page
    {
        private static HttpClient client;

        private RestaurantDto restaurant;
        public UpdateRestaurantPage(RestaurantDto restaurant)
        {
            InitializeComponent();
            this.restaurant = restaurant;
            client = ClientClass.GetClient();
            prepareForm();

        }

        private void prepareForm()
        {
            tbRestaurantName.Text = restaurant.RestaurantName;
            tbRestaurantTyppe.Text = restaurant.RestaurantType;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = tbRestaurantName.Text;
            string type = tbRestaurantTyppe.Text;

            if (name != null && type != null)
            {
                RestaurantDto updateRestaurant = new RestaurantDto();
                updateRestaurant.RestaurantId = restaurant.RestaurantId;
                updateRestaurant.RestaurantName = name;
                updateRestaurant.RestaurantType = type;

                string path = "restaurants/" + restaurant.RestaurantId;
                HttpResponseMessage response = await client.PutAsJsonAsync(path, updateRestaurant);
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
