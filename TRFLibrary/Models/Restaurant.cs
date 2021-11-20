using System;
using System.Collections.Generic;

#nullable disable

namespace TRFLibrary.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Foods = new HashSet<Food>();
        }

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
