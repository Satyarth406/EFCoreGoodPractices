using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreCommandAndQueries
{
    public class FoodItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

    }
}
