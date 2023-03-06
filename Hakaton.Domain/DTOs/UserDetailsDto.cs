using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.DTOs
{
    public class UserDetailsDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Biogram { get; set; }
        public int ScorePoints { get; set; }
        public IEnumerable<AchivementDto> Achivements { get; set; }
    }
}
