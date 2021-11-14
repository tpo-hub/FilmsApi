using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Models
{
    public class Gender : IId
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public List<MoviesGenders> MoviesGenders { get; set; }
    }
}
