using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Models
{
    public class Actor: IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(123)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photho { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
    }
}
