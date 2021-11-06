using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            var newActor = new Actor {Name="DeleteErrorActor", Surname="DeleteErrorActorTest"};
            _context.Actors.Add(newActor);
            _context.SaveChanges();
            var actor = _context.Actors.SingleOrDefault(a => a.Name==newActor.Name && a.Surname==newActor.Surname);
            _context.Actors.Remove(actor);
            _context.SaveChanges();

            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actor.Id;

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör bulunamadı!");

        }

        [Fact]
        public void WhenValidActorIdIsGiven_Actor_ShouldBeDeleted(){
            var newActor = new Actor {Name="DeleteActor", Surname="DeleteActorTest"};
            _context.Actors.Add(newActor);
            _context.SaveChanges();
            var actor = _context.Actors.SingleOrDefault(a => a.Name==newActor.Name && a.Surname==newActor.Surname);

            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actor.Id;

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            var findActor = _context.Actors.SingleOrDefault(a => a.Name==newActor.Name && a.Surname==newActor.Surname);
            findActor.Should().BeNull();

        }

    }
}
