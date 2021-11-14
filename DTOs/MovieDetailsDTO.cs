using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class MovieDetailsDTO: MovieDTO
    {
        public List<GenderDTO> Genders { get; set; }
        public List<ActorMovieDetailsDTO> Actors { get; set; }
    }
}
