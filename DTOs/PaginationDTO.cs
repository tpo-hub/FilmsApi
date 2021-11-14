using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        private int CountRegistersForPage = 10;
        
        private readonly int MaxCountRegisterForPage = 50;

        public int countRegistersForPage {
            
            get => CountRegistersForPage;
            
            set
            {
                CountRegistersForPage = (value > MaxCountRegisterForPage) ? MaxCountRegisterForPage : value;
            }
        }
    }
}
