using FluentAssertions;
using NUnit.Framework;
using Tourine.ServiceInterfaces;

namespace Tourine.Test
{
    public class RoleTest
    {
        [Test]
        public void ParsRole_should_return_result()
        {
            var roles = RoleExtentions.ParseRole<string>(Role.Admin | Role.Agency | Role.Operator);
            roles[0].Should().Be(Role.Admin.ToString());
            roles[1].Should().Be(Role.Operator.ToString());
            roles[2].Should().Be(Role.Agency.ToString());
        }
    }
}
