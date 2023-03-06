using Hakaton.Domain.DTOs;
using Hakaton.Domain.Entities;
using Hakaton.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hakaton.Api.Controllers
{
    [Route("api/achivements")]
    public class AchivementsController : BaseApiController
    {
        private readonly AppDbContext _appDbContext;

        public AchivementsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Description("Creates new Achivements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> CreateAchivement(AchivementForCreation achivementsDto, CancellationToken cancellationToken)
        {
            var achivement = new Achivement
            {
                Name = achivementsDto.Name,
                Description = achivementsDto.Description,
                PhotoSrc = achivementsDto.PhotoSrc
            };
            _appDbContext.Achievements.Add(achivement);


            await _appDbContext.SaveChangesAsync(cancellationToken);
            return achivement.Id;
        }

        [HttpGet]
        [Description("Gets all Achivements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AchivementDto>>> GetAchivements()
        {
            var achivements = await _appDbContext
                .Achievements
                .AsNoTracking()
                .Select(a => new AchivementDto { 
                    Name = a.Name,
                    Description = a.Description,
                    PhotoSrc = a.PhotoSrc
                })
                .ToListAsync();

            return achivements;
        }

        [HttpPost("useradvert")]
        [Description("Creates new User Advert")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> CreateUserVisitedPlace(UserAchivementForCreationDto userAchivementForCreation, CancellationToken cancellationToken)
        {
            var achiv = new UserAchivement
            {
                UserId = userAchivementForCreation.UserId,
                AchivementId = userAchivementForCreation.AchivementId
            };
            _appDbContext.UserAchievements.Add(achiv);


            await _appDbContext.SaveChangesAsync(cancellationToken);
            return achiv.Id;
        }
    }
}
