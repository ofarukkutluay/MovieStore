
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnErrors(int MovieId)
        {
            DeleteMovieCommand commad = new DeleteMovieCommand(null);
            commad.MovieId = MovieId;
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Validator_ShouldNotReturnError(){
            DeleteMovieCommand commad = new DeleteMovieCommand(null);
            commad.MovieId = 1;
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }

    }

}