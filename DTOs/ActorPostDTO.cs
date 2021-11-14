using FilmsApi.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class ActorPostDTO
    {
        [Required]
        [StringLength(123)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        [WeightFileValidation(MaxWeightMb: 10)]
        [TypeFileValidation(typesFilesGroup: TypesFilesGroup.Image)]
        public IFormFile Photho { get; set; }



    }
}
