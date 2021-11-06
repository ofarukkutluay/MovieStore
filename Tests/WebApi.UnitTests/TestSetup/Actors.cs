
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Actors{
        public static void AddActors(this MovieStoreDbContext context){
            List<Actor> actors = new List<Actor>{
                    new Actor
                    {
                        Name = "Daniel",
                        Surname = "Craig"
                    },
                    new Actor
                    {
                        Name = "Timothée",
                        Surname = "Chalamet"
                    },
                    new Actor
                    {
                        Name = "Jason",
                        Surname = "Momoa"
                    },
                    new Actor
                    {
                        Name = "Ana de",
                        Surname = "Armas"
                    }
                };
            context.Actors.AddRange(actors);
            
        }
    }
}