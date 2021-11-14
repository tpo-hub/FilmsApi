using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Models;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();

            CreateMap<GenderPostDTO, Gender>();

            CreateMap<Cinema, CinemaDTO>()
               .ForMember(x => x.latitude, x => x.MapFrom(y => y.location.Y))
               .ForMember(x => x.latitude, x => x.MapFrom(x => x.location.X));

            CreateMap<Actor, ActorDTO>().ReverseMap();

            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            CreateMap<ActorPostDTO, Actor>()
                .ForMember(x => x.Photho, options => options.Ignore());

            CreateMap<PostMovieDTO, Movie>()
                .ForMember(x => x.MoviePoster, options => options.Ignore())
                .ForMember(x => x.MoviesGenders, options => options.MapFrom(MapMoviesGenders))
                .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));

            CreateMap<MoviePatchDTO, Movie>().ReverseMap();

            CreateMap<Movie, MovieDTO>().ReverseMap();

            CreateMap<Movie, MovieDetailsDTO>().ForMember(x=> x.Genders, options => options.MapFrom(MapGenders))
            .ForMember(x=> x.Actors, options => options.MapFrom(MapActors));

             var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<CinemaDTO, Cinema>().ForMember(x => x.location, x => x.MapFrom(
                 y => geometryFactory.CreatePoint(new Coordinate(y.latitude, y.longitude))));

            CreateMap<CinemaPostDTO, Cinema>().ForMember(x => x.location, x => x.MapFrom(
                 y => geometryFactory.CreatePoint(new Coordinate(y.latitude, y.longitude))));

            CreateMap<IdentityUser, UserDTO>();

            CreateMap<Review, ReviewDTO>().ForMember(x=> x.NameUser,x=> x.MapFrom(c=> c.User.UserName));
            CreateMap<ReviewDTO,Review>();

            CreateMap<PostReviewDTO,Review>();



        }

        private List<ActorMovieDetailsDTO> MapActors(Movie movie, MovieDetailsDTO detailsDTO)
        {
            var result = new List<ActorMovieDetailsDTO>();

            if (movie.MoviesActors == null)
            {
                return result;
            }

            foreach (var actor in movie.MoviesActors)
            {
                result.Add(new ActorMovieDetailsDTO() {ActorId = actor.ActorId, 
                    ActorName = actor.Actor.Name, character = actor.character});
            }

            return result;
        }

        private List<MoviesGenders> MapMoviesGenders(PostMovieDTO postMovie, Movie movie)
        {
            var result = new List<MoviesGenders>();

            if (postMovie.GendersIDs == null)
            {
                return result;
            }
            foreach(var id in postMovie.GendersIDs)
            {
                result.Add(new MoviesGenders() { GenderId = id });

            }
            return result;
        } 
        private List<MoviesActors> MapMoviesActors(PostMovieDTO postMovie, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (postMovie.Actors == null)
            {
                return result;
            }
            foreach(var actor in postMovie.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.ActorID, character = actor.character});

            }
            return result;
        }

        private List<GenderDTO> MapGenders(Movie movie, MovieDetailsDTO detailsDTO)
        {
            var result = new List<GenderDTO>();

            if(movie.MoviesGenders == null)
            {
                return result;
            }

            foreach(var gender in movie.MoviesGenders)
            {
               result.Add(new GenderDTO() { Id = gender.GenderId, Name = gender.Gender.Name});
            }

            return result;
        }
    }
}
