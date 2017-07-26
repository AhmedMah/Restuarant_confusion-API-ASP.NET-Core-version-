using System;
using confusionresturant.Entities;
using System.ComponentModel.DataAnnotations;

namespace confusionresturant.Models
{
    public class CommentModel
    {  
        public int CommentId { get; set; }
        public string Rating { get; set; }
        public string DishComment { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string Author { get; set; }
    }
}