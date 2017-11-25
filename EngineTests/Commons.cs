using System.Collections.Generic;
using Engine;

namespace EngineTests
{
    /// <summary>
    /// 
    /// </summary>
    public static class Commons
    {
        /// <summary>
        /// 
        /// </summary>
        public static class QuadraticBeziers
        {
            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double) test1 = 
                (0d, 0d, 0d, 0d, 0d, 0d);

            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double) test2 = 
                (0d, 0d, 10d, 10d, 20d, 0d);

            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double) test3 = 
                (0d, 10d, 10d, 0d, 20d, 10d);

            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double) test4 = 
                (5d, 0d, 10d, 10d, 20d, 0d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static (double, double, double, double, double, double) test5 = 
                (83d, 214d, 335d, 173d, 91d, 137d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static (double, double, double, double, double, double) test6 = 
                (92d, 233d, 152d, 30d, 198d, 227d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static (double, double, double, double, double, double) test7 = 
                (123d, 47d, 146d, 255d, 188d, 47d);
        }

        /// <summary>
        /// 
        /// </summary>
        public static class CubicBeziers
        {
            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test1 = 
                (0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d);

            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test2 = 
                (0d, 10d, 6.66666666666667d, 3.33333333333333d, 13.3333333333333d, 3.33333333333333d, 20d, 10d);

            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test3 = 
                (0d, 0d, 6.66666666666667d, 6.66666666666667d, 13.3333333333333d, 6.66666666666667d, 20d, 0d);

            /// <summary>
            /// 
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test4 = 
                (5d, 0d, 8.33333333333333d, 6.66666666666667d, 13.3333333333333d, 6.66666666666667d, 20d, 0d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test5 = 
                (83d, 214d, 251d, 186.666666666667d, 253.666666666667d, 161d, 91d, 137d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test6 = 
                (92d, 233d, 132d, 97.6666666666667d, 167.333333333333d, 95.6666666666667d, 198d, 227d);

            /// <summary>
            /// KLD Quadratic test
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test7 = 
                (123d, 47d, 138.333333333333d, 185.666666666667d, 160d, 185.666666666667d, 188d, 47d);

            /// <summary>
            /// KLD Cubic test
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test8 = 
                (203d, 140d, 206d, 359d, 245d, 6d, 248d, 212d);

            /// <summary>
            /// KLD Cubic test
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test9 = 
                (177d, 204d, 441d, 204d, 8d, 149d, 265d, 154d);

            /// <summary>
            /// KLD Cubic test
            /// </summary>
            public static (double, double, double, double, double, double, double, double) test10 = 
                (171d, 143d, 22d, 132d, 330d, 64d, 107d, 65d);

        }

        /// <summary>
        /// A Listing of primitive polygons that can exhibit odd behavior.
        /// </summary>
        public static class Polygons
        {
            /// <summary>
            /// Clockwise Square
            /// </summary>
            public static List<Point2D> SquareClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Counter Clockwise Square
            /// </summary>
            public static List<Point2D> SquareCounterClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(25, 100),
                new Point2D(100, 100),
                new Point2D(100, 25),
            };

            /// <summary>
            /// Clockwise winding Right Triangle from the Top Left 
            /// </summary>
            public static List<Point2D> RightTriangleTopLeftClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Counter Clockwise winding Right Triangle from the Top Left 
            /// </summary>
            public static List<Point2D> RightTriangleTopLeftCounterClockwise = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(25, 100),
                new Point2D(100, 25),
            };

            /// <summary>
            /// Clockwise winding Right Triangle from the Bottom Right 
            /// </summary>
            public static List<Point2D> RightTriangleBottomRightClockwise = new List<Point2D>
            {
                new Point2D(100, 100),
                new Point2D(25, 100),
                new Point2D(100, 25),
            };

            /// <summary>
            /// Counter Clockwise winding Right Triangle from the Bottom Right 
            /// </summary>
            public static List<Point2D> RightTriangleBottomRightCounterClockwise = new List<Point2D>
            {
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Right Reversed Bow-tie
            /// </summary>
            public static List<Point2D> BowTieRightReversed = new List<Point2D>
            {
                new Point2D(25, 25),
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// Left Reversed Bow-tie
            /// </summary>
            public static List<Point2D> BowTieLeftReversed = new List<Point2D>
            {
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 25),
                new Point2D(25, 100),
            };

            /// <summary>
            /// C Shape
            /// </summary>
            public static List<Point2D> CShape = new List<Point2D>
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
            public static List<Point2D> NShape = new List<Point2D>
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
            public static List<Point2D> CBowTieHoleShape = new List<Point2D>
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
            public static List<Point2D> NBowTieHoleShape = new List<Point2D>
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
        /// 
        /// </summary>
        public static class Ellipses
        {
            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_0 = new Ellipse(0, 0, 1, 1, 0d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_15 = new Ellipse(0, 0, 1, 1, 15d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_30 = new Ellipse(0, 0, 1, 1, 30d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_45 = new Ellipse(0, 0, 1, 1, 45d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_60 = new Ellipse(0, 0, 1, 1, 60d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_75 = new Ellipse(0, 0, 1, 1, 75d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_90 = new Ellipse(0, 0, 1, 1, 90d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_105 = new Ellipse(0, 0, 1, 1, 105d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_120 = new Ellipse(0, 0, 1, 1, 120d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_135 = new Ellipse(0, 0, 1, 1, 135d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_150 = new Ellipse(0, 0, 1, 1, 150d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_165 = new Ellipse(0, 0, 1, 1, 165d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_180 = new Ellipse(0, 0, 1, 1, 180d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_195 = new Ellipse(0, 0, 1, 1, 195d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_210 = new Ellipse(0, 0, 1, 1, 210d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_225 = new Ellipse(0, 0, 1, 1, 225d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_240 = new Ellipse(0, 0, 1, 1, 240d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_255 = new Ellipse(0, 0, 1, 1, 255d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_270 = new Ellipse(0, 0, 1, 1, 270d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_285 = new Ellipse(0, 0, 1, 1, 285d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_300 = new Ellipse(0, 0, 1, 1, 300d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_315 = new Ellipse(0, 0, 1, 1, 315d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_330 = new Ellipse(0, 0, 1, 1, 330d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_345 = new Ellipse(0, 0, 1, 1, 345d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_1_360 = new Ellipse(0, 0, 1, 1, 360d.ToRadians());


            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_0 = new Ellipse(0, 0, 1, 2, 0d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_15 = new Ellipse(0, 0, 1, 2, 15d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_30 = new Ellipse(0, 0, 1, 2, 30d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_45 = new Ellipse(0, 0, 1, 2, 45d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_60 = new Ellipse(0, 0, 1, 2, 60d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_75 = new Ellipse(0, 0, 1, 2, 75d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_90 = new Ellipse(0, 0, 1, 2, 90d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_105 = new Ellipse(0, 0, 1, 2, 105d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_120 = new Ellipse(0, 0, 1, 2, 120d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_135 = new Ellipse(0, 0, 1, 2, 135d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_150 = new Ellipse(0, 0, 1, 2, 150d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_165 = new Ellipse(0, 0, 1, 2, 165d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_180 = new Ellipse(0, 0, 1, 2, 180d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_195 = new Ellipse(0, 0, 1, 2, 195d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_210 = new Ellipse(0, 0, 1, 2, 210d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_225 = new Ellipse(0, 0, 1, 2, 225d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_240 = new Ellipse(0, 0, 1, 2, 240d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_255 = new Ellipse(0, 0, 1, 2, 255d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_270 = new Ellipse(0, 0, 1, 2, 270d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_285 = new Ellipse(0, 0, 1, 2, 285d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_300 = new Ellipse(0, 0, 1, 2, 300d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_315 = new Ellipse(0, 0, 1, 2, 315d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_330 = new Ellipse(0, 0, 1, 2, 330d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_345 = new Ellipse(0, 0, 1, 2, 345d.ToRadians());

            /// <summary>
            /// 
            /// </summary>
            public static Ellipse ellipse_0_0_1_2_360 = new Ellipse(0, 0, 1, 2, 360d.ToRadians());

        }
    }
}
