using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Peak_Performance.Controllers;

namespace Peak_Performance_Test
{
    [TestFixture]
    internal class ExampleTest
    {
        [Test]
        public void HomeController_Capitolizes_ReturnsCapitolizedString()
        {
            HomeController c = new HomeController();
            string input = "this is a sentence.";
            string expectedOutput = "This is a sentence.";

            string actualResult = c.Capitolize(input);
            Assert.That(actualResult, Is.EqualTo(expectedOutput));
        }
    }
}