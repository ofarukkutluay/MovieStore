using System;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class OrderMovies
    {
        public static void AddOrderMovies(this MovieStoreDbContext context){
            List<OrderMovie> orderMovies = new List<OrderMovie>{
                    new OrderMovie{
                        MovieId=1,
                        CustomerId=1,
                        PurchasedDate = new DateTime(2020,12,01),
                        PurchasedPrice = 90
                    },
                    new OrderMovie{
                        MovieId=3,
                        CustomerId=1,
                        PurchasedDate = new DateTime(2020,12,02),
                        PurchasedPrice = 105
                    },
                    new OrderMovie{
                        MovieId=2,
                        CustomerId=2,
                        PurchasedDate = new DateTime(2020,12,02),
                        PurchasedPrice = 120
                    }
                };
            context.OrderMovies.AddRange(orderMovies);
        }
    }
}