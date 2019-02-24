// <copyright file="UnitTest.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        #region Constants
        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double TestEpsilon = 0.0000000000001d;
        #endregion Constants

        #region Properties
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        #endregion Properties

        #region Assembly Housekeeping
        /// <summary>
        /// The assembly init.
        /// </summary>
        /// <param name="context">The context.</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //MessageBox.Show("AssemblyInit " + context.TestName);
        }

        /// <summary>
        /// The assembly cleanup.
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            //MessageBox.Show("AssemblyCleanup");
        }
        #endregion Assembly Housekeeping

        #region Class Housekeeping
        /// <summary>
        /// ClassInitialize runs code before running the first test in the class.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //MessageBox.Show("ClassInit " + context.TestName);
        }

        /// <summary>
        /// TestInitialize runs code before running each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// TestCleanup runs code after each test has run.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// ClassCleanup runs code after all tests in a class have run.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }
        #endregion Class Housekeeping

        ///// <summary>
        ///// An example exception test involving dividing by 0.
        ///// </summary>
        //[TestMethod()]
        //[Priority(0)]
        //[Owner("Null")]
        //[TestProperty("Null", "Null")]
        //[DeploymentItem("Engine.dll")]
        //[ExpectedException(typeof(DivideByZeroException))]
        //[Ignore]
        //public void DivideMethodTest()
        //{
        //    //const int x = 0;
        //    //var value = 1 / x;
        //    Assert.Fail("No exception was thrown.");
        //}

        ///// <summary>
        ///// An example test to show test structure.
        ///// </summary>
        //[TestMethod]
        //[Priority(0)]
        //[Owner("Null")]
        //[TestProperty("Null", "Null")]
        //[DeploymentItem("Engine.dll")]
        //[Ignore]
        //public void TestMethodExample()
        //{
        //    //
        //    // TODO: Add test logic here
        //    //
        //    Assert.Inconclusive("ToDo: Implement code to verify target.");
        //    throw new NotImplementedException();
        //}
    }
}
