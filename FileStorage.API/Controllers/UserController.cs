using FileStorage.Bussiness.Abstract;
using FileStorage.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create user allowed anonymously
        /// anyone can crate users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            var User = await _userService.Create(user);
            return CreatedAtAction(nameof(Create), User);
        }

        /// <summary>
        /// Delete user with id
        /// Checks if user exists. if so deletes.
        /// Only super admins can delete users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _userService.GetById(id) != null)
            {
                await _userService.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// returns all the users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }



        /// <summary>
        /// Update user 
        /// only super admins can Update users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update([FromBody] User user)
        {

            var oldUser = await _userService.GetById(user.Id);
            if (oldUser != null)
            {
                return Ok(await _userService.Update(user));
            }
            return NotFound();
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("[action]/{userName}/{password}")]
        public async Task<IActionResult> Authenticate(string userName, string password)
        {

            User user = _userService.GetAll().Result.ToList().Find(x => x.UserName == userName);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserName ?? String.Empty),
                        new Claim(ClaimTypes.Role,user.Role.ToString()),
                        new Claim(ClaimTypes.Sid,user.Id.ToString()),
                    };

                    ClaimsIdentity claimsIdentity = new(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties authProperties = new() { AllowRefresh = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return Ok();
                }
                return NotFound();
            }
            return NotFound();
        }
    }

}
