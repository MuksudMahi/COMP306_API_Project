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
    /// Interaction logic for AddFoodPage.xaml
    /// </summary>
    public partial class AddFoodPage : Page
    {
        private readonly HttpClient client;
        private readonly int RestaurantId;
        public AddFoodPage(int RestaurantId)
        {
            InitializeComponent();
            client = ClientClass.GetClient();
            this.RestaurantId = RestaurantId;
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = tbFoodName.Text;
            string description = tbDescription.Text;
            decimal price;
            if (decimal.TryParse(tbPrice.Text, out price) && name != null && description != null)
            {
                CreateFoodDto createFood = new();
                createFood.FoodName = name;
                createFood.FoodDescription = description;
                createFood.FoodPrice = price;
                createFood.RestaurantId = RestaurantId;
                string json = JsonConvert.SerializeObject(createFood);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("foods", content);
                response.EnsureSuccessStatusCode();

                RestaurantMenu restaurants = new RestaurantMenu(RestaurantId);
                Application.Current.MainWindow.Content = restaurants;
            }
            else
            {
                tbMessage.Text = "Invalid Input";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RestaurantMenu restaurants = new RestaurantMenu(RestaurantId);
            Application.Current.MainWindow.Content = restaurants;
        }
    }
}
