using System.Collections.Generic;
using System.Threading.Tasks;
using confusionresturant.Entities;

namespace confusionresturant.Repository
{
    public interface  ICrRepository
    {
        //Basic Db Operations
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        Task<bool> SaveAllAsync();

       // Dishes
        IEnumerable<Dish>GetDishes();
        IEnumerable<Dish> GetDishByCategory(string category);
        
        Dish GetDish(int id);
         Dish GetDishWithComments(int id);

         // Comments
         Comment GetCommentsByDish(int id);
        //Cr user
        CrUser GetUser (string userName);
    }
}