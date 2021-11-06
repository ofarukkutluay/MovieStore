using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = _context.Genres.Max(x => x.Id)+111;
            command.Model = new UpdateGenreModel { Name = "TestUpdateGenreError"};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmen bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeUpdated(){
            var genre = new Genre { Name = "TestGenreUpdate"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            int id = _context.Genres.FirstOrDefault(a => a.Name==genre.Name ).Id;
            var model = new UpdateGenreModel { Name="UpdateTestGenre"};
            command.GenreId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedGenre = _context.Genres.FirstOrDefault(genre => genre.Name == model.Name);
            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(model.Name);

        }
    }

}