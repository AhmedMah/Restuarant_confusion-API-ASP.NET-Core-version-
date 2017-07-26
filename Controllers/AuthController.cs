using System;
using System.Threading.Tasks;
using confusionresturant.Data;
using confusionresturant.Entities;
using confusionresturant.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace confusionresturant.Controllers
{
    public class AuthController : Controller
    {
        private CrContext _context;
        private SignInManager<CrUser> _signInMgr;
        private ILogger<AuthController> _logger;

        public AuthController(CrContext context, SignInManager<CrUser> signInMgr, ILogger<AuthController> logger)
       {
           _context = context;
           _signInMgr = signInMgr;
           _logger = logger;
       }
       [HttpPost("api/Auth/login")]
       public async Task <IActionResult> Login ([FromBody] LoginModel model)
       {
         try
         {
        var result = await _signInMgr.PasswordSignInAsync(model.UserName , model.Password, false, false);
        if(result.Succeeded)
        {
            return Ok();
        }
         }
         catch(Exception ex)
         {
           _logger.LogError($"an Exception is thrown while logging in {ex}");
         }
         return BadRequest("Failed to login");
       }
    }
}