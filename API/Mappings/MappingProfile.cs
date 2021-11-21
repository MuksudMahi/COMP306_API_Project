using API.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRFLibrary.Models;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantFoodsDto>();
            CreateMap<RestaurantNameFoodsDto, Restaurant>();
            CreateMap<Restaurant, RestaurantNameFoodsDto>();
            //CreateMap<Restaurant, RestaurantFoodsDto > ();
            CreateMap<RestaurantFoodsDto, Restaurant>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<RestaurantNameDto, Restaurant>();
            CreateMap<Restaurant,RestaurantNameDto > ();

            //CreateMap<Restaurant, CreateRestaurantDto>();
            CreateMap<CreateRestaurantDto, Restaurant>();

            CreateMap<Food, FoodDto>();
            CreateMap<FoodDto, Food>();
            CreateMap<FoodModelDto, Food>();
            CreateMap<Food, FoodModelDto>();
            CreateMap<CreateFoodDto, Food>();

        }
    }
}
