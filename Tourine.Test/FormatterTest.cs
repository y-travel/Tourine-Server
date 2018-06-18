using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.Test
{
    public class FormatterTest
    {
        [Test]
        public void MaskToList_should_be_return_correct_list()
        {
            var optionType = OptionType.Bus | OptionType.Food;
            optionType.MaskToList<OptionType>();//.Count().Should().Be(2);
        }
    }
}
