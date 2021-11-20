using System;
using System.Collections.Generic;

#nullable disable

namespace TRFLibrary.Models
{
    public partial class Food
    {
        public int FoodId { get; set; }
        public int RestaurantId { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public decimal FoodPrice { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
