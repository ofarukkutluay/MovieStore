using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundDirectorIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            var newDirector = new Director {Name="DeleteErrorDirector", Surname="DeleteErrorDirectorTest"};
            _context.Directors.Add(newDirector);
            _context.SaveChanges();
            var Director = _context.Directors.SingleOrDefault(a => a.Name==newDirector.Name && a.Surname==newDirector.Surname);
            _context.Directors.Remove(Director);
            _context.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = Director.Id;

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı!");

        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldBeDeleted(){
            var newDirector = new Director {Name="DeleteDirector", Surname="DeleteDirectorTest"};
            _context.Directors.Add(newDirector);
            _context.SaveChanges();
            var Director = _context.Directors.SingleOrDefault(a => a.Name==newDirector.Name && a.Surname==newDirector.Surname);

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = Director.Id;

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            var findDirector = _context.Directors.SingleOrDefault(a => a.Name==newDirector.Name && a.Surname==newDirector.Surname);
            findDirector.Should().BeNull();

        }

    }
}
