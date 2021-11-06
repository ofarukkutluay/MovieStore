using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class CustomerFavoritGenres
    {
        public static void AddCustomerFavoritGenres(this MovieStoreDbContext context){
            List<CustomerFavoritGenre> customerFavoritGenres = new List<CustomerFavoritGenre>{
                    new CustomerFavoritGenre{
                        CustomerId = 1,
                        GenreId = 1
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 1,
                        GenreId = 2
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 1,
                        GenreId = 3
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 2,
                        GenreId = 1
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 2,
                        GenreId = 3
                    },
                };
            context.CustomerFavoritGenres.AddRange(customerFavoritGenres);
        }
    }
}