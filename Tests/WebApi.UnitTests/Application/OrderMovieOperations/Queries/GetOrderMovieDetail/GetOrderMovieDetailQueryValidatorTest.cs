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
    public class GetOrderMovieDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int orderId)
        {
            GetOrderMovieDetailQuery query = new GetOrderMovieDetailQuery(null,null);
            query.OrderId = orderId;

            GetOrderMovieDetailQueryValidator validator = new GetOrderMovieDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
        {
            GetOrderMovieDetailQuery Query = new GetOrderMovieDetailQuery(null,null);
            Query.OrderId = 1;

            GetOrderMovieDetailQueryValidator validator = new GetOrderMovieDetailQueryValidator();
            var result = validator.Validate(Query);

            result.Errors.Count.Should().Be(0);
        }
    }
}