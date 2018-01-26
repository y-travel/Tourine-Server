using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.PassengerList
{
    public class ChangePassengerFromBlockValidator : AbstractValidator<ChangePassengerFromBlock>
    {
        public ChangePassengerFromBlockValidator()
        {
            RuleFor(p => p.PassengerBlock.BlockId).NotEmpty();
            RuleFor(p => p.PassengerBlock.PassengerId).NotEmpty();
            RuleFor(p => p.PassengerBlock.Id).NotEmpty();
        }
    }
}
