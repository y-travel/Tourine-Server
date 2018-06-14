using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Persons
{
    public class UpsertLeaderValidator : AbstractValidator<UpsertLeader>
    {
        public UpsertLeaderValidator()
        {
            RuleFor(p => p.Person.Id).NotEmpty().NotNull();
            RuleFor(p => p.Person.BirthDate).NotEmpty();
            RuleFor(p => p.Person.Family).NotEmpty().NotNull();
            RuleFor(p => p.Person.MobileNumber).NotEmpty();
            RuleFor(p => p.Person.Name).NotEmpty().NotNull();
            RuleFor(p => p.Person.NationalCode).NotEmpty();
        }
    }
}
