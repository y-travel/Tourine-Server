using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Agencies
{
    public class CreateAgencyValidator : AbstractValidator<CreateAgency>
    {
        public CreateAgencyValidator()
        {
            RuleFor(agency => agency.Agency.Name.Length).GreaterThanOrEqualTo(2);
            RuleFor(agency => agency.Agency.PhoneNumber).NotEmpty();
        }
    }
}
