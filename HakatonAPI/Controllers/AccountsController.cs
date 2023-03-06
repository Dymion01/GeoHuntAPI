using Hakaton.Application.Common.Interfaces;
using Hakaton.Domain.DTOs;
using Hakaton.Domain.Entities;
using Hakaton.Infrastructure;
using Hakaton.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hakaton.Api.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountsController> _logger;



        public AccountsController(
            ITokenService tokenService,
            AppDbContext appDbContext,
            UserManager<User> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<User> signInManager,
            ILogger<AccountsController> logger
            )
        {
            _tokenService = tokenService;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;

        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            //var asignrole = await _userManager.AddToRoleAsync(user, registerDto.Role);
            //if (!asignrole.Succeeded) return BadRequest("Failed to asign role");

            return new UserDto
            {

                Username = user.UserName,
                UserId = user.Id
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Login);
            if (user == null)
            {
                 user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Login);
            }

            if (user == null) return Unauthorized("Invalid login or email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();
            string token = await _tokenService.CreateToken(user);
            return new UserDto
            {
                Username = user.UserName,
                UserId = user.Id
            };


        }

    }
}
