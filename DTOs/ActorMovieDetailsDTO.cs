using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class ActorMovieDetailsDTO
    {
        public int ActorId { get; set; }
        public string character { get; set; }
        public string ActorName { get; set; }
    }
}
