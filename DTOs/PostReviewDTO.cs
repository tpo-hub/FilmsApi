using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class PostReviewDTO
    {
        public string Coment { get; set; }
        [Range(1,5)]
        public int Punctuation { get; set; }
    }
}
