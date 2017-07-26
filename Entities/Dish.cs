using System.Collections.Generic;

namespace confusionresturant.Entities
{
    public class Dish 
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public string DishLabel { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}