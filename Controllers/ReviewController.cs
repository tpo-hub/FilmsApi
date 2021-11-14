using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Helpers;
using FilmsApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmsApi.Controllers
{
    [Route("api/movies/{movieId:int}/reviews")]
    [ServiceFilter(typeof(MovieExistAttribute))]
    public class ReviewController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReviewController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Reviews(int movieId, [FromQuery] PaginationDTO paginationDTO)
        {

            var queryable = context.Reviews.Include(x => x.User).AsQueryable();
            queryable = queryable.Where(x => x.MovieId == movieId);
            return await Get<Review, ReviewDTO>(paginationDTO, queryable);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int movieId, [FromBody] PostReviewDTO reviewDTO)
        {

            var user = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var reviewExist = await context.Reviews
                .AnyAsync(x => x.MovieId == movieId && x.UserId == user);

            if(reviewExist)
            {
                return BadRequest("The user already has review for this movie");
            }

            var reviewToAdd = mapper.Map<Review>(reviewDTO);
            reviewToAdd.MovieId = movieId;
            reviewToAdd.UserId = user;

            context.Add(reviewToAdd);
            await context.SaveChangesAsync();
            return NoContent();


        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult> Put(int id, [FromBody] PostReviewDTO reviewDTO)
        {

            var reviewDb = await context.Reviews.FirstOrDefaultAsync(x=> x.Id == id);
            
            if (reviewDb == null)
            {
                return BadRequest("The review does not exist");
            }

            var user = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if(reviewDb.UserId != user)
            {
                return Forbid("Only can put yours reviews");
            }

            reviewDb = mapper.Map(reviewDTO, reviewDb);

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {

            var reviewDb = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (reviewDb == null)
            {
                return BadRequest("The review does not exist");
            }

            var user = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (reviewDb.UserId != user)
            {
                return Forbid("Only can delete yours reviews");
            }

            context.Remove(reviewDb);
            await context.SaveChangesAsync();
            return NoContent();
        }



    }
}
