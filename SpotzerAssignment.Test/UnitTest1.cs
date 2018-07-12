using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotzerAssignment.Model;

namespace SpotzerAssignment.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var productLine = new PaidSearchProductLine();
            productLine.CampaignName = "Saraza";

            Line productLineGeneric = productLine;
            var productLineConverted = productLineGeneric as PaidSearchProductLine;

            Assert.AreEqual(productLineConverted.CampaignName, "Saraza");


        }
    }
}
