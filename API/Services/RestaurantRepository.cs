using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRFLibrary.Models;

namespace API.Services
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private projectContext _context;

        public RestaurantRepository(projectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> AddFoodForRestaurant(int restaurantId, Food food)
        {
            food.RestaurantId = restaurantId;
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return await GetFoodsForRestaurant(restaurantId);
        }

        public async Task<string> DeleteFood(Food food)
        {
            try
            {
                Food foodToDelete = _context.Foods.Where(s => s.FoodId == food.FoodId).FirstOrDefault();
                _context.Foods.Remove(foodToDelete);
                await _context.SaveChangesAsync();
                return "Successfully deleted food with id " + foodToDelete.FoodId.ToString();
            }
            catch (Exception)
            {
                return "Fail. Unable to delete food with id " + food.FoodId.ToString();
            }
        }

        public async Task DeleteRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task<Food> GetFoodForRestaurant(int RestaurantId, int FoodId)
        {
            IQueryable<Food> result = _context.Foods.Where(r => r.RestaurantId == RestaurantId && r.FoodId==FoodId);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            var result = _context.Foods.Include(f=>f.Restaurant);
            return await result.ToListAsync();
            
        }

        public async Task<IEnumerable<Food>> GetFoodsForRestaurant(int RestaurantId)
        {
            IQueryable<Food> result = _context.Foods.Where(r => r.RestaurantId == RestaurantId);
            return await result.ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantById(int restaurantId, bool includeFoods)
        {
            IQueryable<Restaurant> result;
            if(includeFoods)
            {
                result = _context.Restaurants.Include(r => r.Foods)
                    .Where(r => r.RestaurantId == restaurantId);
            }
            else result = _context.Restaurants.Where(r => r.RestaurantId == restaurantId);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            var result = _context.Restaurants.OrderBy(r => r.RestaurantName);
            return await result.ToListAsync();
        }

        public async Task<bool> RestaurantExists(int RestaurantId)
        {
            return await _context.Restaurants.AnyAsync(restaurant => restaurant.RestaurantId == RestaurantId);
        }

        public async Task<IEnumerable<Restaurant>> SaveRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return await GetRestaurants();
        }

        public async Task UpdateRestaurant(Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
