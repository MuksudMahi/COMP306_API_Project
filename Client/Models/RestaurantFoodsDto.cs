using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class RestaurantFoodsDto
    {

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }

        public ICollection<FoodModelDto> Foods { get; set; } = new List<FoodModelDto>();
    }
}
