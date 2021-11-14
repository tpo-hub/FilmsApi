using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class ActorPatchDTO
    {
        [Required]
        [StringLength(123)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
