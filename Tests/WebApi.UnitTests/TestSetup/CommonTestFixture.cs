

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDB").Options;
            Context = new MovieStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddMovies();
            Context.AddGenres();
            Context.AddActors();
            Context.AddDirectors();
            Context.AddMovieActors();
            Context.AddOrderMovies();
            Context.AddCustomerFavoritGenres();
            Context.AddCustomers();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();


        }

    }

}