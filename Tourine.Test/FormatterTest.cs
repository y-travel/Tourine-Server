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
            (OptionType.Bus | OptionType.Food).GetMaskArray<OptionType>().Length.Should().Be(2);
        }

        [Test]
        [TestCase(OptionType.Bus | OptionType.Food, "با صندلی/غذا")]
        [TestCase(OptionType.Bus | OptionType.Room, "با تخت/صندلی")]
        [TestCase(OptionType.Bus | OptionType.Room | OptionType.Food, "")]
        [TestCase(OptionType.Empty, "بدون خدمات")]
        [TestCase(OptionType.Bus | OptionType.Food, "بدون تخت", true)]
        [TestCase(OptionType.Bus | OptionType.Room, "بدون غذا", true)]
        [TestCase(OptionType.Bus | OptionType.Room | OptionType.Food, "", true)]//critical option
        [TestCase(OptionType.Empty, "بدون خدمات", true)]//critical option
        public void GetDisplayTitle_should_be_return_correct_value(OptionType inputOptions, string expectedValue, bool reverse = false)
        {
            inputOptions.GetDisplayTitle(reverse).Should().BeEquivalentTo(expectedValue);
        }
    }
}
