using FilmsApi.Helpers;
using FilmsApi.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class PostMovieDTO
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime DateOfPremier { get; set; }

        [WeightFileValidation(MaxWeightMb: 10)]
        [TypeFileValidation(typesFilesGroup: TypesFilesGroup.Image)]
        public IFormFile MoviePoster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GendersIDs { get; set; } 
        
        [ModelBinder(BinderType = typeof(TypeBinder<List<MoviesActorsPostDTO>>))]
        public List<MoviesActorsPostDTO> Actors { get; set; }  
        
    }
}
