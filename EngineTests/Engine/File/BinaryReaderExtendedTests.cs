// <copyright file="BinaryReaderExtendedTests.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EngineTests;

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
    #endregion Properties

    #region Housekeeping
    /// <summary>
    /// ClassInitialize runs code before running the first test in the class.
    /// </summary>
    /// <param name="context">The context.</param>
    [ClassInitialize]
    public static void ClassInit(TestContext context) => _ = context;//MessageBox.Show("ClassInit " + context.TestName);

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
    /// Read the network u int14test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkUInt14Test()
    {
        var intValues = new List<ushort> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0x4000
        };

        using var stream = new MemoryStream();
        using (var writer = new BinaryWriterExtended(stream))
        {
            foreach (var value in intValues)
            {
                writer.WriteNetworkUInt14(value);
            }
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkUInt14());
        }
    }

    /// <summary>
    /// Read the network int14test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkInt14Test()
    {
        var intValues = new List<short> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0x4000
        };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetworkInt14(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkInt14());
        }
    }

    /// <summary>
    /// Read the network u int16test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkUInt16Test()
    {
        var intValues = new List<ushort> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710 };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetwork(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkUInt16());
        }
    }

    /// <summary>
    /// Read the network int16test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkInt16Test()
    {
        var intValues = new List<short> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710 };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetwork(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkInt16());
        }
    }

    /// <summary>
    /// Read the network u int24test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkUInt24Test()
    {
        var intValues = new List<ushort> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710 };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetworkUInt24(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkUInt24());
        }
    }

    /// <summary>
    /// Read the network int24test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkInt24Test()
    {
        var intValues = new List<short> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710 };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetworkInt24(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkInt24());
        }
    }

    /// <summary>
    /// Read the network u int32test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkUInt32Test()
    {
        var intValues = new List<uint> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0xFFFFFFFF
        };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetwork(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkUInt32());
        }
    }

    /// <summary>
    /// Read the network int32test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkInt32Test()
    {
        var intValues = new List<int> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0x7FFFFFFF
        };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetwork(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkInt32());
        }
    }

    /// <summary>
    /// Read the network u int64test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkUInt64Test()
    {
        var intValues = new List<ulong> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0x7FFFFFFFFFFFFFFF
        };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetwork(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkUInt64());
        }
    }

    /// <summary>
    /// Read the network int64test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadNetworkInt64Test()
    {
        var intValues = new List<long> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0x7FFFFFFFFFFFFFFF
        };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.WriteNetwork(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.ReadNetworkInt64());
        }
    }

    /// <summary>
    /// The read7bit encoded int test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void Read7BitEncodedIntTest()
    {
        var intValues = new List<int> {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x8, 0x10, 0x18, 0x20, 0x40, 0x64, 0x80, 0x100,
            0x3E8, 0x2710, 0x186A0, -0xF4240 };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        foreach (var value in intValues)
        {
            writer.Write7BitEncodedInt(value);
        }

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        foreach (var value in intValues)
        {
            Assert.AreEqual(value, reader.Read7BitEncodedInt());
        }
    }

    /// <summary>
    /// Read the UTF8String test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadUTF8StringTest()
    {
        const string fox = "The quick brown fox jumps over the lazy dog.";
        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        writer.WriteUTF8String(fox);

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        Assert.AreEqual(fox, reader.ReadUTF8String());
    }

    /// <summary>
    /// Read the ASCII string test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty("Engine.File", nameof(BinaryReaderExtended))]
    [DeploymentItem("Engine.dll")]
    [DeploymentItem("Engine.File.dll")]
    public void ReadASCIIStringTest()
    {
        const string fox = "The quick brown fox jumps over the lazy dog.";
        using var stream = new MemoryStream();
        using var writer = new BinaryWriterExtended(stream);
        writer.WriteASCIIString(fox);

        stream.Position = 0;

        using var reader = new BinaryReaderExtended(stream);
        Assert.AreEqual(fox, reader.ReadASCIIString());
    }
}
