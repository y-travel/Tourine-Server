using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Agencies
{
    public class UpdateAgencyValidator : AbstractValidator<UpdateAgency>
    {
        public UpdateAgencyValidator()
        {
            RuleFor(a => a.Agency.Id).NotEmpty();
            RuleFor(a => a.Agency.Name.Length).GreaterThanOrEqualTo(2);
            RuleFor(a => a.Agency.PhoneNumber).NotEmpty();
        }
    }
}
