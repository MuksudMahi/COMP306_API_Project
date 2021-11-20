using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RestaurantNameFoodsDto
    {
        public string RestaurantName { get; set; }
        public int NumberOfFoods
        {
            get
            {
                return Foods.Count;
            }
        }

        public ICollection<FoodDto> Foods { get; set; } = new List<FoodDto>();

    }
}
