using Hakaton.Domain.DTOs;
using Hakaton.Domain.Entities;
using Hakaton.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hakaton.Api.Controllers
{
    [Route("api/visitedplaces")]
    public class VisitedPlacesController : BaseApiController
    {
        private readonly AppDbContext _appDbContext;

        public VisitedPlacesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Description("Creates new Visited Place")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> CreateVisitedPlace(VisitedPlaceForCreationDto visitedPlaceForCreation, CancellationToken cancellationToken)
        {
            var place = new VisitedPlace
            {
                Name = visitedPlaceForCreation.Name,
                Description = visitedPlaceForCreation.Description,
                Coordinates = visitedPlaceForCreation.Coordinates,
                PhotoSrc = visitedPlaceForCreation.PhotoSrc,
                ParentId = visitedPlaceForCreation.PatentId ?? null
            };
            _appDbContext.VisitedPlaces.Add(place);


            await _appDbContext.SaveChangesAsync(cancellationToken);
            return place.Id;
        }

        [HttpGet("{id}")]
        [Description("Gets Visited Place by id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VisitedPlaceDto>> GetPlaveDetails(int id)
        {
            var place = await _appDbContext.VisitedPlaces.FindAsync(id);

            var placeDto = new VisitedPlaceDto
            {
                Name = place.Name,
                Coordinates = place.Coordinates,
                Description = place.Description,
                PhotoSrc = place.PhotoSrc
            };
            return placeDto;
        }

        [HttpPost("uservisited")]
        [Description("Creates new User Visited Place")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> CreateUserVisitedPlace(UserVisitedPlaceForCreationDto userVisitedPlaceForCreation, CancellationToken cancellationToken)
        {
            var place = new UserVisitedPlace
            {
                UserId = userVisitedPlaceForCreation.UserId,
                VisitedPlaceId = userVisitedPlaceForCreation.VisitedPlaceId
            };
            _appDbContext.UserVisitedPlaces.Add(place);


            await _appDbContext.SaveChangesAsync(cancellationToken);
            return place.Id;
        }








    }
}
