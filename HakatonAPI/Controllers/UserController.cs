using Hakaton.Domain.DTOs;
using Hakaton.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hakaton.Api.Controllers
{
    [Route("api/users")]
    public class UserController : BaseApiController
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPut()]
        [Description("Update User details ")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> UpdateUserDetails(int userId,UserForUpdateDto updateDto)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.Biogram = updateDto.Biogram;
            }

            return user.Id;

        }

        [HttpGet()]
        [Description("Gets User Details by id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails(int userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            var palces = await _appDbContext.UserVisitedPlaces.Where(u => u.UserId == user.Id).ToListAsync();
            var achivementsId = await _appDbContext.UserAchievements.Where(u => u.UserId == userId).Select(a =>
            a.Id).ToListAsync();
            var achivements = new List<AchivementDto>();
            foreach(var s in achivementsId){
                var a = await _appDbContext.Achievements.FindAsync(s);
                if(a != null)
                {
                    var advert = new AchivementDto
                    {
                        Name = a.Name,
                        Description = a.Description,
                        PhotoSrc = a.PhotoSrc
                    };

                    achivements.Add(advert);
                }
            }
            var userDto = new UserDetailsDto
            {
                Username = user.UserName,
                Email = user.Email,
                Biogram = user.Biogram,
                ScorePoints = palces.Count,
                Achivements = achivements
            };

            return userDto;
        }
    }  
}
