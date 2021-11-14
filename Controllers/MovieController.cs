using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Helpers;
using FilmsApi.Models;
using FilmsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStockerFile stockerFile;
        private readonly string container = "moviesposters";

        public MovieController(ApplicationDbContext context, IMapper mapper, IStockerFile stockerFile)
            :base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.stockerFile = stockerFile;
        }

        [HttpGet]
        public async Task<ActionResult<MoviesIndexDTO>> Movies()
        {
            var top = 5;
            var today = DateTime.Today;
            var premiereComingSoon = await context.Movies
                .Where(x => x.DateOfPremier > today)
                .OrderBy(x=> x.DateOfPremier)
                .Take(top).ToListAsync();

            var inCinema = await context.Movies.Where(x => x.InCinemas)
                .Take(top)
                .ToListAsync();

            var result = new MoviesIndexDTO();
            result.ComingSoonPremiere = mapper.Map<List<MovieDTO>>(premiereComingSoon);
            result.InCinemas = mapper.Map<List<MovieDTO>>(inCinema);

            return result;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> Filter([FromQuery] FilterMovieDTO filterMovie)
        {
            var moviesQueriyable = context.Movies.AsQueryable();

            if(!string.IsNullOrEmpty(filterMovie.Title))
            {
                moviesQueriyable = moviesQueriyable.Where(x => x.Title.Contains(filterMovie.Title));
            }

            if(filterMovie.InCinema)
            {
                moviesQueriyable = moviesQueriyable.Where(x=> x.InCinemas);
            }
           
            if(filterMovie.GenderId != 0)
            {
                moviesQueriyable = moviesQueriyable.Where(x => x.MoviesGenders.Select(c => c.GenderId)
                    .Contains(filterMovie.GenderId));      
            }

            if(!string.IsNullOrEmpty(filterMovie.Order))
            {
                if(filterMovie.Order == "Title")
                {
                    moviesQueriyable = moviesQueriyable.OrderBy(x => x.Title);
                }
            }

            await HttpContext.InsertPaginationParams(moviesQueriyable, filterMovie.CountRegisterForPage);

            var movies = await moviesQueriyable.Paginate(filterMovie.Pagination).ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<ActionResult<MovieDetailsDTO>> Movie(int id)
        {
            var movie = await context.Movies
                .Include(x=> x.MoviesActors)
                .ThenInclude(x=> x.Actor)
                .Include(x=> x.MoviesGenders)
                .ThenInclude(x=> x.Gender)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(movie == null)
            {
                return NotFound();
            }

            movie.MoviesActors.OrderBy(x => x.Order).ToList();

            var movieMap = mapper.Map<MovieDetailsDTO>(movie);

            return movieMap;

            
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] PostMovieDTO postMovie)
        {
            var toAddMovie = mapper.Map<Movie>(postMovie);

            if (postMovie.MoviePoster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await postMovie.MoviePoster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(postMovie.MoviePoster.FileName);

                    toAddMovie.MoviePoster = await stockerFile.StockFile(content, extension, container,
                       postMovie.MoviePoster.ContentType);

                };
            }
            AssignOrderforActors(toAddMovie);
            context.Add(toAddMovie);

            await context.SaveChangesAsync();

            var movieMap = mapper.Map<MovieDTO>(toAddMovie);

            return new CreatedAtRouteResult("GetMovie", new { id = movieMap.Id }, movieMap);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromForm] PostMovieDTO moviePut)
        {

            var movie = await context.Movies
                .Include(x=> x.MoviesActors)
                .Include(x=> x.MoviesGenders)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            movie = mapper.Map(moviePut, movie);

            if (moviePut.MoviePoster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await moviePut.MoviePoster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(moviePut.MoviePoster.FileName);

                    movie.MoviePoster = await stockerFile.EditFile(content, extension, container,
                                        movie.MoviePoster, moviePut.MoviePoster.ContentType);
                };
            }

            AssignOrderforActors(movie);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MoviePatchDTO> patchDocument)
        {
            return await Patch<Movie, MoviePatchDTO>(id, patchDocument);
        }

        [HttpDelete("{int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Movie>(id);
        }
            

        private void AssignOrderforActors(Movie movie)
        {
            if(movie.MoviesActors != null )
            {
                for(int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }

    }
}
