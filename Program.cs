using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreCommandAndQueries
{
    class Program
    {
        private static RestaurantDbContext context = new RestaurantDbContext();
        static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            //AddRestaurant();
            //AddRestaurants();
            //AddRestaurantAndFoodItems();

            DeleteRestaurant();

            //UpdateRestaurant();
            //UpdateRestaurantWhenNotTracked();
            //UpdateRestaurantWithFoodItemsIncludedNotTracked();
            //UpdateFoodItemWithIncludeRestaurantNotTracked();
            //UpdateRestaurants();


            Console.ReadLine();
        }

        private static void UpdateFoodItemWithIncludeRestaurantNotTracked()
        {
            var foodItem = context.FoodItems.Include(t=>t.Restaurant).FirstOrDefault(t => t.Id == 1);
            foodItem.Name = "FoodItem 1 updated";
            using (var context1 = new RestaurantDbContext())
            {
                // this is not efficient; it has a navigation property and so the untracked update with lead to
                //updating the navigation field as well here restaurant
                //context1.FoodItems.Update(foodItem);

                //Since we have navigation property to any other entity do the following
                //now only update for food item will be sent and not for restaurant
                context1.Entry(foodItem).State = EntityState.Modified;
                context1.SaveChanges();
            }
        }

        private static void UpdateRestaurantWithFoodItemsIncludedNotTracked()
        {
            var restaurant = context.Restaurants.Include(t=>t.FoodItems).FirstOrDefault(t=>t.Id==7);
            restaurant.Name = "Restaurant 1";
            using (var context1 = new RestaurantDbContext())
            {
                // this works fine since it has no navigation property to any other entity
                context1.Restaurants.Update(restaurant);
                //In case there was navigation property to any other entity then do the following
                //context1.Entry(restaurant).State = EntityState.Modified;
                context1.SaveChanges();
            }
        }

        //below is the scenario when the restaurant is not tracked
        private static void UpdateRestaurantWhenNotTracked()
        {
            var restaurant = context.Restaurants.Find(1);
            restaurant.Name = "Restaurant 1";
            using(var context1 = new RestaurantDbContext())
            {
                context1.Restaurants.Update(restaurant);
                context1.SaveChanges();
            }
        }

        private static void UpdateRestaurants()
        {
            throw new NotImplementedException();
        }

        private static void UpdateRestaurant()
        {
            var restaurant = context.Restaurants.Find(1);
            restaurant.Name = "Restaurant 1 changed";
            context.SaveChanges();

        }

        private static void DeleteRestaurant()
        {
            var restaurant = context.Restaurants.FirstOrDefault(t=>t.Id==1);
            context.Restaurants.Remove(restaurant);
            context.SaveChanges();
        }

        private static void AddRestaurantAndFoodItems()
        {
            var res = new Restaurant() { Name = "Restaurant 6"};
            res.FoodItems.AddRange(new List<FoodItem>()
            {
                new FoodItem(){ Name="FoodItem 1" },
                new FoodItem(){ Name="FoodItem 2" },
                new FoodItem(){ Name="FoodItem 3"},
                new FoodItem(){ Name="FoodItem 4" }}
            );
            context.Restaurants.Add(res);
            context.SaveChanges();
        }

        private static void AddRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant(){Name = "Restaurant 2"},
                new Restaurant(){Name = "Restaurant 3"},
                new Restaurant(){Name = "Restaurant 4" },
                new Restaurant(){Name = "Restaurant 5"}
            };

            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
        }

        private static void AddRestaurant()
        {
            var restaurant = new Restaurant()
            {
                Name = "Restaurant 1"
            };

            context.Restaurants.Add(restaurant);
            context.SaveChanges();
        }
    }
}
