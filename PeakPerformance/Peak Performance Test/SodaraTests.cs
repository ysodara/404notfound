using System;
using NUnit.Framework;
using Peak_Performance.Controllers;
using Peak_Performance.Areas.Athlete.Controllers;
using Peak_Performance.Models;
using Peak_Performance.Models.ViewModels;


namespace Peak_Performance_Test
{
    [TestFixture]
    public class SodaraTests
    {
        [Test]
        public void Convert_DateTime_FormattedString()
        {
            AthleteProfileViewModel athlete = new AthleteProfileViewModel();
            var date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            string test = athlete.ConvertToDate(date1);

            string expected = "05-01-2008";

            Assert.That(test, Is.EqualTo(expected));
        }
    }
}