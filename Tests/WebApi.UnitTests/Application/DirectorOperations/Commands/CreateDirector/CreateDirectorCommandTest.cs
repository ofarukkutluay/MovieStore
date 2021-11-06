using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyDirectorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Director { Name = "TestError", Surname = "DirectorError" };
            _context.Directors.Add(director);
            _context.SaveChanges();

            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            command.Model = new CreateDirectorViewModel { Name = director.Name, Surname = director.Surname };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmen zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Director_ShouldBeCreated(){
            CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper);
            var model = new CreateDirectorViewModel { Name="Test",Surname="DirectorTest"};
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var director = _context.Directors.FirstOrDefault(Director => Director.Name == model.Name && Director.Surname == model.Surname);
            director.Should().NotBeNull();
            director.Name.Should().Be(model.Name);
            director.Surname.Should().Be(model.Surname);

        }
    }

}