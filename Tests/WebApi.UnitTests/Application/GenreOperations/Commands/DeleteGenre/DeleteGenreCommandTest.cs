using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotFoundGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            var newGenre = new Genre {Name="DeleteErrorGenre"};
            _context.Genres.Add(newGenre);
            _context.SaveChanges();
            var genre = _context.Genres.SingleOrDefault(a => a.Name==newGenre.Name);
            _context.Genres.Remove(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film türü bulunamadı!");

        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted(){
            var newGenre = new Genre {Name="DeleteGenre"};
            _context.Genres.Add(newGenre);
            _context.SaveChanges();
            var genre = _context.Genres.SingleOrDefault(a => a.Name==newGenre.Name);

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            var findGenre = _context.Genres.SingleOrDefault(a => a.Name==newGenre.Name);
            findGenre.Should().BeNull();

        }

    }
}
