using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class FilterMovieDTO
    {
        public int Page { get; set; }

        public int CountRegisterForPage { get; set; }

        public PaginationDTO Pagination {
            get {
                return new PaginationDTO()
                {
                    Page = Page,
                    countRegistersForPage = CountRegisterForPage
                };
            }
        }
        public string Title { get; set; }
        public int GenderId { get; set; }
        public bool InCinema { get; set; }

        public string Order { get; set; }
        public bool OrderAsc { get; set; } = true;

    }
}
