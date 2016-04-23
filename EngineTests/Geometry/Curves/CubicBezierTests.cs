using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class CubicBezierTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            CubicBezier bezier = new CubicBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150), new Point2D(44, 66));
            string value = bezier.ToString();
            Assert.AreEqual("", value);
        }
    }
}