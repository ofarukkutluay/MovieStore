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
    public class CreateOrderMovieCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0, 0, -1)]
        [InlineData(-1, 0, -1)]
        [InlineData(0, -1, -1)]
        [InlineData(-1, -1, -1)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, -1)]
        [InlineData(0, 1, -1)]
        [InlineData(1, 1, -1)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int movieId, int customerId, decimal purchasedPrice)
        {
            CreateOrderMovieCommand command = new CreateOrderMovieCommand(null, null);
            command.Model = new CreateOrderMovieModel
            {
                MovieId = movieId,
                CustomerId = customerId,
                PurchasedPrice = purchasedPrice
            };

            CreateOrderMovieCommandValidator validator = new CreateOrderMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
        {
            CreateOrderMovieCommand command = new CreateOrderMovieCommand(null, null);
            command.Model = new CreateOrderMovieModel
            {
                CustomerId = 1,
                MovieId = 1,
                PurchasedPrice = 10.0m
            };

            CreateOrderMovieCommandValidator validator = new CreateOrderMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}