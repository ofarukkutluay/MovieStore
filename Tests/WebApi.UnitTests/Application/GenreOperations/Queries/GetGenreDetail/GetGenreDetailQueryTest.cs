using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(){
        
            int id = _context.Genres.Max(x => x.Id)+111;

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = id;

            FluentActions.Invoking(()=>query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film türü bulunamadı!");

        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeReturn(){
            var newGenre = new Genre {Name="GetGenreDetail"};
            _context.Genres.Add(newGenre);
            _context.SaveChanges();
            var Genre = _context.Genres.SingleOrDefault(a => a.Name==newGenre.Name );

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = Genre.Id;

            FluentActions.Invoking(()=>query.Handle()).Invoke();
            var findGenre = _context.Genres.SingleOrDefault(a => a.Id == Genre.Id);
            findGenre.Should().NotBeNull();
            findGenre.Name.Should().Be(newGenre.Name);
        }

    }
}
