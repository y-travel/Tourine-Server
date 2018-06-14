using System;
using ServiceStack;
using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Models
{
    public class UpdateDestinationValidator : AbstractValidator<UpdateDestination>
    {
        public UpdateDestinationValidator()
        {
            RuleFor(d => d.Destination.Id).NotEmpty();
            RuleFor(d => d.Destination.Name.Length).GreaterThanOrEqualTo(2);
        }
    }

    [Route("/destination/", "PUT")]
    public class UpdateDestination : IReturn
    {
        public Destination Destination { get; set; }
    }

    [Route("/destinations/{Id}", "GET")]
    [Route("/destinations", "GET")]
    public class GetDestinations : QueryDb<Destination>
    {
        public Guid? Id { get; set; }
    }

    public class CreateDestinationValidator : AbstractValidator<CreateDestination>
    {
        public CreateDestinationValidator()
        {
            RuleFor(d => d.Destination.Name.Length).GreaterThanOrEqualTo(2);
        }
    }

    [Route("/destination/", "POST")]
    public class CreateDestination : IReturn<Destination>
    {
        public Destination Destination { get; set; }
    }

    public class Destination
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }    
    }
}
