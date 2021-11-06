using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteMovieActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = _context.Movies.Max(x => x.Id) + 111;
            var actorId = _context.Actors.Max(x => x.Id);

            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.ActorId = actorId;
            command.MovieId = movieId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Filmde tanımlı aktör bulunamadı!");

        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = _context.Movies.Max(x => x.Id);
            var actorId = _context.Actors.Max(x => x.Id) + 111;

            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.ActorId = actorId;
            command.MovieId = movieId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Filmde tanımlı aktör bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_MovieActor_ShouldBeDeleted()
        {
            var movie = new Movie { Title = "MovieActorTestTitle", ReleaseDate = 2002 };
            var actor = new Actor { Name = "MovieActorTestName", Surname = "MovieActorTestSurname" };
            _context.Movies.Add(movie);
            _context.Actors.Add(actor);
            _context.SaveChanges();
            var movieId = _context.Movies.FirstOrDefault(x => x.Title == movie.Title).Id;
            var actorId = _context.Actors.FirstOrDefault(x => x.Name == actor.Name && x.Surname == actor.Surname).Id;
            _context.MovieActors.Add(new MovieActor{MovieId=movieId,ActorId=actorId});
            _context.SaveChanges();

            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.ActorId = actorId;
            command.MovieId = movieId;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            var movieActor = _context.MovieActors.FirstOrDefault(x => x.MovieId == movieId && x.ActorId == actorId);
            movieActor.Should().BeNull();
        }

    }

}