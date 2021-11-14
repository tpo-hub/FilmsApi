using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Models
{
    public class MoviesGenders 
    {
        public int GenderId { get; set; }
        public int MovieId { get; set; }
        public Gender Gender { get; set; }
        public Movie Movie { get; set; }
    }
}
