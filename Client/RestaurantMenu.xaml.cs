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
    /// Interaction logic for RestaurantMenu.xaml
    /// </summary>
    public partial class RestaurantMenu : Page
    {
        private readonly HttpClient client;
        private readonly int RestaurantId;

        public RestaurantMenu(int RestaurantId)
        {
            InitializeComponent();
            client = ClientClass.GetClient();
            this.RestaurantId = RestaurantId;
            DoMyWork();
        }

        private async void DoMyWork()
        {
            //tbTest.Text = RestaurantId.ToString();
            try
            {
                string json;
                HttpResponseMessage response = await client.GetAsync("foods?id=" + RestaurantId);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    //tbText.Text = RestaurantId.ToString();
                    IEnumerable<FoodDto> restaurants = JsonConvert.DeserializeObject<IEnumerable<FoodDto>>(json);
                    //RestaurantDto restaurants = JsonConvert.DeserializeObject<RestaurantDto>(json);
                    //dgRestaurants.Items.Clear();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddFoodPage addFood = new AddFoodPage(RestaurantId);
            Application.Current.MainWindow.Content = addFood;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            RestaurantsPage restaurants = new RestaurantsPage();
            Application.Current.MainWindow.Content = restaurants;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedFood = dgRestaurants.SelectedItem as FoodDto;
            if(selectedFood!=null)
            {
                string path = "foods/" + selectedFood.FoodId;
                HttpResponseMessage response = await client.DeleteAsync(path);
                response.EnsureSuccessStatusCode();
                //dgRestaurants.Items.Clear();
                DoMyWork();
            }

        }

        private void btnUpdateFoods_Click(object sender, RoutedEventArgs e)
        {
            var selectedFood = dgRestaurants.SelectedItem as FoodDto;
            if (selectedFood != null)
            {
                UpdateFood updateFood = new UpdateFood(selectedFood, RestaurantId);
                Application.Current.MainWindow.Content = updateFood;
            }
        }
    }
}
