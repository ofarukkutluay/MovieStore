using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyActorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var actor = new Actor { Name = "TestError", Surname = "ActorError" };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = new CreateActorViewModel { Name = actor.Name, Surname = actor.Surname };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Aktör zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeCreated(){
            CreateActorCommand command = new CreateActorCommand(_context,_mapper);
            var model = new CreateActorViewModel { Name="Test",Surname="ActorTest"};
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var actor = _context.Actors.FirstOrDefault(actor => actor.Name == model.Name && actor.Surname == model.Surname);
            actor.Should().NotBeNull();
            actor.Name.Should().Be(model.Name);
            actor.Surname.Should().Be(model.Surname);

        }
    }

}