using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class MovieActors
    {
        public static void AddMovieActors(this MovieStoreDbContext context){
            List<MovieActor> movieActors = new List<MovieActor>{
                    new MovieActor
                    {
                        ActorId = 1,
                        MovieId = 1
                    },
                    new MovieActor
                    {
                        ActorId = 4,
                        MovieId = 1
                    },
                    new MovieActor
                    {
                        ActorId = 2,
                        MovieId = 2
                    },
                    new MovieActor
                    {
                        ActorId = 3,
                        MovieId = 3
                    }
                };
            context.MovieActors.AddRange(movieActors);
        }
    }
}