using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRFLibrary.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly projectContext _context;
        private IRestaurantRepository _contextRepo;
        private readonly IMapper _mapper;

        public RestaurantsController(projectContext context, IMapper mapper)
        {
            _context = context;
            _contextRepo = new RestaurantRepository(context);
            _mapper = mapper;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _contextRepo.GetRestaurants();
            var results = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return Ok(results);
        }


        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id, bool includeFoods=false)
        {
            var restaurant = await _contextRepo.GetRestaurantById(id, includeFoods);

            if (restaurant == null)
            {
                return NotFound();
            }

            if(includeFoods)
            {
                var restaurantFoods = _mapper.Map<RestaurantNameFoodsDto>(restaurant);
                return Ok(restaurantFoods);
            }

            var restaurantWithoutFoods = _mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantWithoutFoods);
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantDto restaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            if (id != restaurant.RestaurantId)
            {
                return BadRequest();
            }

            if(! _contextRepo.RestaurantExists(id).Result)
            {
                return NotFound();
            }

            await _contextRepo.UpdateRestaurant(restaurant);

            return NoContent();
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(CreateRestaurantDto restaurant)
        {
            var restaurantToAdd =_mapper.Map<Restaurant>(restaurant);
            await _contextRepo.SaveRestaurant(restaurantToAdd);

            var restaurantFoods = _mapper.Map<RestaurantFoodsDto>(restaurantToAdd);

            return CreatedAtAction("GetRestaurant", new { Id = restaurantFoods.RestaurantId }, restaurantFoods);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _contextRepo.GetRestaurantById(id, false);
            if (restaurant == null)
            {
                return NotFound();
            }

            await _contextRepo.DeleteRestaurant(restaurant);

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.RestaurantId == id);
        }
    }
}
