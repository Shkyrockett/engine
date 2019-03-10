// <copyright file="BinaryWriterExtendedTests.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace EngineTests
{
    /// <summary>
    /// Tests the functionality of the <see cref="BinaryWriterExtended"/> class.
    /// </summary>
    [TestClass]
    public class BinaryWriterExtendedTests
    {
        #region Properties
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        #endregion Properties

        #region Housekeeping
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
        #endregion Housekeeping

        /// <summary>
        /// Write the network u int14test.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", nameof(BinaryWriterExtended))]
        [DeploymentItem("Engine.File.dll")]
        public void WriteNetworkUInt14Test()
        {
            var intValues = new List<ushort> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetworkUInt14(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkUInt14());
                }
            }
        }

        /// <summary>
        /// Write the network int14test.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", nameof(BinaryWriterExtended))]
        [DeploymentItem("Engine.File.dll")]
        public void WriteNetworkInt14Test()
        {
            var intValues = new List<short> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetworkInt14(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkInt14());
                }
            }
        }

        /// <summary>
        /// The write7bit encoded int test.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", nameof(BinaryWriterExtended))]
        [DeploymentItem("Engine.File.dll")]
        public void Write7BitEncodedIntTest()
        {
            var intValues = new List<int> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0x186A0, -0xF4240 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.Write7BitEncodedInt(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.Read7BitEncodedInt());
                }
            }
        }
    }
}
