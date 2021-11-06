using System;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context){
            List<Movie> movies = new List<Movie>{
                    
                    new Movie
                    {
                        Title = "No Time To Die",
                        GenreId = 1,
                        ReleaseDate = 2021,
                        DirectorId = 2,
                        Price = 100.0m
                    },
                    new Movie
                    {
                        Title = "Dune: Part One",
                        GenreId = 1,
                        ReleaseDate = 2021,
                        DirectorId = 1,
                        Price = 110.0m
                    },
                    new Movie
                    {
                        Title = "Aquaman",
                        GenreId = 3,
                        ReleaseDate = 2018,
                        DirectorId = 3,
                        Price = 50.0m
                    }
                
                };
            context.Movies.AddRange(movies);
        }

    }
}