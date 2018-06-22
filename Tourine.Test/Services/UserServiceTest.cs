using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.FluentValidation.TestHelper;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class UserServiceTest : ServiceTest<UserService>
    {
        private readonly Person _person = new Person();
        private readonly User _user = new User { Password = "pass", Username = "username", Role = Role.Admin | Role.Agency };

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            CreateUsers();
        }

        [Test]
        public void GetUser_should_throw_exception()
        {
            new Action(() => MockService.Get(new GetUser { Id = Guid.NewGuid() }))
                .ShouldThrow<HttpError>().WithMessage(ErrorCode.UserNotFound.ToString());
        }

        [Test]
        public void GetUser_should_return_User()
        {
            var result = (User)MockService.Get(new GetUser { Id = _user.Id });
            result.Id.ShouldBeEquivalentTo(_user.Id);
            result.Roles.Should().Contain(Role.Admin);
            result.Roles.Should().Contain(Role.Agency);
        }

        [Test]
        public void PutUser_should_throw_exception()
        {
            new Action(() => MockService.Put( new PutUser {User = new User()}))
                .ShouldThrow<HttpError>().WithMessage(ErrorCode.UserNotFound.ToString());
        }

        [Test]
        public void PutUser_should_not_throw_exception()
        {
            new Action(()=> MockService.Put(new PutUser{User = _user}))
                .ShouldNotThrow();
        }

        public void CreateUsers()
        {
            _user.PersonId = _person.Id;
            InsertDb(_user, true);
            InsertDb(_person,true);
        }
    }

    public class UserValidatorTest
    {
        [Test]
        public void PostUser_should_throw_error()
        {
            var validator = new PostUserValidator();
            validator.ShouldHaveValidationErrorFor(x => x.User.Password, string.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.User.Username,string.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.User.PersonId, Guid.Empty);
        }
    }

}
