using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyMovieNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = _context.Movies.Max(x => x.Id)+111;
            command.Model = new UpdateMovieViewModel { Title = "TestUpdateMovieError"};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Film bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Movie_ShouldBeUpdated(){
            var Movie = new Movie { Title = "TestMovieUpdate", ReleaseDate = 2001 , DirectorId = 1,GenreId=1 , Price = 150.0m};
            _context.Movies.Add(Movie);
            _context.SaveChanges();

            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            int id = _context.Movies.FirstOrDefault(a => a.Title==Movie.Title ).Id;
            var model = new UpdateMovieViewModel { Title="UpdateTestMovie"};
            command.MovieId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedMovie = _context.Movies.FirstOrDefault(Movie => Movie.Title == model.Title);
            updatedMovie.Should().NotBeNull();
            updatedMovie.Title.Should().Be(model.Title);
        }
    }

}