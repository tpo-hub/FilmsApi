using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Coment { get; set; }
        public int Punctuation { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string NameUser { get; set; }
    }
}
