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
    /// Interaction logic for UpdateFood.xaml
    /// </summary>
    public partial class UpdateFood : Page
    {
        private static HttpClient client;

        private FoodDto food;
        private int RestaurantId;
        public UpdateFood(FoodDto food, int RestaurantId)
        {
            InitializeComponent();
            this.food = food;
            this.RestaurantId = RestaurantId;
            client = ClientClass.GetClient();
            PrepareForm();
        }

        private void PrepareForm()
        {
            tbFoodName.Text = food.FoodName;
            tbDescription.Text = food.FoodDescription;
            tbPrice.Text = food.FoodPrice.ToString();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = tbFoodName.Text;
            string description = tbDescription.Text;
            decimal price;
            if (decimal.TryParse(tbPrice.Text, out price) && name != "" && description != "")
            {
                FoodDto updateFood = new();
                updateFood.FoodId = food.FoodId;
                updateFood.FoodName = name;
                updateFood.FoodDescription = description;
                updateFood.FoodPrice = price;
                //updateFood.RestaurantId = RestaurantId;
                //string json = JsonConvert.SerializeObject(createFood);
                //StringContent content = new StringContent(json, Encoding.UTF8, "application/json")
                string path = "foods/" + RestaurantId;
                HttpResponseMessage response = await client.PutAsJsonAsync(path , updateFood);
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
