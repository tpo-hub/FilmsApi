using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
