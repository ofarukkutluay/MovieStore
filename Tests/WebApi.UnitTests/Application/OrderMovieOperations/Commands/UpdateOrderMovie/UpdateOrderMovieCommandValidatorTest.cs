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
    public class UpdateOrderMovieCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int orderId,bool isVisible)
        {
            UpdateOrderMovieCommand command = new UpdateOrderMovieCommand(null);
            command.OrderId = orderId;
            command.Model = new UpdateOrderMovieModel
            {
                IsVisible = isVisible
            };

            UpdateOrderMovieCommandValidator validator = new UpdateOrderMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateOrderMovieCommand command = new UpdateOrderMovieCommand(null);
            command.OrderId = 1;
            command.Model = new UpdateOrderMovieModel
            {
                IsVisible = false
            };

            UpdateOrderMovieCommandValidator validator = new UpdateOrderMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}