using FilmsApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmsApi
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>().HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<MoviesGenders>().HasKey(x => new { x.GenderId, x.MovieId });
           
            modelBuilder.Entity<MovieCinema>().HasKey(x => new { x.MovieId, x.CinemaId });
           
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder model)
        {
            var rolAdminId = "****************";
            var UserAdminId = "**************";
            var UserAdminName = "***********";

            var rolAdmin = new IdentityRole()
            {
                Id = rolAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            var UserAdmin = new IdentityUser()
            {
                Id = UserAdminId,
                UserName = UserAdminName,
                NormalizedUserName = UserAdminName,
                NormalizedEmail = UserAdminName,
                PasswordHash = passwordHasher.HashPassword(null, "abc123!")
            };

            model.Entity<IdentityUser>().HasData(UserAdmin);
            model.Entity<IdentityRole>().HasData(rolAdmin);
            model.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>()
            {
                Id = 1,
                ClaimType = ClaimTypes.Role,
                UserId = UserAdminId,
                ClaimValue = "Admin"
            });

        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenders> MoviesGenders { get; set; }
        public DbSet<MovieCinema> MovieCinemas { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
