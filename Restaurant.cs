using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreCommandAndQueries
{
    public class Restaurant
    {
        public Restaurant()
        {
            FoodItems = new List<FoodItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<FoodItem> FoodItems { get; set; }


    }
}
