using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Blocks
{
    public class PostBlockValidator : AbstractValidator<PostBlock>
    {
        public PostBlockValidator()
        {
            RuleFor(b => b.Block.Code).NotEmpty();
            RuleFor(b => b.Block.CustomerId).NotEmpty();
            RuleFor(b => b.Block.SubmitDate).NotEmpty();
            RuleFor(b => b.Block.TourId).NotEmpty();
            RuleFor(b => b.Block.Price).NotEmpty();
        }
    }
}
