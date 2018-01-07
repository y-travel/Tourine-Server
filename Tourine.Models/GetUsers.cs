using System;
using System.Runtime.Serialization;
using ServiceStack;

namespace Tourine.Models
{
    [DataContract]
    [Route("/customer/users", "GET")]
    public class GetUsers : QueryDb<User,UserInfo>, IGet
    {
        [DataMember(Name = "UserId")]
        public Guid Id { get; set; }
        [IgnoreDataMember]
        public string Name { get; set; }
    }
}
