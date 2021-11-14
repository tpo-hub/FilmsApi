using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Models
{
    public class Review : IId
    {
        public int Id { get ; set; }
        public string Coment { get; set; }
        [Range(1,5)]
        public int Punctuation { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
