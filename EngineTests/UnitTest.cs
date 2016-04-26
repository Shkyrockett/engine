using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// 
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTest"/>.
        /// </summary>
        public UnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            //MessageBox.Show("AssemblyInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            //MessageBox.Show("ClassInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            //MessageBox.Show("AssemblyCleanup");
        }

        /// <summary>
        /// A Garbage test of dividing by 0.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(DivideByZeroException))]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DivideMethodTest()
        {
            int x = 0;
            int value = 1 / x;
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
