using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Blocks
{
    public class PutBlockValidator : AbstractValidator<PutBlock>
    {
        public PutBlockValidator()
        {
            RuleFor(b => b.Block.Id).NotEmpty();
            RuleFor(b => b.Block.Code).NotEmpty();
            RuleFor(b => b.Block.CustomerId).NotEmpty();
            RuleFor(b => b.Block.SubmitDate).NotEmpty();
            RuleFor(b => b.Block.TourId).NotEmpty();
            RuleFor(b => b.Block.Price).NotEmpty();
        }
    }
}