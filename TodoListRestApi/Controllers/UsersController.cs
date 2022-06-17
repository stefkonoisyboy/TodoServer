using Microsoft.AspNetCore.Mvc;
using TodoListRestApi.Data.Models;
using TodoListRestApi.Helpers;
using TodoListRestApi.Services.Interfaces;
using TodoListRestApi.ViewModels.Users;

namespace TodoListRestApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersService usersService;
        private readonly JwtService jwtService;

        public UsersController(IUsersService usersService, JwtService jwtService)
        {
            this.usersService = usersService;
            this.jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel dto)
        {
            if (this.usersService.Exists(dto.Email))
            {
                return this.BadRequest(new { Message = "User with that email already exists!", IsSuccessful = false });
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Message = "Your email or password does not fit the requirements! Password should be between 6 and 100 characters and should contain at least 1 small letter, 1 capital letter and 1 special symbol!", IsSuccessful = false });
            }

            var user = new ApplicationUser
            {
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            var result = this.usersService.Create(user);

            return this.Ok(new
            {
                Message = "You registered successfully!",
                IsSuccessful = true
            });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel dto)
        {
            var user = this.usersService.GetByEmail(dto.Email);

            if (user == null)
            {
                return this.BadRequest(new { Message = "Invalid Credentials!", IsSuccessful = false });
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return this.BadRequest(new { Message = "Invalid Credentials!", IsSuccessful = false });
            }

            return this.Ok(new { Message = "You logged-in successfully!", IsSuccessful = true });
        }

        [HttpGet("{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            return this.Ok(this.usersService.GetByEmail(email));
        }
    }
}
