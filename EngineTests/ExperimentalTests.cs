using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Geometry;

namespace Engine.Tests
{
    [TestClass()]
    public class ExperimentalTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Perimeter1Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.Perimeter1();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.Perimeter1();
            Assert.AreEqual(1110.72073453959d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Perimeter2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.Perimeter2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.Perimeter2();
            Assert.AreEqual(1105.17460803539d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterKeplerTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterKepler();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterKepler();
            Assert.AreEqual(1088.27961854053d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterKepler();
            Assert.AreEqual(0.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterSiposTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterSipos();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterSipos();
            Assert.AreEqual(1110.95211059346d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterSipos();
            Assert.AreEqual(double.PositiveInfinity.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterNaiveTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterNaive();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterNaive();
            Assert.AreEqual(1099.55742875643d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterNaive();
            Assert.AreEqual(314.159265358979.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPeanoTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPeano();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPeano();
            Assert.AreEqual(1105.19633386438d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPeano();
            Assert.AreEqual(471.238898038469d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterEulerTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterEuler();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterEuler();
            Assert.AreEqual(1110.72073453959d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterEuler();
            Assert.AreEqual(444.288293815837d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterAlmkvistTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterAlmkvist();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterAlmkvist();
            Assert.AreEqual(1362.09460653742d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterAlmkvist();
            Assert.AreEqual(24.4893050647622.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterQuadraticTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterQuadratic();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterQuadratic();
            Assert.AreEqual(1105.15317700073d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterQuadratic();
            Assert.AreEqual(384.764949048559.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterMuirTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterMuir();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterMuir();
            Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterMuir();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterLindnerTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterLindner();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterLindner();
            Assert.AreEqual(1100.9590321664d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterLindner();
            Assert.AreEqual(333.216220361877.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterRamanujanTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterRamanujan();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterRamanujan();
            Assert.AreEqual(1105.17458954584d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterRamanujan();
            Assert.AreEqual(398.337986806673d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterSelmerTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterSelmer();
            Assert.AreEqual(694.113089432035d.ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterSelmer();
            Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterSelmer();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterRamanujan2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterRamanujan2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterRamanujan2();
            Assert.AreEqual(1105.1746080353828d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterRamanujan2();
            Assert.AreEqual(399.839065002337d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPadéSelmerTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPadéSelmer();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPadéSelmer();
            Assert.AreEqual(1105.17458037204d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPadéSelmer();
            Assert.AreEqual(397.935069454707d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPadéMichonTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPadéMichon();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPadéMichon();
            Assert.AreEqual(1105.17460778829d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPadéMichon();
            Assert.AreEqual(398.932400455847d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPadéHudsonLipkaBronshteinTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPadéHudsonLipkaBronshtein();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPadéHudsonLipkaBronshtein();
            Assert.AreEqual(1105.21776167395d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPadéHudsonLipkaBronshtein();
            Assert.AreEqual(438.513974563575d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPadéJacobsenWaadelandTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPadéJacobsenWaadeland();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPadéJacobsenWaadeland();
            Assert.AreEqual(1105.17460803497d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPadéJacobsenWaadeland();
            Assert.AreEqual(399.644779742375.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPadé3_2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPadé3_2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPadé3_2();
            Assert.AreEqual(1105.17460803547d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPadé3_2();
            Assert.AreEqual(399.815958260254.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterPadé3_3Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterPadé3_3();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterPadé3_3();
            Assert.AreEqual(1105.17460803547d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterPadé3_3();
            Assert.AreEqual(399.893168774002d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterOptimizedPeanoTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterOptimizedPeano();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterOptimizedPeano();
            Assert.AreEqual(1103.16632802551d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterOptimizedPeano();
            Assert.AreEqual(414.690230273853.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterOptimizedQuadratic1Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterOptimizedQuadratic1();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterOptimizedQuadratic1();
            Assert.AreEqual(1106.19333100019d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterOptimizedQuadratic1();
            Assert.AreEqual(396.540829769506d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterOptimizedQuadratic2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterOptimizedQuadratic2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterOptimizedQuadratic2();
            Assert.AreEqual(1115.22975969741d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterOptimizedQuadratic2();
            Assert.AreEqual(487.380298264211d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterOptimizedRamanujan1Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterOptimizedRamanujan1();
            Assert.AreEqual(628.318530717958d.ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterOptimizedRamanujan1();
            Assert.AreEqual(1105.25126379061d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterOptimizedRamanujan1();
            Assert.AreEqual(399.487026354434.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterBartolomeuMichonTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterBartolomeuMichon();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterBartolomeuMichon();
            Assert.AreEqual(1106.99713336319d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterBartolomeuMichon();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterCantrell2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCantrell2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCantrell2();
            Assert.AreEqual(1105.17405478863d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCantrell2();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterTakakazuSekiTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterTakakazuSeki();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterTakakazuSeki();
            Assert.AreEqual(1106.50464442348d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterTakakazuSeki();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterLockwoodTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterLockwood();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterLockwood();
            Assert.AreEqual(1103.68403081356d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterLockwood();
            Assert.AreEqual(double.NaN.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterSykoraRiveraCantrellsParticularlyFruitfulTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterSykoraRiveraCantrellsParticularlyFruitful();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterSykoraRiveraCantrellsParticularlyFruitful();
            Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterSykoraRiveraCantrellsParticularlyFruitful();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterYNOTTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterYNOT();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterYNOT();
            Assert.AreEqual(1105.5575586484d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterYNOT();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterCombinedPadéTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCombinedPadé();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCombinedPadé();
            Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCombinedPadé();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterCombinedPadé2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCombinedPadé2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCombinedPadé2();
            Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCombinedPadé2();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterCombinedPadéHudsonLipkaMichonTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCombinedPadéHudsonLipkaMichon();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCombinedPadéHudsonLipkaMichon();
            Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCombinedPadéHudsonLipkaMichon();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterJacobsenWaadelandHudsonLipkaTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterJacobsenWaadelandHudsonLipka();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterJacobsenWaadelandHudsonLipka();
            Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterJacobsenWaadelandHudsonLipka();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void Perimeter2_3JacobsenWaadelandTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.Perimeter2_3JacobsenWaadeland();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.Perimeter2_3JacobsenWaadeland();
            Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.Perimeter2_3JacobsenWaadeland();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void Perimeter3_3_3_2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.Perimeter3_3_3_2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.Perimeter3_3_3_2();
            Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.Perimeter3_3_3_2();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerimeterBartolomeuTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterBartolomeu();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterBartolomeu();
            Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterBartolomeu();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterRivera1Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterRivera1();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterRivera1();
            Assert.AreEqual(1100.37262930979d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterRivera1();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterRivera2Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterRivera2();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterRivera2();
            Assert.AreEqual(1105.68890980221d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterRivera2();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterCantrellTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCantrell();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCantrell();
            Assert.AreEqual(1105.19458936518d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCantrell();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterSykoraTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterSykora();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterSykora();
            Assert.AreEqual(1105.19458936518d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterSykora();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterCantrellRamanujanTest()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCantrellRamanujan();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCantrellRamanujan();
            Assert.AreEqual(1105.17460803538d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCantrellRamanujan();
            Assert.AreEqual(485.679799643358.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterK13Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterK13();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterK13();
            Assert.AreEqual(1105.13908164801d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterK13();
            Assert.AreEqual(379.223779587408d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterThomasBlankenhorn1Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterThomasBlankenhorn1();
            Assert.AreEqual(628.318513355998.ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterThomasBlankenhorn1();
            Assert.AreEqual(1105.17462238101.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterThomasBlankenhorn1();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterThomasBlankenhorn8Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterThomasBlankenhorn8();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterThomasBlankenhorn8();
            Assert.AreEqual(1105.1746116698d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterThomasBlankenhorn8();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        [TestMethod()]
        public void PerimeterCantrell2006Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterCantrell2006();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterCantrell2006();
            Assert.AreEqual(1105.17412721933d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterCantrell2006();
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PerimeterAhmadi2006Test()
        {
            // Test a perfect circle.
            Ellipse ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.PerimeterAhmadi2006();
            Assert.AreEqual((2 * Math.PI * 100).ToString(), value.ToString());

            ellipse = new Ellipse(new Point2D(), 200, 150, 0);
            value = ellipse.PerimeterAhmadi2006();
            Assert.AreEqual(1105.17434614743d.ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.PerimeterAhmadi2006();
            Assert.AreEqual(400.ToString(), value.ToString());
        }
    }
}