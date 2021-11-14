using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Models
{
    public class MoviesActors 
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        public string character { get; set; }
        public int Order { get; set; }
        public Actor Actor { get; set; }
        public Movie Gender { get; set; }

    }
}
