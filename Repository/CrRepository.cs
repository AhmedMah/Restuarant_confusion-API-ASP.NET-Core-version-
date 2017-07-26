using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using confusionresturant.Data;
using confusionresturant.Entities;
using Microsoft.EntityFrameworkCore;

namespace confusionresturant.Repository
{
    public class CrRepository : ICrRepository
    {
        private readonly CrContext _context;
        public CrRepository(CrContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public IEnumerable<Dish> GetDishes()
        {
            return _context.Dishes.ToList();
        }
        public Dish GetDishWithComments(int id)
        {
            return _context.Dishes
                .Include(d => d.Comments)
                .Where(d => d.DishId == id)
                .FirstOrDefault();
        }
        public Dish GetDish(int id)
        {
            return _context.Dishes
                .Where(d => d.DishId == id)
                .FirstOrDefault();
        }
        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public IEnumerable<Dish> GetDishByCategory(string category)
        {
            return _context.Dishes
                         .Where(c => c.Category.Equals(category, StringComparison.CurrentCultureIgnoreCase))
                         .OrderBy(d => d.DishName)
                         .ToList();
        }

        public CrUser GetUser(string userName)
        {
            return _context.Users
                 .Include(u => u.Claims)
                 .Include(u => u.Roles)
                 .Where(u => u.UserName == userName)
                 .Cast<CrUser>()
                 .FirstOrDefault();
        }
    }
}