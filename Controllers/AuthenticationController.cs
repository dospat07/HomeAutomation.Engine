using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeAutomation.Engine.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace HomeAutomation.Engine.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserQuery usersQuery;

       

        public AuthenticationController(IUserQuery usersQuery)
        {
            this.usersQuery = usersQuery;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public async Task<bool> Login([FromBody] User user)
        {
            
            var count = usersQuery.GetAll().Where(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password)).ToList().Count;
            if (count == 1 )
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim("Name", user.UserName));
                // *** NOTE: SignInAsync is now on the HttpContext instance! ***
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
                {
                   
                    IsPersistent = true,
                   
                    
                });

                return true;
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return false;
        }
        [HttpPut]
        [HttpGet]
        [HttpPost]
        [Route("Account/Login")]
        public  IActionResult Unauthorized(string ReturnUrl)
       {
            return Unauthorized();
        }

        [Authorize]   
        [HttpGet]
        [Route("api/isAuthorized")]
        public bool IsAuthorized()
        {
            return true;
        }
    }
}
