using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("a",-1,0,0,1500)]
        [InlineData("",-1,0,0,1500)]
        [InlineData("a",0,1,1,1501)]
        [InlineData("",0,1,1,1501)]
        [InlineData("ab",-1,1,1,1501)]
        [InlineData("ab",0,0,1,1501)]
        [InlineData("ab",0,-1,1,1501)]
        [InlineData("ab",0,1,0,1501)]
        [InlineData("ab",0,1,-1,1501)]
        [InlineData("ab",0,1,1,1500)]
        [InlineData("ab",0,1,1,5000)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title,decimal price,int genreId,int directorId,int releaseDate){
            CreateMovieCommand command = new CreateMovieCommand(null,null);
            command.Model = new CreateMovieViewModel{
                Title = title,
                Price = price,
                GenreId = genreId,
                DirectorId = directorId,
                ReleaseDate = releaseDate
            };

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError(){
            CreateMovieCommand command = new CreateMovieCommand(null,null);
            command.Model = new CreateMovieViewModel{
                Title = "ValidMovie",
                Price = 10,
                GenreId = 1,
                DirectorId = 1,
                ReleaseDate = 1870
            };

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}