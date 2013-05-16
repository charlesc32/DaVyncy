using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DaVyncyTests
{
    
    
    /// <summary>
    ///This is a test class for StringExtensionTest and is intended
    ///to contain all StringExtensionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringExtensionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for FindOverlap
        ///</summary>
        [TestMethod()]
        public void FindOverlapTestNoOverlap()
        {
            string str1 = "ABCDEF";
            string str2 = "XCDEZ";
            string expected = string.Empty; 
            string actual;
            actual = str1.FindOverlap(str2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FindOverlapTestAtEnd()
        {
            string str1 = "ABCDEF";
            string str2 = "DEFG";
            string expected = "ABCDEFG";
            string actual;
            actual = str1.FindOverlap(str2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FindOverlapTestAtBeginning()
        {
            string str1 = "ABCDEF";
            string str2 = "XYZABC";
            string expected = "XYZABCDEF";
            string actual;
            actual = str1.FindOverlap(str2);
            Assert.AreEqual(expected, actual);

            str2 = "BCDE";
            expected = "ABCDEF";
            actual = str1.FindOverlap(str2);
            Assert.AreEqual(expected, actual);
        }
    }
}
