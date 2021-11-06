
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "a", -1, 0, 0, 1500)]
        [InlineData(0, "", -1, 0, 0, 1500)]
        [InlineData(0, "a", 0, 1, 1, 1501)]
        [InlineData(0, "", 0, 1, 1, 1501)]
        [InlineData(0, "ab", -1, 1, 1, 1501)]
        [InlineData(0, "ab", 0, 0, 1, 1501)]
        [InlineData(0, "ab", 0, -1, 1, 1501)]
        [InlineData(0, "ab", 0, 1, 0, 1501)]
        [InlineData(0, "ab", 0, 1, -1, 1501)]
        [InlineData(0, "ab", 0, 1, 1, 1500)]
        [InlineData(0, "ab", 0, 1, 1, 4000)]
        [InlineData(-1, "a", -1, 0, 0, 1500)]
        [InlineData(-1, "", -1, 0, 0, 1500)]
        [InlineData(-1, "a", 0, 1, 1, 1501)]
        [InlineData(-1, "", 0, 1, 1, 1501)]
        [InlineData(-1, "ab", -1, 1, 1, 1501)]
        [InlineData(-1, "ab", 0, 0, 1, 1501)]
        [InlineData(-1, "ab", 0, -1, 1, 1501)]
        [InlineData(-1, "ab", 0, 1, 0, 1501)]
        [InlineData(-1, "ab", 0, 1, -1, 1501)]
        [InlineData(-1, "ab", 0, 1, 1, 1500)]
        [InlineData(-1, "ab", 0, 1, 1, 5000)]
        [InlineData(1, "a", -1, 0, 0, 1500)]
        [InlineData(1, "", -1, 0, 0, 1500)]
        [InlineData(1, "a", 0, 1, 1, 1501)]
        [InlineData(1, "ab", -1, 1, 1, 1501)]
        [InlineData(1, "ab", 0, 0, 1, 1501)]
        [InlineData(1, "ab", 0, -1, 1, 1501)]
        [InlineData(1, "ab", 0, 1, 0, 1501)]
        [InlineData(1, "ab", 0, 1, -1, 1501)]
        [InlineData(1, "ab", 0, 1, 1, 1500)]
        [InlineData(1, "ab", 0, 1, 1, 5000)]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnErrors(int movieId, string title, decimal price, int genreId, int directorId, int releaseDate)
        {
            UpdateMovieCommand commad = new UpdateMovieCommand(null);
            commad.MovieId = movieId;
            commad.Model = new UpdateMovieViewModel
            {
                Title = title,
                Price = price,
                GenreId = genreId,
                DirectorId = directorId,
                ReleaseDate = releaseDate
            };
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1, "", 0, 1, 1, 1501)]
        [InlineData(1, "ab", 0, 1, 1, 1501)]
        public void WhenValidMovieIdIsGiven_Validator_ShouldNotReturnError(int movieId, string title, decimal price, int genreId, int directorId, int releaseDate)
        {
            UpdateMovieCommand commad = new UpdateMovieCommand(null);
            commad.MovieId = movieId;
            commad.Model = new UpdateMovieViewModel
            {
                Title = title,
                Price = price,
                GenreId = genreId,
                DirectorId = directorId,
                ReleaseDate = releaseDate
            };
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }

    }

}