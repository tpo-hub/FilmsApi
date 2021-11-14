using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class MoviesIndexDTO
    {
        public List<MovieDTO> ComingSoonPremiere { get; set; }
        public List<MovieDTO> InCinemas { get; set; }
    }
}
