using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class GenderPostDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
