using Engine;
using System.Collections.Generic;

namespace EngineTests
{
    /// <summary>
    /// The commons class.
    /// </summary>
    public static class Commons
    {
        /// <summary>
        /// The quadratic beziers class.
        /// </summary>
        public static class QuadraticBeziers
        {
            /// <summary>
            /// The test1.
            /// </summary>
            public static readonly (double, double, double, double, double, double) test1 = (0d, 0d, 0d, 0d, 0d, 0d);

            /// <summary>
            /// The test2.
            /// </summary>
            public static readonly (double, double, double, double, double, double) test2 = (0d, 0d, 10d, 10d, 20d, 0d);

            /// <summary>
            /// The test3.
            /// </summary>
            public static readonly (double, double, double, double, double, double) test3 = (0d, 10d, 10d, 0d, 20d, 10d);

            /// <summary>
            /// The test4.
            /// </summary>
            public static readonly (double, double, double, double, double, double) test4 = (5d, 0d, 10d, 10d, 20d, 0d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static readonly (double, double, double, double, double, double) test5 = (83d, 214d, 335d, 173d, 91d, 137d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static readonly (double, double, double, double, double, double) test6 = (92d, 233d, 152d, 30d, 198d, 227d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static readonly (double, double, double, double, double, double) test7 = (123d, 47d, 146d, 255d, 188d, 47d);
        }

        /// <summary>
        /// The cubic beziers class.
        /// </summary>
        public static class CubicBeziers
        {
            /// <summary>
            /// The test1.
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test1 = (0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d);

            /// <summary>
            /// The test2.
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test2 = (0d, 10d, 6.66666666666667d, 3.33333333333333d, 13.3333333333333d, 3.33333333333333d, 20d, 10d);

            /// <summary>
            /// The test3.
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test3 = (0d, 0d, 6.66666666666667d, 6.66666666666667d, 13.3333333333333d, 6.66666666666667d, 20d, 0d);

            /// <summary>
            /// The test4.
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test4 = (5d, 0d, 8.33333333333333d, 6.66666666666667d, 13.3333333333333d, 6.66666666666667d, 20d, 0d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test5 = (83d, 214d, 251d, 186.666666666667d, 253.666666666667d, 161d, 91d, 137d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test6 = (92d, 233d, 132d, 97.6666666666667d, 167.333333333333d, 95.6666666666667d, 198d, 227d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test7 = (123d, 47d, 138.333333333333d, 185.666666666667d, 160d, 185.666666666667d, 188d, 47d);

            /// <summary>
            /// KLD Cubic test
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test8 = (203d, 140d, 206d, 359d, 245d, 6d, 248d, 212d);

            /// <summary>
            /// KLD Cubic test
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test9 = (177d, 204d, 441d, 204d, 8d, 149d, 265d, 154d);

            /// <summary>
            /// KLD Cubic test
            /// </summary>
            public static readonly (double, double, double, double, double, double, double, double) test10 = (171d, 143d, 22d, 132d, 330d, 64d, 107d, 65d);
        }

        /// <summary>
        /// A Listing of primitive polygons that can exhibit odd behavior.
        /// </summary>
        public static class Polygons
        {
            /// <summary>
            /// Clockwise Square
            /// </summary>
            public static readonly List<Point2D> SquareClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Counter Clockwise Square
            /// </summary>
            public static readonly List<Point2D> SquareCounterClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(25, 100),
                new Point2D(100, 100),
                new Point2D(100, 25),
            };

            /// <summary>
            /// Clockwise winding Right Triangle from the Top Left 
            /// </summary>
            public static readonly List<Point2D> RightTriangleTopLeftClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Counter Clockwise winding Right Triangle from the Top Left 
            /// </summary>
            public static readonly List<Point2D> RightTriangleTopLeftCounterClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(25, 100),
                new Point2D(100, 25),
            };

            /// <summary>
            /// Clockwise winding Right Triangle from the Bottom Right 
            /// </summary>
            public static readonly List<Point2D> RightTriangleBottomRightClockwise = new List<Point2D>
            {
                new Point2D(100, 100),
                new Point2D(25, 100),
                new Point2D(100, 25),
            };

            /// <summary>
            /// Counter Clockwise winding Right Triangle from the Bottom Right 
            /// </summary>
            public static readonly List<Point2D> RightTriangleBottomRightCounterClockwise = new List<Point2D>
            {
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Right Reversed Bow-tie
            /// </summary>
            public static readonly List<Point2D> BowTieRightReversed = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Left Reversed Bow-tie
            /// </summary>
            public static readonly List<Point2D> BowTieLeftReversed = new List<Point2D>
            {
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// C Shape
            /// </summary>
            public static readonly List<Point2D> CShape = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 50),
                new Point2D(50, 50),
                new Point2D(50, 75),
                new Point2D(100, 75),
                new Point2D(100, 100),
                new Point2D(25, 100),
            };

            /// <summary>
            /// n Shape
            /// </summary>
            public static readonly List<Point2D> NShape = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(75, 100),
                new Point2D(75, 50),
                new Point2D(50, 50),
                new Point2D(50, 100),
                new Point2D(25, 100),
            };

            /// <summary>
            /// C Bow-tie hole Shape
            /// </summary>
            public static readonly List<Point2D> CBowTieHoleShape = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 50),
                new Point2D(50, 75),
                new Point2D(50, 50),
                new Point2D(100, 75),
                new Point2D(100, 100),
                new Point2D(25, 100),
            };

            /// <summary>
            /// n Bow-tie hole Shape
            /// </summary>
            public static readonly List<Point2D> NBowTieHoleShape = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(75, 100),
                new Point2D(50, 50),
                new Point2D(75, 50),
                new Point2D(50, 100),
                new Point2D(25, 100),
            };
        }

        /// <summary>
        /// The ellipses class.
        /// </summary>
        public static class Ellipses
        {
            /// <summary>
            /// The ellipse 0 0 1 1 0.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_0 = new Ellipse2D(0, 0, 1, 1, 0d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 15.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_15 = new Ellipse2D(0, 0, 1, 1, 15d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 30.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_30 = new Ellipse2D(0, 0, 1, 1, 30d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 45.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_45 = new Ellipse2D(0, 0, 1, 1, 45d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 60.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_60 = new Ellipse2D(0, 0, 1, 1, 60d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 75.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_75 = new Ellipse2D(0, 0, 1, 1, 75d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 90.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_90 = new Ellipse2D(0, 0, 1, 1, 90d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 105.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_105 = new Ellipse2D(0, 0, 1, 1, 105d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 120.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_120 = new Ellipse2D(0, 0, 1, 1, 120d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 135.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_135 = new Ellipse2D(0, 0, 1, 1, 135d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 150.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_150 = new Ellipse2D(0, 0, 1, 1, 150d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 165.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_165 = new Ellipse2D(0, 0, 1, 1, 165d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 180.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_180 = new Ellipse2D(0, 0, 1, 1, 180d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 195.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_195 = new Ellipse2D(0, 0, 1, 1, 195d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 210.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_210 = new Ellipse2D(0, 0, 1, 1, 210d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 225.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_225 = new Ellipse2D(0, 0, 1, 1, 225d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 240.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_240 = new Ellipse2D(0, 0, 1, 1, 240d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 255.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_255 = new Ellipse2D(0, 0, 1, 1, 255d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 270.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_270 = new Ellipse2D(0, 0, 1, 1, 270d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 285.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_285 = new Ellipse2D(0, 0, 1, 1, 285d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 300.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_300 = new Ellipse2D(0, 0, 1, 1, 300d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 315.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_315 = new Ellipse2D(0, 0, 1, 1, 315d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 330.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_330 = new Ellipse2D(0, 0, 1, 1, 330d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 345.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_345 = new Ellipse2D(0, 0, 1, 1, 345d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 1 360.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_1_360 = new Ellipse2D(0, 0, 1, 1, 360d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 0.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_0 = new Ellipse2D(0, 0, 1, 2, 0d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 15.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_15 = new Ellipse2D(0, 0, 1, 2, 15d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 30.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_30 = new Ellipse2D(0, 0, 1, 2, 30d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 45.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_45 = new Ellipse2D(0, 0, 1, 2, 45d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 60.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_60 = new Ellipse2D(0, 0, 1, 2, 60d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 75.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_75 = new Ellipse2D(0, 0, 1, 2, 75d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 90.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_90 = new Ellipse2D(0, 0, 1, 2, 90d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 105.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_105 = new Ellipse2D(0, 0, 1, 2, 105d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 120.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_120 = new Ellipse2D(0, 0, 1, 2, 120d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 135.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_135 = new Ellipse2D(0, 0, 1, 2, 135d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 150.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_150 = new Ellipse2D(0, 0, 1, 2, 150d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 165.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_165 = new Ellipse2D(0, 0, 1, 2, 165d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 180.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_180 = new Ellipse2D(0, 0, 1, 2, 180d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 195.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_195 = new Ellipse2D(0, 0, 1, 2, 195d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 210.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_210 = new Ellipse2D(0, 0, 1, 2, 210d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 225.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_225 = new Ellipse2D(0, 0, 1, 2, 225d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 240.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_240 = new Ellipse2D(0, 0, 1, 2, 240d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 255.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_255 = new Ellipse2D(0, 0, 1, 2, 255d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 270.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_270 = new Ellipse2D(0, 0, 1, 2, 270d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 285.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_285 = new Ellipse2D(0, 0, 1, 2, 285d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 300.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_300 = new Ellipse2D(0, 0, 1, 2, 300d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 315.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_315 = new Ellipse2D(0, 0, 1, 2, 315d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 330.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_330 = new Ellipse2D(0, 0, 1, 2, 330d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 345.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_345 = new Ellipse2D(0, 0, 1, 2, 345d.DegreesToRadians());

            /// <summary>
            /// The ellipse 0 0 1 2 360.
            /// </summary>
            public static readonly Ellipse2D ellipse_0_0_1_2_360 = new Ellipse2D(0, 0, 1, 2, 360d.DegreesToRadians());
        }
    }
}
