using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyDirectorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = _context.Directors.Max(x => x.Id)+111;
            command.Model = new UpdateDirectorViewModel { Name = "TestUpdateError", Surname = "DirectorUpdateError" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmen bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Director_ShouldBeUpdated(){
            var director = new Director { Name = "TestUpdate", Surname = "DirectorUpdateTest" };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            int id = _context.Directors.FirstOrDefault(a => a.Name==director.Name && a.Surname == director.Surname).Id;
            var model = new UpdateDirectorViewModel { Name="UpdateTest",Surname="UpdateDirectorTest"};
            command.DirectorId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedDirector = _context.Directors.FirstOrDefault(Director => Director.Name == model.Name && Director.Surname == model.Surname);
            updatedDirector.Should().NotBeNull();
            updatedDirector.Name.Should().Be(model.Name);
            updatedDirector.Surname.Should().Be(model.Surname);

        }
    }

}