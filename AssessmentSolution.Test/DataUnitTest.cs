using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace AssessmentSolution.Test
{
    [TestClass]
    public class DataUnitTest
    {       
        [TestMethod]
        [Description("To Test total number of records that are present in test data")]
        public void TestNumbeOfRecordsInData()
        {
            Data data = new Data();
            data.ReadTextFile(@"Data\AssessmentInput.txt");            
            Assert.AreEqual(12, data.GetCount(), "Test Failed - Failed to load records");
        }

        [TestMethod]
        [Description("To Test count of records must be 0 when invalid file format is provided")]
        public void TestInvalidFileFormat()
        {
            Data data = new Data();
            data.ReadTextFile(@"Data\AssessmentInput.txt");
            Assert.AreEqual(0, data.GetCount(".jpeg"), "Test Failed - Invalid format file found");


        }
        [TestMethod]
        [Description("To Test total number of records present in test data for each valid file type")]
        public void TestTotalRecordOfFileType()
        {
            Data data = new Data(@"Data\AssessmentInput.txt");
            Assert.AreEqual(4, data.GetCount(Constants.XML), "Test Failed - Expected 'xml' files count is not equal to actual count");
            Assert.AreEqual(4, data.GetCount(Constants.Dll), "Test Failed - Expected 'dll' files count is not equal to actual count");
            Assert.AreEqual(4, data.GetCount(Constants.CONFIG), "Test Failed - Expected 'config' files count is not equal to actual count");
            Assert.AreEqual(0, data.GetCount(Constants.NF), "Test Failed - Expected 'nf' files count is not equal to actual count");


        }
        [TestMethod]
        [Description("To Test sum of total size of a file type")]
        public void TestTotalSizeOfFileType()
        {
            Data data = new Data(@"Data\AssessmentInput.txt");
            Assert.AreEqual(24377, data.GetSize(Constants.XML), "Test Failed - Expected size of 'xml' is not equal to actual size");
            Assert.AreEqual(195976, data.GetSize(Constants.Dll), "Test Failed - Expected size of 'dll' is not equal to actual size");
            Assert.AreEqual(1324, data.GetSize(Constants.CONFIG), "Test Failed - Expected size of 'config' is not equal to actual size");
            Assert.AreEqual(0, data.GetSize(Constants.NF), "Test Failed - Expected size of 'nf' is not equal to actual size");


        }
        [TestMethod]
        [Description("To Test average size of a file type")]

        public void TestAverageSizeOfFileType()
        {
            Data data = new Data(@"Data\AssessmentInput.txt");
            Assert.AreEqual(6094.25, data.GetAverageSize(Constants.XML), "Test Failed - Expected average size of 'xml' is not equal to actual average size");
            Assert.AreEqual(48994, data.GetAverageSize(Constants.Dll), "Test Failed - Expected average size of 'dll' is not equal to actual average size");
            Assert.AreEqual(331, data.GetAverageSize(Constants.CONFIG), "Test Failed - Expected average size of 'config' is not equal to actual average size");
            Assert.AreEqual(0, data.GetAverageSize(Constants.NF), "Test Failed - Expected average size of 'nf' is not equal to actual average size");


        }       
    }
}
