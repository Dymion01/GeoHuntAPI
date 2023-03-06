using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.DTOs
{
    public class VisitedPlaceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoSrc { get; set; }
        public string Coordinates { get; set; }
    }
}
