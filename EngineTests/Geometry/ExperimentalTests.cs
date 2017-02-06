// <copyright file="ExperimentalTests.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Engine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ExperimentalTests
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeter1Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeter1();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeter1();
        //    Assert.AreEqual(1110.72073453959d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeter2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeter2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeter2();
        //    Assert.AreEqual(1105.17460803539d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterKeplerTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterKepler();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterKepler();
        //    Assert.AreEqual(1088.27961854053d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterKepler();
        //    Assert.AreEqual(0.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterSiposTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterSipos();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterSipos();
        //    Assert.AreEqual(1110.95211059346d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterSipos();
        //    Assert.AreEqual(double.PositiveInfinity.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterNaiveTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterNaive();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterNaive();
        //    Assert.AreEqual(1099.55742875643d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterNaive();
        //    Assert.AreEqual(314.159265358979.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPeanoTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPeano();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPeano();
        //    Assert.AreEqual(1105.19633386438d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPeano();
        //    Assert.AreEqual(471.238898038469d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterEulerTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterEuler();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterEuler();
        //    Assert.AreEqual(1110.72073453959d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterEuler();
        //    Assert.AreEqual(444.288293815837d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterAlmkvistTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterAlmkvist();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterAlmkvist();
        //    Assert.AreEqual(1362.09460653742d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterAlmkvist();
        //    Assert.AreEqual(24.4893050647622.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterQuadraticTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterQuadratic();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterQuadratic();
        //    Assert.AreEqual(1105.15317700073d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterQuadratic();
        //    Assert.AreEqual(384.764949048559.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterMuirTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterMuir();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterMuir();
        //    Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterMuir();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterLindnerTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterLindner();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterLindner();
        //    Assert.AreEqual(1100.9590321664d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterLindner();
        //    Assert.AreEqual(333.216220361877.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterRamanujanTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterRamanujan();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterRamanujan();
        //    Assert.AreEqual(1105.17458954584d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterRamanujan();
        //    Assert.AreEqual(398.337986806673d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterSelmerTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterSelmer();
        //    Assert.AreEqual(694.113089432035d.ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterSelmer();
        //    Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterSelmer();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterRamanujan2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterRamanujan2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterRamanujan2();
        //    Assert.AreEqual(1105.1746080353828d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterRamanujan2();
        //    Assert.AreEqual(399.839065002337d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPadéSelmerTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPadéSelmer();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPadéSelmer();
        //    Assert.AreEqual(1105.17458037204d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPadéSelmer();
        //    Assert.AreEqual(397.935069454707d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPadéMichonTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPadéMichon();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPadéMichon();
        //    Assert.AreEqual(1105.17460778829d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPadéMichon();
        //    Assert.AreEqual(398.932400455847d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPadéHudsonLipkaBronshteinTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPadéHudsonLipkaBronshtein();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPadéHudsonLipkaBronshtein();
        //    Assert.AreEqual(1105.21776167395d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPadéHudsonLipkaBronshtein();
        //    Assert.AreEqual(438.513974563575d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPadéJacobsenWaadelandTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPadéJacobsenWaadeland();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPadéJacobsenWaadeland();
        //    Assert.AreEqual(1105.17460803497d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPadéJacobsenWaadeland();
        //    Assert.AreEqual(399.644779742375.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPadé3_2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPadé3_2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPadé3_2();
        //    Assert.AreEqual(1105.17460803547d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPadé3_2();
        //    Assert.AreEqual(399.815958260254.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterPadé3_3Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterPadé3_3();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterPadé3_3();
        //    Assert.AreEqual(1105.17460803547d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterPadé3_3();
        //    Assert.AreEqual(399.893168774002d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterOptimizedPeanoTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterOptimizedPeano();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterOptimizedPeano();
        //    Assert.AreEqual(1103.16632802551d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterOptimizedPeano();
        //    Assert.AreEqual(414.690230273853.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterOptimizedQuadratic1Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterOptimizedQuadratic1();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterOptimizedQuadratic1();
        //    Assert.AreEqual(1106.19333100019d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterOptimizedQuadratic1();
        //    Assert.AreEqual(396.540829769506d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterOptimizedQuadratic2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterOptimizedQuadratic2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterOptimizedQuadratic2();
        //    Assert.AreEqual(1115.22975969741d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterOptimizedQuadratic2();
        //    Assert.AreEqual(487.380298264211d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterOptimizedRamanujan1Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterOptimizedRamanujan1();
        //    Assert.AreEqual(628.318530717958d.ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterOptimizedRamanujan1();
        //    Assert.AreEqual(1105.25126379061d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterOptimizedRamanujan1();
        //    Assert.AreEqual(399.487026354434.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterBartolomeuMichonTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterBartolomeuMichon();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterBartolomeuMichon();
        //    Assert.AreEqual(1106.99713336319d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterBartolomeuMichon();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterCantrell2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCantrell2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCantrell2();
        //    Assert.AreEqual(1105.17405478863d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCantrell2();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterTakakazuSekiTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterTakakazuSeki();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterTakakazuSeki();
        //    Assert.AreEqual(1106.50464442348d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterTakakazuSeki();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterLockwoodTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterLockwood();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterLockwood();
        //    Assert.AreEqual(1103.68403081356d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterLockwood();
        //    Assert.AreEqual(double.NaN.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitfulTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful();
        //    Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterYNOTTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterYNOT();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterYNOT();
        //    Assert.AreEqual(1105.5575586484d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterYNOT();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterCombinedPadéTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCombinedPadé();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCombinedPadé();
        //    Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCombinedPadé();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterCombinedPadé2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCombinedPadé2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCombinedPadé2();
        //    Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCombinedPadé2();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterCombinedPadéHudsonLipkaMichonTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCombinedPadéHudsonLipkaMichon();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCombinedPadéHudsonLipkaMichon();
        //    Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCombinedPadéHudsonLipkaMichon();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterJacobsenWaadelandHudsonLipkaTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterJacobsenWaadelandHudsonLipka();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterJacobsenWaadelandHudsonLipka();
        //    Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterJacobsenWaadelandHudsonLipka();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeter2_3JacobsenWaadelandTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeter2_3JacobsenWaadeland();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeter2_3JacobsenWaadeland();
        //    Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeter2_3JacobsenWaadeland();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeter3_3_3_2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeter3_3_3_2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeter3_3_3_2();
        //    Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeter3_3_3_2();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //[Ignore]
        //public void EllipsePerimeterBartolomeuTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterBartolomeu();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterBartolomeu();
        //    Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterBartolomeu();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterRivera1Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterRivera1();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterRivera1();
        //    Assert.AreEqual(1100.37262930979d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterRivera1();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterRivera2Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterRivera2();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterRivera2();
        //    Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterRivera2();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterCantrellTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCantrell();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCantrell();
        //    Assert.AreEqual(1105.19458936518d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCantrell();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterSykoraTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterSykora();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterSykora();
        //    Assert.AreEqual(1105.19458936518d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterSykora();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterCantrellRamanujanTest()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCantrellRamanujan();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCantrellRamanujan();
        //    Assert.AreEqual(1105.17460803538d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCantrellRamanujan();
        //    Assert.AreEqual(485.679799643358.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterK13Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterK13();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterK13();
        //    Assert.AreEqual(1105.13908164801d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterK13();
        //    Assert.AreEqual(379.223779587408d.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterThomasBlankenhorn1Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterThomasBlankenhorn1();
        //    Assert.AreEqual(628.318513355998.ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterThomasBlankenhorn1();
        //    Assert.AreEqual(1105.17462238101.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterThomasBlankenhorn1();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterThomasBlankenhorn8Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterThomasBlankenhorn8();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterThomasBlankenhorn8();
        //    Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterThomasBlankenhorn8();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterCantrell2006Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterCantrell2006();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterCantrell2006();
        //    Assert.AreEqual(1105.17412721933d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterCantrell2006();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod()]
        //public void EllipsePerimeterAhmadi2006Test()
        //{
        //    // Test a perfect circle.
        //    Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
        //    double value = ellipse.EllipsePerimeterAhmadi2006();
        //    Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

        //    ellipse = new Ellipse(new Point2D(), 200, 150, 0);
        //    value = ellipse.EllipsePerimeterAhmadi2006();
        //    Assert.AreEqual(1105.17434614743d.ToString(), value.ToString());

        //    // Test a flat line.
        //    ellipse = new Ellipse(new Point2D(), 100, 0, 0);
        //    value = ellipse.EllipsePerimeterAhmadi2006();
        //    Assert.AreEqual(400.ToString(), value.ToString());
        //}

        [TestMethod]
        public void DistanceModulusTest()
        {
            for (int i = 0; i < 1000000; i++)
            {
                //Experimental.DistanceModulus(2, 2, 2, 2);
            }
        }
    }
}