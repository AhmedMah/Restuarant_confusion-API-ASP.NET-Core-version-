using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using confusionresturant.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace confusionresturant.Data
{
    public class CRInitializer
    {
        private CrContext _ctx;
        private UserManager<CrUser> _userMgr;
        private RoleManager<IdentityRole> _roleMgr;

        public CRInitializer(UserManager<CrUser> userMgr,
                              RoleManager<IdentityRole> roleMgr,
                              CrContext ctx)
        {
            _ctx = ctx;
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task Seed()
        {
          
            var user = await _userMgr.FindByNameAsync("ahmedabdi");

            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    role.Claims.Add(new IdentityRoleClaim<string>()
                    {
                        ClaimType = "IsAdmin",
                        ClaimValue = "True"
                    });
                    await _roleMgr.CreateAsync(role);
                }
                user = new CrUser()
                {
                    UserName = "ahmedabdi",
                    FirstName = "Ahmed",
                    LastName = "Abdi",
                    Email = "aabdi417@gmail.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "Ahm3dia@!");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user or role");
                }
            }

            if (!_ctx.Dishes.Any())
            {
                _ctx.AddRange(_sample);
                await _ctx.SaveChangesAsync();
            }
        }
        List<Dish> _sample = new List<Dish>
        {
                    new Dish()
                    {
                        DishName = "Vadonut",
                        DishLabel = "Hot",
                        Category = "appetizer",
                        Price = 1,
                        Description = "A quintessential ConFusion experience, is it a vada or is it a donut?",
                        ImageUrl = "/images/vadonut.jpg",
                    },
                    new Dish()
                    {
                        DishName = "Croissants",
                        DishLabel = "New",
                        Category = "appetizer",
                        Price = 3,
                        Description = "light, flaky and buttery crescent-shaped French pastries.",
                        ImageUrl = "/images/croissants.jpg",
                    },
                   new Dish()
                   {
                       DishName = "Uthapiza",
                       DishLabel = "Hot",
                       Category = "mains",
                       Price = 4,
                       Description="Unique Italian Uthapiza ",
                       ImageUrl = "/images/uthapiza.jpg",
                   },
        };
        
    }
}