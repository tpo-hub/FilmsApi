using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Controllers
{
    [ApiController]
    [Route("api/genders")]
    public class GenderController : CustomBaseController
    {

        public GenderController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>> Genders()
        {
            return await Get<Gender, GenderDTO>();
           
        }

        [HttpGet("{id}", Name = "GetGender")]
        public async Task<ActionResult<GenderDTO>> Gender(int id)
        {
            return await Get<Gender, GenderDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenderPostDTO genderPost)
        {
            return await Post<GenderPostDTO, Gender, GenderDTO>(genderPost, "GetGender");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenderPostDTO genderPut)
        {
            return  await Put<GenderPostDTO, Gender>(genderPut, id);
        } 
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Gender>(id);
        }


    }
}
