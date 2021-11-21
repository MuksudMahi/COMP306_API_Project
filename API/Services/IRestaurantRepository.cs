using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRFLibrary.Models;

namespace API.Services
{
    public interface IRestaurantRepository
    {
        Task<bool> RestaurantExists(int RestaurantId);
        Task<IEnumerable<Restaurant>> GetRestaurants();
        Task<Restaurant> GetRestaurantById(int restaurantId, bool includeFoods);
        Task<IEnumerable<Food>> GetFoodsForRestaurant(int RestaurantId);
        Task<Food> GetFoodForRestaurant(int RestaurantId, int FoodId);
        Task<IEnumerable<Food>> AddFoodForRestaurant(Food food);
        Task<IEnumerable<Restaurant>> SaveRestaurant(Restaurant restaurant);

        Task DeleteRestaurant(Restaurant restaurant);
        Task UpdateRestaurant(Restaurant restaurant);
        Task<IEnumerable<Food>> GetFoods();
        Task<string> DeleteFood(int foodId);
        Task<Food> GetFoodById(int foodId, bool includeRestaurant);
        Task UpdateFood(Food food);
        Task<bool> FoodExists(int foodId);

    }
}
