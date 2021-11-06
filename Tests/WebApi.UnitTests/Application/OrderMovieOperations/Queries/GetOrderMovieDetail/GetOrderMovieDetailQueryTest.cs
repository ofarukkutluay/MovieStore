using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.OrderMovieOperations.Queries.GetOrderMovieDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.OrderMovieOperations.Queries.GetOrderMovieDetail
{
    public class GetOrderMovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderMovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputAreGiven_InvalidOperationException_ShouldBeReturn(){
            var orderId = _context.OrderMovies.Max(x=> x.Id)+111;
            GetOrderMovieDetailQuery query = new GetOrderMovieDetailQuery(_context,_mapper);
            query.OrderId = orderId;

            FluentActions
                .Invoking(()=>query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Satın alma bulunamadı!");
           
        }


        [Fact]
        public void WhenValidInputAreGiven_OrderMovie_ShouldBeReturn(){
            var orderId = _context.OrderMovies.Max(x=> x.Id);
            GetOrderMovieDetailQuery query = new GetOrderMovieDetailQuery(_context,_mapper);
            query.OrderId = orderId;

            FluentActions
                .Invoking(()=>query.Handle()).Invoke();

            var orderMovie = _context.OrderMovies.FirstOrDefault(x => x.Id == orderId);
            orderMovie.Should().NotBeNull();
           
        }
    }

}