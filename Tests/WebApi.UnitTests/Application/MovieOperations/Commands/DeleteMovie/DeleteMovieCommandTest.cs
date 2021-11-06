using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            var newMovie = new Movie {Title="DeleteErrorMovie"};
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            var movie = _context.Movies.SingleOrDefault(a => a.Title==newMovie.Title);
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = movie.Id;

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");

        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeDeleted(){
            var newMovie = new Movie {Title="DeleteMovie"};
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            var Movie = _context.Movies.SingleOrDefault(a => a.Title==newMovie.Title);

            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = Movie.Id;

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            var findMovie = _context.Movies.SingleOrDefault(a => a.Title==newMovie.Title);
            findMovie.Should().BeNull();

        }

    }
}
