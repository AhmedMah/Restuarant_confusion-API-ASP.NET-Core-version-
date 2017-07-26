using System;

namespace confusionresturant.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int Rating { get; set; }
        public string DishComment { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public CrUser User { get; set; }
        public Dish Dish { get; set; }
    }
}