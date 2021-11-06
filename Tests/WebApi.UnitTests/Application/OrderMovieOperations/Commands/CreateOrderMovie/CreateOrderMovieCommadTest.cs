using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.OrderMovieOperations.Commands.CreateOrderMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.OrderMovieOperations.Commands.CreateOrderMovie
{
    public class CreateOrderMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenValidInputAreGiven_OrderMovie_ShouldBeCreated(){
            CreateOrderMovieCommand command = new CreateOrderMovieCommand(_context,_mapper);
            var model = new CreateOrderMovieModel { CustomerId= 1,MovieId=1,PurchasedPrice=10.0m};
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var orderMovie = _context.OrderMovies.FirstOrDefault(x => x.MovieId == model.MovieId && x.CustomerId == model.CustomerId && x.PurchasedPrice == model.PurchasedPrice);
            orderMovie.Should().NotBeNull();
           

        }
    }

}