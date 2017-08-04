﻿// <copyright file="MathsTests.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Engine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class MathsTests
    {
        #region Constants

        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double TestEpsilon = 0.0000000000001d;

        #endregion

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
            //MessageBox.Show("TestClassInit");
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

        #region Arithmetic Safety Tests

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void IsAdditionSafeTest()
        {
            //// A listing of expected results for specific values.
            //var sbyteTestCases = new Dictionary<(sbyte a, sbyte b), bool>
            //{
            //    { (0, sbyte.MinValue), true },
            //    { (sbyte.MinValue, 0), true },
            //    { (0, sbyte.MaxValue), true },
            //    { (sbyte.MaxValue, 0), true },
            //    { (sbyte.MinValue, sbyte.MaxValue), true },
            //    { (sbyte.MaxValue, sbyte.MinValue), true },
            //    { (sbyte.MinValue / 2, sbyte.MaxValue / 2), true },
            //    { (sbyte.MinValue / 2, sbyte.MinValue / 2), true },
            //    { (sbyte.MaxValue / 2, sbyte.MaxValue / 2), true },
            //    { (sbyte.MaxValue, sbyte.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in sbyteTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (sbyte)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test sbyteTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test sbyteTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(sbyteTestCases[value], result, $"sbyteTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var byteTestCases = new Dictionary<(byte a, byte b), bool>
            //{
            //    { (byte.MinValue, byte.MaxValue), true },
            //    { (byte.MaxValue, byte.MinValue), true },
            //    { (byte.MinValue / 2, byte.MaxValue / 2), true },
            //    { (byte.MinValue / 2, byte.MinValue / 2), true },
            //    { (byte.MaxValue / 2, byte.MaxValue / 2), true },
            //    { (byte.MaxValue, byte.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in byteTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (byte)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test byteTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test byteTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(byteTestCases[value], result, $"byteTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var shortTestCases = new Dictionary<(short a, short b), bool>
            //{
            //    { (0, short.MinValue), true },
            //    { (short.MinValue, 0), true },
            //    { (0, short.MaxValue), true },
            //    { (short.MaxValue, 0), true },
            //    { (short.MinValue, short.MaxValue), true },
            //    { (short.MaxValue, short.MinValue), true },
            //    { ((short.MinValue / 2), (short.MaxValue / 2)), true },
            //    { ((short.MinValue / 2), (short.MinValue / 2)), true },
            //    { ((short.MaxValue / 2), (short.MaxValue / 2)), true },
            //    { (short.MaxValue, short.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in shortTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (short)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test shortTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test shortTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(shortTestCases[value], result, $"shortTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var ushortTestCases = new Dictionary<(ushort a, ushort b), bool>
            //{
            //    { (ushort.MinValue, ushort.MaxValue), true },
            //    { (ushort.MaxValue, ushort.MinValue), true },
            //    { (ushort.MinValue / 2, ushort.MaxValue / 2), true },
            //    { (ushort.MinValue / 2, ushort.MinValue / 2), true },
            //    { (ushort.MaxValue / 2, ushort.MaxValue / 2), true },
            //    { (ushort.MaxValue, ushort.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in ushortTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (ushort)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test ushortTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test ushortTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(ushortTestCases[value], result, $"ushortTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var intTestCases = new Dictionary<(int a, int b), bool>
            //{
            //    { (0, int.MinValue), true },
            //    { (int.MinValue, 0), true },
            //    { (0, int.MaxValue), true },
            //    { (int.MaxValue, 0), true },
            //    { (int.MinValue, int.MaxValue), true },
            //    { (int.MaxValue, int.MinValue), true },
            //    { ((int.MinValue / 2), (int.MaxValue / 2)), true },
            //    { ((int.MinValue / 2), (int.MinValue / 2)), true },
            //    { ((int.MaxValue / 2), (int.MaxValue / 2)), true },
            //    { (int.MaxValue, int.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in intTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test intTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test intTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(intTestCases[value], result, $"intTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var uintTestCases = new Dictionary<(uint a, uint b), bool>
            //{
            //    { (uint.MinValue, uint.MaxValue), true },
            //    { (uint.MaxValue, uint.MinValue), true },
            //    { ((uint.MinValue / 2u), (uint.MaxValue / 2u)), true },
            //    { ((uint.MinValue / 2u), (uint.MinValue / 2u)), true },
            //    { ((uint.MaxValue / 2u), (uint.MaxValue / 2u)), true },
            //    { (uint.MaxValue, uint.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in uintTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test uintTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test uintTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(uintTestCases[value], result, $"uintTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var longTestCases = new Dictionary<(long a, long b), bool>
            //{
            //    { (0, long.MinValue), true },
            //    { (long.MinValue, 0), true },
            //    { (0, long.MaxValue), true },
            //    { (long.MaxValue, 0), true },
            //    { (long.MinValue, long.MaxValue), true },
            //    { (long.MaxValue, long.MinValue), true },
            //    { ((long.MinValue / 2L), (long.MaxValue / 2L)), true },
            //    { ((long.MinValue / 2L), (long.MinValue / 2L)), true },
            //    { ((long.MaxValue / 2L), (long.MaxValue / 2L)), true },
            //    { (long.MaxValue, long.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in longTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test longTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test longTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(longTestCases[value], result, $"longTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var ulongTestCases = new Dictionary<(ulong a, ulong b), bool>
            //{
            //    { (ulong.MinValue, ulong.MaxValue), true },
            //    { (ulong.MaxValue, ulong.MinValue), true },
            //    { ((ulong.MinValue / 2ul), (ulong.MaxValue / 2ul)), true },
            //    { ((ulong.MinValue / 2ul), (ulong.MinValue / 2ul)), true },
            //    { ((ulong.MaxValue / 2ul), (ulong.MaxValue / 2ul)), true },
            //    { (ulong.MaxValue, ulong.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in ulongTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test ulongTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test ulongTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(ulongTestCases[value], result, $"ulongTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var floatTestCases = new Dictionary<(float a, float b), bool>
            //{
            //    { (0, float.MinValue), true },
            //    { (float.MinValue, 0), true },
            //    { (0, float.MaxValue), true },
            //    { (float.MaxValue, 0), true },
            //    { (float.MinValue, float.MaxValue), true },
            //    { (float.MaxValue, float.MinValue), true },
            //    { ((float.MinValue / 2f), (float.MaxValue / 2f)), true },
            //    { ((float.MinValue / 2f), (float.MinValue / 2f)), true },
            //    { ((float.MaxValue / 2f), (float.MaxValue / 2f)), true },
            //    { (float.MaxValue, float.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in floatTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test floatTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test floatTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(floatTestCases[value], result, $"floatTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var doubleTestCases = new Dictionary<(double a, double b), bool>
            //{
            //    { (0, double.MinValue), true },
            //    { (double.MinValue, 0), true },
            //    { (0, double.MaxValue), true },
            //    { (double.MaxValue, 0), true },
            //    { (double.MinValue, double.MaxValue), true },
            //    { (double.MaxValue, double.MinValue), true },
            //    { ((double.MinValue * 0.5d), (double.MaxValue * 0.5d)), true },
            //    { ((double.MinValue * 0.5d), (double.MinValue * 0.5d)), true },
            //    { ((double.MaxValue * 0.5d), (double.MaxValue * 0.5d)), true },
            //    { (double.MaxValue, double.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in doubleTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test doubleTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test doubleTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(doubleTestCases[value], result, $"doubleTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var decimalTestCases = new Dictionary<(decimal a, decimal b), bool>
            //{
            //    { (decimal.Zero, decimal.MinValue), true },
            //    { (decimal.MinValue, decimal.Zero), true },
            //    { (decimal.Zero, decimal.MaxValue), true },
            //    { (decimal.MaxValue, decimal.Zero), true },
            //    { (decimal.MinValue, decimal.MaxValue), true },
            //    { (decimal.MaxValue, decimal.MinValue), true },
            //    { ((decimal.MinValue / 2m), (decimal.MaxValue / 2m)), true },
            //    // Need to figure out why the following two test cases fail only for decimals.
            //    //{ ((decimal.MinValue / 2m), (decimal.MinValue / 2m)), true },
            //    //{ ((decimal.MaxValue / 2m), (decimal.MaxValue / 2m)), true },
            //    { (decimal.MaxValue, decimal.MaxValue), false },
            //};

            //// Run tests for each height and width.
            //foreach (var value in decimalTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsAdditionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test decimalTestCases({value.a}, {value.b})={value.a + value.b}");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test decimalTestCases({value.a}, {value.b})");
            //    }

            //    Assert.AreEqual(decimalTestCases[value], result, $"decimalTestCases({value.a}, {value.b})");
            //}
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void IsMultiplicationSafeTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void IsSubtractionSafeTest()
        {
            //// A listing of expected results for specific values.
            //var sbyteTestCases = new Dictionary<(sbyte a, sbyte b), bool>
            //{
            //    { (0, sbyte.MinValue), true },
            //    { (sbyte.MinValue, 0), true },
            //    { (0, sbyte.MaxValue), true },
            //    { (sbyte.MaxValue, 0), true },
            //    // Wait, what? Oh, right.
            //    { (sbyte.MinValue, sbyte.MaxValue), true },
            //    { (sbyte.MaxValue, sbyte.MinValue), true },
            //    { (sbyte.MinValue / 2, sbyte.MaxValue / 2), true },
            //    { (sbyte.MinValue / 2, sbyte.MinValue / 2), true },
            //    { (sbyte.MaxValue / 2, sbyte.MaxValue / 2), true },
            //    { (sbyte.MaxValue, sbyte.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in sbyteTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (sbyte)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test sbyteTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test sbyteTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(sbyteTestCases[value], result, $"sbyteTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var byteTestCases = new Dictionary<(byte a, byte b), bool>
            //{
            //    { (byte.MinValue, byte.MaxValue), false },
            //    { (byte.MaxValue, byte.MinValue), true },
            //    { (byte.MinValue, byte.MaxValue / 2), false },
            //    { (byte.MinValue, byte.MinValue), true },
            //    { (byte.MaxValue / 2, byte.MaxValue / 2), true },
            //    { (byte.MaxValue, byte.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in byteTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (byte)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test byteTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test byteTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(byteTestCases[value], result, $"byteTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var shortTestCases = new Dictionary<(short a, short b), bool>
            //{
            //    { (0, short.MinValue), true },
            //    { (short.MinValue, 0), true },
            //    { (0, short.MaxValue), true },
            //    { (short.MaxValue, 0), true },
            //    // Wait, what? Oh right.
            //    { (short.MinValue, short.MaxValue), true },
            //    { (short.MaxValue, short.MinValue), true },
            //    { ((short.MinValue / 2), (short.MaxValue / 2)), true },
            //    { ((short.MinValue / 2), (short.MinValue / 2)), true },
            //    { ((short.MaxValue / 2), (short.MaxValue / 2)), true },
            //    { (short.MaxValue, short.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in shortTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (short)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test shortTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test shortTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(shortTestCases[value], result, $"shortTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var ushortTestCases = new Dictionary<(ushort a, ushort b), bool>
            //{
            //    { (ushort.MinValue, ushort.MaxValue), false },
            //    { (ushort.MaxValue, ushort.MinValue), true },
            //    { (ushort.MinValue, ushort.MaxValue / 2), false },
            //    { (ushort.MinValue / 2, ushort.MinValue / 2), true },
            //    { (ushort.MaxValue / 2, ushort.MaxValue / 2), true },
            //    { (ushort.MaxValue, ushort.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in ushortTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = (ushort)(value.a + value.b);
            //        Assert.AreEqual(true, result, $"Crash Test ushortTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test ushortTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(ushortTestCases[value], result, $"ushortTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var intTestCases = new Dictionary<(int a, int b), bool>
            //{
            //    { (0, int.MinValue), true },
            //    { (int.MinValue, 0), true },
            //    { (0, int.MaxValue), true },
            //    { (int.MaxValue, 0), true },
            //    // Wait, what? Oh, right.
            //    { (int.MinValue, int.MaxValue), true },
            //    { (int.MaxValue, int.MinValue), true },
            //    { ((int.MinValue / 2), (int.MaxValue / 2)), false },
            //    { ((int.MinValue / 2), (int.MinValue / 2)), false },
            //    { ((int.MaxValue / 2), (int.MaxValue / 2)), false },
            //    //{ (int.MaxValue, int.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in intTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test intTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test intTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(intTestCases[value], result, $"intTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var uintTestCases = new Dictionary<(uint a, uint b), bool>
            //{
            //    { (uint.MinValue, uint.MaxValue), false },
            //    { (uint.MaxValue, uint.MinValue), true },
            //    { ((uint.MinValue), (uint.MaxValue / 2u)), false },
            //    //{ ((uint.MinValue / 2u), (uint.MinValue / 2u)), true },
            //    //{ ((uint.MaxValue / 2u), (uint.MaxValue / 2u)), true },
            //    //{ (uint.MaxValue, uint.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in uintTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test uintTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test uintTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(uintTestCases[value], result, $"uintTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var longTestCases = new Dictionary<(long a, long b), bool>
            //{
            //    { (0, long.MinValue), true },
            //    { (long.MinValue, 0), true },
            //    { (0, long.MaxValue), true },
            //    { (long.MaxValue, 0), true },
            //    //{ (long.MinValue, long.MaxValue), false },
            //    //{ (long.MaxValue, long.MinValue), true },
            //    //{ ((long.MinValue / 2L), (long.MaxValue / 2L)), true },
            //    //{ ((long.MinValue / 2L), (long.MinValue / 2L)), true },
            //    //{ ((long.MaxValue / 2L), (long.MaxValue / 2L)), true },
            //    //{ (long.MaxValue, long.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in longTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test longTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test longTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(longTestCases[value], result, $"longTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var ulongTestCases = new Dictionary<(ulong a, ulong b), bool>
            //{
            //    { (ulong.MinValue, ulong.MaxValue), false },
            //    { (ulong.MaxValue, ulong.MinValue), true },
            //    //{ ((ulong.MinValue), (ulong.MaxValue / 2ul)), false },
            //    //{ ((ulong.MinValue / 2ul), (ulong.MinValue / 2ul)), true },
            //    //{ ((ulong.MaxValue / 2ul), (ulong.MaxValue / 2ul)), true },
            //    //{ (ulong.MaxValue, ulong.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in ulongTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test ulongTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test ulongTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(ulongTestCases[value], result, $"ulongTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var floatTestCases = new Dictionary<(float a, float b), bool>
            //{
            //    { (0, float.MinValue), true },
            //    { (float.MinValue, 0), true },
            //    { (0, float.MaxValue), true },
            //    { (float.MaxValue, 0), true },
            //    //{ (float.MinValue, float.MaxValue), false },
            //    //{ (float.MaxValue, float.MinValue), true },
            //    //{ ((float.MinValue / 2f), (float.MaxValue / 2f)), true },
            //    //{ ((float.MinValue / 2f), (float.MinValue / 2f)), true },
            //    //{ ((float.MaxValue / 2f), (float.MaxValue / 2f)), true },
            //    //{ (float.MaxValue, float.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in floatTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test floatTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test floatTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(floatTestCases[value], result, $"floatTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var doubleTestCases = new Dictionary<(double a, double b), bool>
            //{
            //    { (0, double.MinValue), true },
            //    { (double.MinValue, 0), true },
            //    { (0, double.MaxValue), true },
            //    { (double.MaxValue, 0), true },
            //    //{ (double.MinValue, double.MaxValue), false },
            //    //{ (double.MaxValue, double.MinValue), true },
            //    //{ ((double.MinValue * 0.5d), (double.MaxValue * 0.5d)), true },
            //    //{ ((double.MinValue * 0.5d), (double.MinValue * 0.5d)), true },
            //    //{ ((double.MaxValue * 0.5d), (double.MaxValue * 0.5d)), true },
            //    //{ (double.MaxValue, double.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in doubleTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test doubleTestCases({value.a},{value.b})");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test doubleTestCases({value.a},{value.b})");
            //    }

            //    Assert.AreEqual(doubleTestCases[value], result, $"doubleTestCases({value.a},{value.b})");
            //}

            //// A listing of expected results for specific values.
            //var decimalTestCases = new Dictionary<(decimal a, decimal b), bool>
            //{
            //    { (decimal.Zero, decimal.MinValue), true },
            //    { (decimal.MinValue, decimal.Zero), true },
            //    { (decimal.Zero, decimal.MaxValue), true },
            //    { (decimal.MaxValue, decimal.Zero), true },
            //    //{ (decimal.MinValue, decimal.MaxValue), false },
            //    //{ (decimal.MaxValue, decimal.MinValue), true },
            //    //{ ((decimal.MinValue / 2m), (decimal.MaxValue / 2m)), true },
            //    //{ ((decimal.MinValue / 2m), (decimal.MinValue / 2m)), true },
            //    //{ ((decimal.MaxValue / 2m), (decimal.MaxValue / 2m)), true },
            //    //{ (decimal.MaxValue, decimal.MaxValue), true },
            //};

            //// Run tests for each height and width.
            //foreach (var value in decimalTestCases.Keys)
            //{
            //    // Retrieve the result of the operation. 
            //    var result = Maths.IsSubtractionSafe(value.a, value.b);
            //    // Check for a correct result.
            //    try
            //    {
            //        var test = value.a + value.b;
            //        Assert.AreEqual(true, result, $"Crash Test decimalTestCases({value.a}, {value.b})={value.a + value.b}");
            //    }
            //    catch (Exception)
            //    {
            //        Assert.AreEqual(false, result, $"Crash Test decimalTestCases({value.a}, {value.b})");
            //    }

            //    Assert.AreEqual(decimalTestCases[value], result, $"decimalTestCases({value.a}, {value.b})");
            //}
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// A Test for converting Radians to Degrees.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        [DeploymentItem("Engine.dll")]
        public void ToRadiansTest()
        {
            double value = 0;
            value = Maths.ToRadians(0);
            Assert.AreEqual(0, value);
            value = Maths.ToRadians(30);
            Assert.AreEqual(30 * (Math.PI / 180f), value);
            value = Maths.ToRadians(45);
            Assert.AreEqual(45 * (Math.PI / 180f), value);
            value = Maths.ToRadians(60);
            Assert.AreEqual(60 * (Math.PI / 180f), value);
            value = Maths.ToRadians(90);
            Assert.AreEqual(90 * (Math.PI / 180f), value);
            value = Maths.ToRadians(120);
            Assert.AreEqual(120 * (Math.PI / 180f), value);
            value = Maths.ToRadians(135);
            Assert.AreEqual(135 * (Math.PI / 180f), value);
            value = Maths.ToRadians(150);
            Assert.AreEqual(150 * (Math.PI / 180f), value);
            value = Maths.ToRadians(180);
            Assert.AreEqual(180 * (Math.PI / 180f), value);
            value = Maths.ToRadians(210);
            Assert.AreEqual(210 * (Math.PI / 180f), value);
            value = Maths.ToRadians(225);
            Assert.AreEqual(225 * (Math.PI / 180f), value);
            value = Maths.ToRadians(240);
            Assert.AreEqual(240 * (Math.PI / 180f), value);
            value = Maths.ToRadians(270);
            Assert.AreEqual(270 * (Math.PI / 180f), value);
            value = Maths.ToRadians(300);
            Assert.AreEqual(300 * (Math.PI / 180f), value);
            value = Maths.ToRadians(315);
            Assert.AreEqual(315 * (Math.PI / 180f), value);
            value = Maths.ToRadians(330);
            Assert.AreEqual(330 * (Math.PI / 180f), value);
            value = Maths.ToRadians(360);
            Assert.AreEqual(360 * (Math.PI / 180f), value);
        }

        /// <summary>
        /// A Test for converting Degrees to Radians.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        [DeploymentItem("Engine.dll")]
        public void ToDegreesTest()
        {
            double value = 0;
            value = Maths.ToDegrees(0);
            Assert.AreEqual(0, value);
            value = Maths.ToDegrees(30);
            Assert.AreEqual(30 * (180f / Math.PI), value);
            value = Maths.ToDegrees(45);
            Assert.AreEqual(45 * (180f / Math.PI), value);
            value = Maths.ToDegrees(60);
            Assert.AreEqual(60 * (180f / Math.PI), value);
            value = Maths.ToDegrees(90);
            Assert.AreEqual(90 * (180f / Math.PI), value);
            value = Maths.ToDegrees(120);
            Assert.AreEqual(120 * (180f / Math.PI), value);
            value = Maths.ToDegrees(135);
            Assert.AreEqual(135 * (180f / Math.PI), value);
            value = Maths.ToDegrees(150);
            Assert.AreEqual(150 * (180f / Math.PI), value);
            value = Maths.ToDegrees(180);
            Assert.AreEqual(180 * (180f / Math.PI), value);
            value = Maths.ToDegrees(210);
            Assert.AreEqual(210 * (180f / Math.PI), value);
            value = Maths.ToDegrees(225);
            Assert.AreEqual(225 * (180f / Math.PI), value);
            value = Maths.ToDegrees(240);
            Assert.AreEqual(240 * (180f / Math.PI), value);
            value = Maths.ToDegrees(270);
            Assert.AreEqual(270 * (180f / Math.PI), value);
            value = Maths.ToDegrees(300);
            Assert.AreEqual(300 * (180f / Math.PI), value);
            value = Maths.ToDegrees(315);
            Assert.AreEqual(315 * (180f / Math.PI), value);
            value = Maths.ToDegrees(330);
            Assert.AreEqual(330 * (180f / Math.PI), value);
            value = Maths.ToDegrees(360);
            Assert.AreEqual(360 * (180f / Math.PI), value);
        }

        /// <summary>
        /// A Test for rounding a number to an arbitrary value.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        [DeploymentItem("Engine.dll")]
        public void RoundToMultipleTest()
        {
            double value = 0;
            value = Maths.RoundToMultiple(3.5, 1.5);
            Assert.AreEqual(3, value);
        }

        /// <summary>
        /// A Test for retrieving the modulo of an arbitrary value, the same way Excel does.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        [DeploymentItem("Engine.dll")]
        public void ModuloTest()
        {
            double value = 0;
            value = Maths.Modulo(3.5, 1.5);
            Assert.AreEqual(0.5d, value);
        }

        /// <summary>
        /// A Test for finding the average value from a list of numbers.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        [DeploymentItem("Engine.dll")]
        public void AverageTest()
        {
            double value = 0;
            var array = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            value = Maths.Average(array);
            Assert.AreEqual(4.5, value);
        }

        /// <summary>
        /// A Test for finding the sum value from a list of numbers.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        [DeploymentItem("Engine.dll")]
        public void SumTest()
        {
            double value = 0;
            var array = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            value = Maths.Sum(array);
            Assert.AreEqual(45, value);
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void RandomTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void Atan2Test()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void SecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void CosecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void CotangentTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseSineTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseCosineTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseSecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseCosecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseCotangentTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HyperbolicSineTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HyperbolicCosineTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HyperbolicTangentTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HyperbolicSecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HyperbolicCosecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HyperbolicCotangentTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseHyperbolicSineTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseHyperbolicCosineTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseHyperbolicTangentTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseHyperbolicSecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseHyperbolicCosecantTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InverseHyperbolicCotangentTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void LogarithmTobaseNTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ToFloatTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void LessThanTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void GreaterThanTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void LessThanOrCloseTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void GreaterThanOrCloseTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void IsOneTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void IsZeroTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void IsBetweenZeroAndOneTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void FloatToIntTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void DoubleToIntTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void HIWORDTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        [TestMethod]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void LOWORDTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }
    }
}