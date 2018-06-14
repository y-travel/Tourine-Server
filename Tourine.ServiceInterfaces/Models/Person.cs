using System;
using ServiceStack;
using ServiceStack.FluentValidation;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    public class UpsertLeaderValidator : AbstractValidator<UpsertLeader>
    {
        public UpsertLeaderValidator()
        {
            RuleFor(p => p.Person.Id).NotEmpty().NotNull();
            RuleFor(p => p.Person.BirthDate).NotEmpty();
            RuleFor(p => p.Person.Family).NotEmpty().NotNull();
            RuleFor(p => p.Person.MobileNumber).NotEmpty();
            RuleFor(p => p.Person.Name).NotEmpty().NotNull();
            RuleFor(p => p.Person.NationalCode).NotEmpty();
        }
    }

    [Route("/persons/leaders", "POST")]
    public class UpsertLeader : IReturn<Person>
    {
        public Person Person { get; set; }
    }

    public class UpdatePersonValidatior : AbstractValidator<UpdatePerson>
    {
        public UpdatePersonValidatior()
        {
            RuleFor(p => p.Person.Id).NotEmpty().NotNull();
            RuleFor(p => p.Person.BirthDate).NotEmpty();
            RuleFor(p => p.Person.Family).NotEmpty().NotNull();
            RuleFor(p => p.Person.MobileNumber).NotEmpty();
            RuleFor(p => p.Person.Name).NotEmpty().NotNull();
            RuleFor(p => p.Person.NationalCode).NotEmpty();
        }
    }

    [Route("/persons/", "PUT")]
    public class UpdatePerson : IReturn<Person>
    {
        public Person Person { get; set; }
    }

    [Route("/persons/", "GET")]
    [Route("/persons/{id}", "GET")]
    public class GetPersons : QueryDb<Person>
    {
        public Guid? Id { get; set; }
    }

    [Route("/persons/leaders","GET")]
    public class GetLeaders : QueryDb<Person>
    {
    }

    [Route("/persons/current","GET")]
    public class GetCurrentPerson : IReturn<Person>
    {
    }

    [Route("/persons/search/{Str}/agency{AgencyId}/", "GET")]
    [Route("/persons/search/{Str}", "GET")]
    public class FindPersonInAgency : QueryDb<Person> , IJoin<Person, AgencyPerson>
    {
        public Guid? AgencyId { get; set; }
        public string Str { get; set; }
    }

    [Route("/persons/nc/{NationalCode}", "GET")]
    public class FindPersonFromNc : IReturn<Person>
    {
        public string NationalCode { get; set; }
    }

    [Route("/persons/{ID}", "DELETE")]
    public class DeletePerson : IReturn
    {
        public Guid Id { get; set; }
    }

    public class AddNewPersonValidator : AbstractValidator<AddNewPerson>
    {
        public AddNewPersonValidator()
        {
            RuleFor(p => p.Person.BirthDate).NotEmpty();
            RuleFor(p => p.Person.Family).NotEmpty();
            RuleFor(p => p.Person.MobileNumber).NotEmpty();
            RuleFor(p => p.Person.Name).NotEmpty();
            RuleFor(p => p.Person.NationalCode).NotEmpty();
        }
    }

    [Route("/persons/leaders/{Id}","DELETE")]
    public class DeleteLeader : IReturnVoid
    {
        public Guid Id { get; set; }
    }

    [Route("/persons/", "POST")]
    public class AddNewPerson : IReturn<Person>
    {
        public Person Person { get; set; }
    }

    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Family { get; set; }
        public string EnglishName { get; set; }
        public string EnglishFamily { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? PassportExpireDate { get; set; }
        public DateTime? VisaExpireDate { get; set; }
        public string PassportNo { get; set; }
        public bool Gender { get; set; }
        public PersonType Type { get; set; } = PersonType.Passenger;
        public string SocialNumber { get; set; }
        public long? ChatId { get; set; }

        public bool IsUnder5 { get; set; }
        public bool IsInfant { get; set; }
    }

    public static class PersonExtention
    {
        public static int CalculateAge(this Person person, DateTime toYear)
        {
            var birthdate = DateTime.Parse(person.BirthDate.ToString());
            var age = toYear.Year - birthdate.Year;
            if (birthdate > toYear.AddYears(-age)) age--;
            return age;
        }
    }
}
