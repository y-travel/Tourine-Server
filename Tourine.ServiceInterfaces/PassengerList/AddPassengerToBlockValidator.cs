using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.PassengerList
{
    public class AddPassengerToBlockValidator : AbstractValidator<AddPassengerToBlock>
    {
        public AddPassengerToBlockValidator()
        {
            RuleFor(p => p.PassengerList.BlockId).NotEmpty();
            RuleFor(p => p.PassengerList.PassengerId).NotEmpty();
        }
    }
}