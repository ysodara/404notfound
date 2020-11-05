using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Peak_Performance;

namespace Peak_Performance_Test
{
    [TestFixture]
    public class MeganTests
    {
        /**
         * This is to test if the controller "All Admin" will return more than zero records, as there is always more than one Admin Account
         * */

        [Test]
        public void ReturnsAllAdmin_NumberGreaterThanZero_ReturnsTrue()
        {
            /*Attempt at MOQ
            //Mock<Peak_Performance.Areas.Admin.Controllers.HomeController> mock = new Mock<Peak_Performance.Areas.Admin.Controllers.HomeController>();
            //mock.Setup(a => a.RecordCheck()).Returns(true);*/

            Peak_Performance.Areas.Admin.Controllers.HomeController c = new Peak_Performance.Areas.Admin.Controllers.HomeController();

            List<string> list = new List<string>();
            list.Add("testing");
            list.Add("testing2");

            bool check = c.RecordCheck(list.Count);
            Assert.That(true, Is.EqualTo(check));
        }
    }
}