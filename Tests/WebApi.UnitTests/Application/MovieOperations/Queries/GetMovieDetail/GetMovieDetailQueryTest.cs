using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn(){
        
            int id = _context.Movies.Max(x => x.Id)+111;

            GetMovieDetailQuery query = new GetMovieDetailQuery(_context,_mapper);
            query.MovieId = id;

            FluentActions.Invoking(()=>query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");

        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeReturn(){
            var newMovie = new Movie {Title="GetMovieDetail", ReleaseDate = 2001 , DirectorId = 1,GenreId=1 , Price = 150.0m};
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            var Movie = _context.Movies.SingleOrDefault(a => a.Title==newMovie.Title );

            GetMovieDetailQuery query = new GetMovieDetailQuery(_context,_mapper);
            query.MovieId = Movie.Id;

            FluentActions.Invoking(()=>query.Handle()).Invoke();
            var findMovie = _context.Movies.SingleOrDefault(a => a.Id == Movie.Id);
            findMovie.Should().NotBeNull();
            findMovie.Title.Should().Be(newMovie.Title);
            findMovie.ReleaseDate.Should().Be(newMovie.ReleaseDate);
            findMovie.DirectorId.Should().Be(newMovie.DirectorId);
            findMovie.GenreId.Should().Be(newMovie.GenreId);
            findMovie.Price.Should().Be(newMovie.Price);


            
        }

    }
}
