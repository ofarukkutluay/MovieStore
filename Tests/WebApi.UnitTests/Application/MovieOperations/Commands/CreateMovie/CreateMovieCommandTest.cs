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
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyMovieNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movie = new Movie { Title = "TestMovieError",ReleaseDate= 2001 };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieViewModel { Title = movie.Title , ReleaseDate=movie.ReleaseDate};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Film zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Movie_ShouldBeCreated(){
            CreateMovieCommand command = new CreateMovieCommand(_context,_mapper);
            var model = new CreateMovieViewModel { Title="MovieTest" , ReleaseDate = 2001 , DirectorId = 1,GenreId=1 , Price = 150.0m};
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var movie = _context.Movies.FirstOrDefault(m => m.Title == model.Title && m.ReleaseDate == model.ReleaseDate);
            movie.Should().NotBeNull();
            movie.Title.Should().Be(model.Title);
            movie.ReleaseDate.Should().Be(model.ReleaseDate);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.GenreId.Should().Be(model.GenreId);
            movie.Price.Should().Be(model.Price);

        }
    }

}