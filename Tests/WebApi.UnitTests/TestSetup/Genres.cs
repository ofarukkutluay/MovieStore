
using System;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this MovieStoreDbContext context)
        {
            List<Genre> genres = new List<Genre>{
                    new Genre{
                        Name = "Action"
                    },
                    new Genre{
                        Name = "Adventure"
                    },
                    new Genre{
                        Name = "Fantacy"
                    }};

            context.Genres.AddRange(genres);
        }
    }

}