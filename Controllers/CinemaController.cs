using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemaController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinemaController(ApplicationDbContext context, IMapper mapper)
            :base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CinemaDTO>>> Cinemas()
        {
            return await Get<Cinema, CinemaDTO>();
        }

        [HttpGet("{id}", Name = "GetCinema")]
        public async Task<ActionResult<CinemaDTO>> Cinema(int id)
        {
            return await Get<Cinema, CinemaDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaPostDTO cinemaPost)
        {
            return await Post<CinemaPostDTO, Cinema, CinemaDTO>(cinemaPost, "GetCinema");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaPostDTO cinemaPost)
        {
            return await Put<CinemaPostDTO, Cinema>(cinemaPost, id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Cinema>(id);
        }
    }
}
