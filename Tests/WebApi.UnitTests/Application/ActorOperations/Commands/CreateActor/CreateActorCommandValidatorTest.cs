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
    public class CreateActorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("a","b")]
        [InlineData("a","bf")]
        [InlineData("ae","b")]
        [InlineData("ae","")]
        [InlineData("","bf")]
        [InlineData("","b")]
        [InlineData("a","")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name,string surname){
            CreateActorCommand command = new CreateActorCommand(null,null);
            command.Model = new CreateActorViewModel{
                Name = name,
                Surname = surname
            };

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError(){
            CreateActorCommand command = new CreateActorCommand(null,null);
            command.Model = new CreateActorViewModel{
                Name = "ValidActor",
                Surname = "ValidActorTest"
            };

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}