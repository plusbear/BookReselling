using AutoMapper;
using Identity.DataTransferObjects;
using Identity.Infrastructure;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public AuthenticationController(IdentityContext identityContext, IMapper autoMapper, UserManager<User> userManager, IAuthenticationManager authManager)
        {
            IdentityContext = identityContext;
            AutoMapper = autoMapper;
            UserManager = userManager;
            AuthManager = authManager;
        }

        public IMapper AutoMapper { get; set; }
        public IdentityContext IdentityContext { get; set; }
        public UserManager<User> UserManager { get; set; }
        public IAuthenticationManager AuthManager { get; set; }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = AutoMapper.Map<User>(userForRegistration);
            var result = await UserManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await UserManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuth)
        {
            var isValid = await AuthManager.ValidateUser(userForAuth);

            if(isValid)
            {
                return Ok(new { Token = await AuthManager.CreateToken() });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
