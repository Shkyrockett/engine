using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Geometry.Tests
{
    [TestClass()]
    public class MathsTests
    {
        [TestMethod()]
        public void Distance0Test()
        {
            //for (int i = 0; i < 100000000; i++)
            //{
                Maths.Distance(1, 1, 2, 2, 3, 3);
            //}
        }
    }
}