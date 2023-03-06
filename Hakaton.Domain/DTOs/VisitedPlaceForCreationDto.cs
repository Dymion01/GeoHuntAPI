using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.DTOs
{
    public class VisitedPlaceForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public int? PatentId { get; set; }
        public string PhotoSrc { get; set; }
    }
}
