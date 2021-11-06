using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.OrderMovieOperations.Commands.UpdateOrderMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.OrderMovieOperations.Commands.UpdateOrderMovie
{
    public class UpdateOrderMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateOrderMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }


        [Fact]
        public void WhenValidInputAreGiven_OrderMovie_ShouldBeUpdated(){
            var orderId = _context.OrderMovies.Max(x=> x.Id);
            UpdateOrderMovieCommand command = new UpdateOrderMovieCommand(_context);
            var model = new UpdateOrderMovieModel { IsVisible = false};
            command.OrderId = orderId;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var orderMovie = _context.OrderMovies.FirstOrDefault(x => x.Id == orderId);
            orderMovie.Should().NotBeNull();
            orderMovie.IsVisible.Should().Be(model.IsVisible);
           
        }
    }

}