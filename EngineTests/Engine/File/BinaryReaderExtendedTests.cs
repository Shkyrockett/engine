// <copyright file="BinaryReaderExtendedTests.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
    public class BinaryReaderExtendedTests
    {
        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Housekeeping

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

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkUInt14Test()
        {
            var intValues = new List<ushort> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0x4000
            };

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
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkInt14Test()
        {
            var intValues = new List<short> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0x4000
            };

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
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkUInt16Test()
        {
            var intValues = new List<ushort> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetwork(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkUInt16());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkInt16Test()
        {
            var intValues = new List<short> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetwork(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkInt16());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkUInt24Test()
        {
            var intValues = new List<ushort> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetworkUInt24(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkUInt24());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkInt24Test()
        {
            var intValues = new List<short> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710 };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetworkInt24(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkInt24());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkUInt32Test()
        {
            var intValues = new List<uint> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0xFFFFFFFF
            };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetwork(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkUInt32());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkInt32Test()
        {
            var intValues = new List<int> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0x7FFFFFFF
            };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetwork(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkInt32());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkUInt64Test()
        {
            var intValues = new List<ulong> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0x7FFFFFFFFFFFFFFF
            };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetwork(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkUInt64());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadNetworkInt64Test()
        {
            var intValues = new List<long> {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
                0x3E8, 0x2710, 0x7FFFFFFFFFFFFFFF
            };

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                foreach (var value in intValues)
                {
                    writer.WriteNetwork(value);
                }

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                foreach (var value in intValues)
                {
                    Assert.AreEqual(value, reader.ReadNetworkInt64());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void Read7BitEncodedIntTest()
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

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadUTF8StringTest()
        {
            var fox = "The quick brown fox jumps over the lazy dog.";
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                writer.WriteUTF8String(fox);

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                Assert.AreEqual(fox, reader.ReadUTF8String());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine.File", "BinaryReaderExtended")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("Engine.File.dll")]
        public void ReadASCIIStringTest()
        {
            var fox = "The quick brown fox jumps over the lazy dog.";
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriterExtended(stream);
                writer.WriteASCIIString(fox);

                stream.Position = 0;

                var reader = new BinaryReaderExtended(stream);
                Assert.AreEqual(fox, reader.ReadASCIIString());
            }
        }
    }
}
