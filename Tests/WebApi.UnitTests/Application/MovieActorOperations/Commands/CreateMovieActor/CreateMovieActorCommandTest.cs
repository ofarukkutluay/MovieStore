using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieActorOperations.Commands.CreateMovieActor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = _context.Movies.Max(x => x.Id) + 111;
            var actorId = _context.Actors.Max(x => x.Id);

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel { MovieId = movieId , ActorId = actorId};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Film bulunamadı!");

        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = _context.Movies.Max(x => x.Id);
            var actorId = _context.Actors.Max(x => x.Id) + 111;

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel { ActorId = actorId ,MovieId = movieId};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Aktör bulunamadı!");
        }

        [Fact]
        public void WhenFoundInputAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = _context.Movies.Max(x => x.Id);
            var actorId = _context.Actors.Max(x => x.Id);
            var model = new MovieActor { ActorId = actorId, MovieId = movieId };
            _context.MovieActors.Add(model);
            _context.SaveChanges();

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel { ActorId = model.ActorId , MovieId = model.MovieId };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Filmde aktör tanımlı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_MovieActor_ShouldBeCreated()
        {
            var movie = new Movie { Title = "MovieActorTestTitle", ReleaseDate = 2002 };
            var actor = new Actor { Name = "MovieActorTestName", Surname = "MovieActorTestSurname" };
            _context.Movies.Add(movie);
            _context.Actors.Add(actor);
            _context.SaveChanges();
            var movieId = _context.Movies.FirstOrDefault(x => x.Title == movie.Title).Id;
            var actorId = _context.Actors.FirstOrDefault(x => x.Name == actor.Name && x.Surname == actor.Surname).Id;

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel { ActorId = actorId, MovieId = movieId };

            FluentActions.Invoking(() => command.Handle()).Invoke();
            var movieActor = _context.MovieActors.FirstOrDefault(x => x.MovieId == movieId && x.ActorId == actorId);
            movieActor.Should().NotBeNull();
            movieActor.ActorId.Should().Be(actorId);
            movieActor.MovieId.Should().Be(movieId);
        }

    }

}