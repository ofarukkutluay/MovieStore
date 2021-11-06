using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2).When(x=> x.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x=> x.Model.Surname.Trim() != string.Empty);
            RuleFor(command => command.DirectorId).GreaterThan(0);
        }
    }
}