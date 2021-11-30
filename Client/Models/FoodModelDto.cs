﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{ 
    public class FoodModelDto
    {
        public int FoodId { get; set; }
        public int RestaurantId { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public decimal FoodPrice { get; set; }

    }
}
