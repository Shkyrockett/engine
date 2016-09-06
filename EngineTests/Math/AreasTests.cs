using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class AreasTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void ArcTest()
        {
            double radiusLimit = 1;
            double angleLowerLimit = -15;// -360;
            double angleUpperLimit = 15;// 360;

            var results = new Dictionary<(double, double), double>
            {
                { (0, -15), 0 }, { (1, -15), 0 },
                { (0, -14), 0 }, { (1, -14), 0 },
                { (0, -13), 0 }, { (1, -13), 0 },
                { (0, -12), 0 }, { (1, -12), 0 },
                { (0, -11), 0 }, { (1, -11), 0 },
                { (0, -10), 0 }, { (1, -10), 0 },
                { (0, -9), 0 }, { (1, -9), 0 },
                { (0, -8), 0 }, { (1, -8), 0 },
                { (0, -7), 0 }, { (1, -7), 0 },
                { (0, -6), 0 }, { (1, -6), 0 },
                { (0, -5), 0 }, { (1, -5), 0 },
                { (0, -4), 0 }, { (1, -4), 0 },
                { (0, -3), 0 }, { (1, -3), 0 },
                { (0, -2), 0 }, { (1, -2), 0 },
                { (0, -1), 0 }, { (1, -1), 0 },
                { (0, 0), 0 }, { (1, 0), 0 },
                { (0, 1), 0 }, { (1, 1), 0 },
                { (0, 2), 0 }, { (1, 2), 0 },
                { (0, 3), 0 }, { (1, 3), 0 },
                { (0, 4), 0 }, { (1, 4), 0 },
                { (0, 5), 0 }, { (1, 5), 0 },
                { (0, 6), 0 }, { (1, 6), 0 },
                { (0, 7), 0 }, { (1, 7), 0 },
                { (0, 8), 0 }, { (1, 8), 0 },
                { (0, 9), 0 }, { (1, 9), 0 },
                { (0, 10), 0 }, { (1, 10), 0 },
                { (0, 11), 0 }, { (1, 11), 0 },
                { (0, 12), 0 }, { (1, 12), 0 },
                { (0, 13), 0 }, { (1, 13), 0 },
                { (0, 14), 0 }, { (1, 14), 0 },
                { (0, 15), 0 }, { (1, 15), 0 },
            };

            for (double radius = 0; radius < radiusLimit; radius += 1d)
            {
                for (double sweepAngle = angleLowerLimit; sweepAngle < angleUpperLimit; sweepAngle++)
                {
                    var area = Areas.Arc(radius, sweepAngle.ToRadians());
                    Assert.AreEqual(results[(radius, sweepAngle)], area, double.Epsilon);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void CircleTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void EllipseTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void RectangleTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SquareTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void PolygonTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SignedPolygonTest()
        {
            throw new NotImplementedException();
        }
    }
}