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
    /// Interaction logic for RestaurantsPage.xaml
    /// </summary>
    public partial class RestaurantsPage : Page
    {
        private static HttpClient client;
        public RestaurantsPage()
        {
            InitializeComponent();
            client = ClientClass.GetClient();
            DoMyWork();
        }
        private async void DoMyWork()
        {
            try
            {
                string json;
                HttpResponseMessage response = await client.GetAsync("restaurants");
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    IEnumerable<RestaurantDto> restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(json);
                    dgRestaurants.ItemsSource = restaurants;
                }

                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        private void btnFoods_Click(object sender, RoutedEventArgs e)
        {
            var selectedRestaurant = dgRestaurants.SelectedItem as RestaurantDto;
            if (selectedRestaurant != null)
            {
                RestaurantMenu restaurantMenu= new(selectedRestaurant.RestaurantId);
                Application.Current.MainWindow.Content = restaurantMenu;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedRestaurant = dgRestaurants.SelectedItem as RestaurantDto;
            if (selectedRestaurant != null)
            {
                UpdateRestaurantPage updateRestaurant = new(selectedRestaurant);
                Application.Current.MainWindow.Content = updateRestaurant;
            }
        }

        private void btnAddRestaurant_Click(object sender, RoutedEventArgs e)
        {
            AddRestaurantPage restaurantPage = new AddRestaurantPage();
            Application.Current.MainWindow.Content = restaurantPage;

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRestaurant = dgRestaurants.SelectedItem as RestaurantDto;
            if (selectedRestaurant != null)
            {
                string path = "restaurants/" + selectedRestaurant.RestaurantId;
                HttpResponseMessage response = await client.DeleteAsync(path);
                response.EnsureSuccessStatusCode();
                //dgRestaurants.Items.Clear();
                DoMyWork();
            }
        }
    }
}
