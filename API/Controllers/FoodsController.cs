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
    public class FoodsController : ControllerBase
    {
        private readonly projectContext _context;
        private IRestaurantRepository _contextRepo;
        private readonly IMapper _mapper;

        public FoodsController(projectContext context, IMapper mapper)
        {
            _context = context;
            _contextRepo = new RestaurantRepository(context);
            _mapper = mapper;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
            var foods = await _contextRepo.GetFoods();
            var results = _mapper.Map<IEnumerable<FoodModelDto>>(foods);
            return Ok(results);
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id, bool includeRestaurant=false)
        {
            var food = await _contextRepo.GetFoodById(id, includeRestaurant);

            if (food == null)
            {
                return NotFound();
            }

            if (includeRestaurant)
            {
                var foodsRestaurant = _mapper.Map<FoodModelDto>(food);
                return Ok(foodsRestaurant);
            }

            var foodWithoutRestaurant = _mapper.Map<FoodDto>(food);
            return Ok(foodWithoutRestaurant);
        }

        // PUT: api/Foods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, FoodDto food)
        {
            var foodToUpdate = _mapper.Map<Food>(food);


            if (!_contextRepo.FoodExists(foodToUpdate.FoodId).Result)
            {
                return NotFound();
            }
            foodToUpdate.RestaurantId = id;
            await _contextRepo.UpdateFood(foodToUpdate);
            return NoContent();
        }

        // POST: api/Foods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(CreateFoodDto food)
        {
            var foodToAdd = _mapper.Map<Food>(food);
            await _contextRepo.AddFoodForRestaurant(foodToAdd);

            return Ok();
            //return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (! _contextRepo.FoodExists(id).Result)
            {
                return NotFound();
            }

            await _contextRepo.DeleteFood(id);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
