
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidActorIdIsGiven_Validator_ShouldBeReturnErrors(int actorId)
        {
            DeleteActorCommand commad = new DeleteActorCommand(null);
            commad.ActorId = actorId;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidActorIdIsGiven_Validator_ShouldNotReturnError(){
            DeleteActorCommand commad = new DeleteActorCommand(null);
            commad.ActorId = 1;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }

    }

}