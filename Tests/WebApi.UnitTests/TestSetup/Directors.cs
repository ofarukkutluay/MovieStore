
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Directors{
        public static void AddDirectors(this MovieStoreDbContext context){
            List<Director> directors = new List<Director>{
                    new Director
                    {
                        Name = "Denis",
                        Surname = "Villeneuve"
                    },
                    new Director
                    {
                        Name = "Cary Joji",
                        Surname = "Fukunaga"
                    },
                    new Director
                    {
                        Name = "James",
                        Surname = " Wan"
                    }
                };
            context.Directors.AddRange(directors);
            
        }
    }
}