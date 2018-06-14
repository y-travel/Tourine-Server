using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    public class PutUserValidator : AbstractValidator<PutUser>
    {
        public PutUserValidator()
        {
            RuleFor(u => u.User.PersonId).NotEmpty();
            RuleFor(u => u.User.Password.Length).GreaterThanOrEqualTo(8);
            RuleFor(u => u.User.Username).NotEmpty();
            RuleFor(u => u.User.Role).NotEmpty();
        }
    }

    [Route("/user","PUT")]
    public class PutUser : IReturn
    {
        public User User { get; set; }
    }

    public class PostUserValidator : AbstractValidator<PostUser>
    {
        public PostUserValidator()
        {
            RuleFor(u => u.User.PersonId).NotEmpty();
            RuleFor(u => u.User.Password.Length).GreaterThanOrEqualTo(8);
            RuleFor(u => u.User.Username).NotEmpty();
            RuleFor(u => u.User.Role).NotEmpty();
        }
    }

    [Route("/user/","POST")]
    public class PostUser : IReturn<User>
    {
        public User User { get; set; }
    }

    [Route("/user/{Id}", "GET")]
    public class GetUser : IGet
    {
        public Guid Id { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }

        public Role Role { get; set; }

        [Ignore]
        public List<Role> Roles { get; set; }
    }
}
