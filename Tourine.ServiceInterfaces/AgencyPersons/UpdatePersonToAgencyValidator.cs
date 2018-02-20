using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.AgencyPersons
{
    public class UpdatePersonToAgencyValidator : AbstractValidator<UpdatePersonToAgency>
    {
        public UpdatePersonToAgencyValidator()
        {
            RuleFor(a => a.AgencyId).NotEmpty();
            RuleFor(a => a.PersonId).NotEmpty();
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
