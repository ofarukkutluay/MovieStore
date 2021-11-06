using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static  class Customers
    {
        public static void AddCustomers(this MovieStoreDbContext context){
            List<Customer> customers = new List<Customer>{
                    new Customer{
                        Name = "Omer",
                        Surname = "Kutluay",
                        Email = "omer@kutluay.com",
                        Password ="password"
                    },
                    new Customer{
                        Name = "Ali",
                        Surname = "Avare",
                        Email = "ali@avare.com",
                        Password ="password"
                    }
                };
            context.Customers.AddRange(customers);
        }
    }
}