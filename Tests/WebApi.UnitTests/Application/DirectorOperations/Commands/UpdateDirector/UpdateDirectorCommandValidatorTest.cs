
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"a","b",true)]
        [InlineData(0,"a","bf",true)]
        [InlineData(0,"ae","b",true)]
        [InlineData(0,"ae","",true)]
        [InlineData(0,"","bf",true)]
        [InlineData(0,"","b",true)]
        [InlineData(0,"a","",true)]
        [InlineData(-1,"a","b",true)]
        [InlineData(-1,"a","bf",true)]
        [InlineData(-1,"ae","b",true)]
        [InlineData(-1,"ae","",true)]
        [InlineData(-1,"","bf",true)]
        [InlineData(-1,"","b",true)]
        [InlineData(-1,"a","",true)]
        [InlineData(1,"a","b",true)]
        [InlineData(1,"a","bf",true)]
        [InlineData(1,"ae","b",true)]
        [InlineData(1,"","b",true)]
        [InlineData(1,"a","",true)]
        public void WhenInvalidDirectorIdIsGiven_Validator_ShouldBeReturnErrors(int directorId,string name,string surname,bool isActive)
        {
            UpdateDirectorCommand commad = new UpdateDirectorCommand(null);
            commad.DirectorId = directorId;
            commad.Model = new UpdateDirectorViewModel{Name=name,Surname=surname,IsActive=isActive};
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
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
        public void WhenValidDirectorIdIsGiven_Validator_ShouldNotReturnError(int directorId,string name,string surname,bool isActive){
            UpdateDirectorCommand commad = new UpdateDirectorCommand(null);
            commad.DirectorId = directorId;
            commad.Model = new UpdateDirectorViewModel{Name=name,Surname=surname,IsActive=isActive};
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }

    }

}