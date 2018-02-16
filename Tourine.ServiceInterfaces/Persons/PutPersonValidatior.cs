using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Persons
{
    public class PutPersonValidatior : AbstractValidator<UpdatePerson>
    {
        public PutPersonValidatior()
        {
            RuleFor(p => p.Person.Id).NotEmpty();
            RuleFor(p => p.Person.BirthDate).NotEmpty();
            RuleFor(p => p.Person.Family).NotEmpty();
            RuleFor(p => p.Person.MobileNumber).NotEmpty();
            RuleFor(p => p.Person.Name).NotEmpty();
            RuleFor(p => p.Person.NationalCode).NotEmpty();
            RuleFor(p => p.Person.PassportExpireDate).NotEmpty();
            RuleFor(p => p.Person.PassportNo).NotEmpty();
        }
    }
}
