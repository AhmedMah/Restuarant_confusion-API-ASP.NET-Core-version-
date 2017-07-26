using AutoMapper;
using confusionresturant.Entities;

namespace confusionresturant.Models
{
    public class DishMappingProfile : Profile
    {
       public DishMappingProfile()
       {
           CreateMap<Dish, DishModel>()
           .ReverseMap();
           
           CreateMap<Comment, CommentModel>()
            .ForMember(m => m.Author, opt => opt.ResolveUsing(u => u.User.UserName.ToString()))
            .ReverseMap();
       }
    }
}