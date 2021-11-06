using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyActorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = _context.Actors.Max(x => x.Id)+111;
            command.Model = new UpdateActorViewModel { Name = "TestUpdateError", Surname = "ActorUpdateError" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Aktör bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeUpdated(){
            var actor = new Actor { Name = "TestUpdate", Surname = "ActorUpdateTest" };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(_context);
            int id = _context.Actors.FirstOrDefault(a => a.Name==actor.Name && a.Surname == actor.Surname).Id;
            var model = new UpdateActorViewModel { Name="UpdateTest",Surname="UpdateActorTest"};
            command.ActorId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedActor = _context.Actors.FirstOrDefault(actor => actor.Name == model.Name && actor.Surname == model.Surname);
            updatedActor.Should().NotBeNull();
            updatedActor.Name.Should().Be(model.Name);
            updatedActor.Surname.Should().Be(model.Surname);

        }
    }

}