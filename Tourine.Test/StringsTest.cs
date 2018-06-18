using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.Test
{
    public class StringsTest
    {
        [Test]
        public void Loc_should_be_return_all_Strings_constants()
        {
            var items = typeof(Strings).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => x.Name).ToList();
            items.All(x => x.Loc() != x).Should().BeTrue();
        }
    }
}
