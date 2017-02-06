// <copyright file="UnitTest.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //MessageBox.Show("AssemblyInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //MessageBox.Show("ClassInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            //MessageBox.Show("AssemblyCleanup");
        }

        ///// <summary>
        ///// A Garbage test of dividing by 0.
        ///// </summary>
        //[TestMethod()]
        //[ExpectedException(typeof(DivideByZeroException))]
        ////[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void DivideMethodTest()
        //{
        //    int x = 0;
        //    int value = 1 / x;
        //    Assert.Fail("No exception was thrown.");
        //}

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
