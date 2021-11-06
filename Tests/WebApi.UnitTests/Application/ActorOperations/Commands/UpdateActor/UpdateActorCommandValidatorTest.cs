
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"a","b")]
        [InlineData(0,"a","bf")]
        [InlineData(0,"ae","b")]
        [InlineData(0,"ae","")]
        [InlineData(0,"","bf")]
        [InlineData(0,"","b")]
        [InlineData(0,"a","")]
        [InlineData(-1,"a","b")]
        [InlineData(-1,"a","bf")]
        [InlineData(-1,"ae","b")]
        [InlineData(-1,"ae","")]
        [InlineData(-1,"","bf")]
        [InlineData(-1,"","b")]
        [InlineData(-1,"a","")]
        [InlineData(1,"a","b")]
        [InlineData(1,"a","bf")]
        [InlineData(1,"ae","b")]
        [InlineData(1,"","b")]
        [InlineData(1,"a","")]
        public void WhenInvalidActorIdIsGiven_Validator_ShouldBeReturnErrors(int actorId,string name,string surname)
        {
            UpdateActorCommand commad = new UpdateActorCommand(null);
            commad.ActorId = actorId;
            commad.Model = new UpdateActorViewModel{Name=name,Surname=surname};
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,"up","up",true)]
        [InlineData(1,"","",true)]
        [InlineData(1,"up","",true)]
        [InlineData(1,"","up",true)]
        [InlineData(1,"up","up",false)]
        [InlineData(1,"","",false)]
        [InlineData(1,"up","",false)]
        [InlineData(1,"","up",false)]
        public void WhenValidActorIdIsGiven_Validator_ShouldNotReturnError(int actorId,string name,string surname,bool isActive){
            UpdateActorCommand commad = new UpdateActorCommand(null);
            commad.ActorId = actorId;
            commad.Model = new UpdateActorViewModel{Name=name,Surname=surname,IsActive=isActive};
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }

    }

}