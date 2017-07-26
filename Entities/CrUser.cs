using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace confusionresturant.Entities
{
    public class CrUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}