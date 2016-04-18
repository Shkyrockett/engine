using Engine.Geometry;
using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Experimental
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double Perimeter1(this Ellipse ellipse)
        {
            return Perimeter1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// This approximation is within about 5% of the true value, so long as a is not more than 3 times longer than b (in other words, the ellipse is not too "squashed"):
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double Perimeter1(double a, double b)
        {
            return 2 * Math.PI * (Math.Sqrt(0.5 * ((b * b) + (a * a))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double Perimeter2(this Ellipse ellipse)
        {
            return Perimeter2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference.blogspot.com/
        /// </remarks>
        private static double Perimeter2(double a, double b)
        {
            double h = (((b - a) * (b - a)) / ((b + a) * (b + a)));
            double H2 = 4 - 3 * h;
            double d = ((11 * Math.PI / (44 - 14 * Math.PI)) + 24100) - 24100 * h;
            return Math.PI * (b + a) * (1 + (3 * h) / (10 + Math.Pow(H2, 0.5)) + (1.5 * Math.Pow(h, 6) - .5 * Math.Pow(h, 12)) / d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterKepler(this Ellipse ellipse)
        {
            return PerimeterKepler(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterKepler(double a, double b)
        {
            return 2 * Math.PI * (Math.Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterSipos(this Ellipse ellipse)
        {
            return PerimeterSipos(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterSipos(double a, double b)
        {
            return 2 * Math.PI * (((a + b) * (a + b)) / ((Math.Sqrt(a) + Math.Sqrt(a)) * (Math.Sqrt(b) + Math.Sqrt(b))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterNaive(this Ellipse ellipse)
        {
            return PerimeterNaive(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterNaive(double a, double b)
        {
            return Math.PI * (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPeano(this Ellipse ellipse)
        {
            return PerimeterPeano(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterPeano(double a, double b)
        {
            return Math.PI * ((3 * (a + b) / 2) - Math.Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterEuler(this Ellipse ellipse)
        {
            return PerimeterEuler(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterEuler(double a, double b)
        {
            return 2 * Math.PI * Math.Sqrt(((a * a) + (b * b)) / 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterAlmkvist(this Ellipse ellipse)
        {
            return PerimeterAlmkvist(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterAlmkvist(double a, double b)
        {
            return 2 * Math.PI
                * ((2 * Math.Pow(a + b, 2) - Math.Pow(Math.Sqrt(a) - Math.Sqrt(b), 4))
                / (Math.Pow(Math.Sqrt(a) - Math.Sqrt(b), 2) + (2 * Math.Sqrt(2 * (a + b)) * Math.Pow(a * b, (1 / 4)))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterQuadratic(this Ellipse ellipse)
        {
            return PerimeterQuadratic(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterQuadratic(double a, double b)
        {
            return (Math.PI / 2) * Math.Sqrt((6) * (a * a + b * b) + (4 * a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterMuir(this Ellipse ellipse)
        {
            return PerimeterMuir(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterMuir(double a, double b)
        {
            return 2 * Math.PI * Math.Pow((Math.Pow(a, 3 / 2) + Math.Pow(b, 3 / 2)) / 2, 2 / 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterLindner(this Ellipse ellipse)
        {
            return PerimeterLindner(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double PerimeterLindner(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * Math.Sqrt(1 + (h / 8));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterSykoraRiveraCantrellsParticularlyFruitful(this Ellipse ellipse)
        {
            return PerimeterSykoraRiveraCantrellsParticularlyFruitful(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        private static double PerimeterSykoraRiveraCantrellsParticularlyFruitful(double a, double b)
        {
            return 4 * ((Math.PI * a * b) + ((a - b) * (a - b))) / (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterYNOT(this Ellipse ellipse)
        {
            return PerimeterYNOT(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double PerimeterYNOT(double a, double b)
        {
            double s = Math.Log(2, Math.E) / Math.Log(Math.PI / 2, Math.E);
            return 4 * Math.Pow(Math.Pow(a, s) + Math.Pow(b, s), 1 / s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCombinedPadé(this Ellipse ellipse)
        {
            return PerimeterCombinedPadé(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double PerimeterCombinedPadé(double a, double b)
        {
            double d1 = (Math.PI / 4) * (19 / 15) - 1;
            double d2 = (Math.PI / 4) * (80 / 63) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((64 + 16 * h)
                / (64 - h * h))
                + (1 - p) * ((16 + 3 * h) / (16 - h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCombinedPadé2(this Ellipse ellipse)
        {
            return PerimeterCombinedPadé2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double PerimeterCombinedPadé2(double a, double b)
        {
            double d1 = (Math.PI / 4) * (81 / 64) - 1;
            double d2 = (Math.PI / 4) * (19 / 15) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((16 - 3 * h)
                / (16 - h))
                + (1 - p) * Math.Pow(1 + (h) / 8, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterJacobsenWaadelandHudsonLipka(this Ellipse ellipse)
        {
            return PerimeterJacobsenWaadelandHudsonLipka(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double PerimeterJacobsenWaadelandHudsonLipka(double a, double b)
        {
            double d1 = (Math.PI / 4) * (61 / 48) - 1;
            double d2 = (Math.PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((256 - 48 * h - 21 * h * h)
                / (256 - 112 * h + 3 * h * h))
                + (1 - p) * ((64 - 3 * h * h) / (64 - 16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double Perimeter2_3JacobsenWaadeland(this Ellipse ellipse)
        {
            return Perimeter2_3JacobsenWaadeland(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double Perimeter2_3JacobsenWaadeland(double a, double b)
        {
            double d1 = (Math.PI / 4) * (61 / 48) - 1;
            double d2 = (Math.PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h))
                + (1 - p) * ((256 - 48 * h - 21 * h * h) / (256 - 112 * h + 3 * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double Perimeter3_3_3_2(this Ellipse ellipse)
        {
            return Perimeter3_3_3_2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double Perimeter3_3_3_2(double a, double b)
        {
            double d1 = (Math.PI / 4) * (61 / 48) - 1;
            double d2 = (Math.PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((135168 - 85760 * h - 5568 * h * h + 3867 * h * h * h)
                / (135168 - 119552 * h + 22208 * h * h - 345 * h * h * h))
                + (1 - p) * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterRamanujan(this Ellipse ellipse)
        {
            return PerimeterRamanujan(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterRamanujan(double a, double b)
        {
            return Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterSelmer(this Ellipse ellipse)
        {
            return PerimeterSelmer(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterSelmer(double a, double b)
        {
            return (Math.PI / 4) * ((6 + .5 * (Math.Pow(a - b, 2) * Math.Pow(a - b, 2) / Math.Pow(a + b, 2) * Math.Pow(a + b, 2))) * (a + b) - Math.Sqrt(2 * (a * a + 3 * a * b + b * b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterRamanujan2(this Ellipse ellipse)
        {
            return PerimeterRamanujan2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterRamanujan2(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * (1 + ((3 * h) / (10 + Math.Sqrt(4 - 3 * h))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPadéSelmer(this Ellipse ellipse)
        {
            return PerimeterPadéSelmer(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterPadéSelmer(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((16 + (3 * h)) / (16 - h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPadéMichon(this Ellipse ellipse)
        {
            return PerimeterPadéMichon(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterPadéMichon(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((64 + (16 * h)) / (64 - (h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPadéHudsonLipkaBronshtein(this Ellipse ellipse)
        {
            return PerimeterPadéHudsonLipkaBronshtein(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterPadéHudsonLipkaBronshtein(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCombinedPadéHudsonLipkaMichon(this Ellipse ellipse)
        {
            return PerimeterCombinedPadéHudsonLipkaMichon(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// Not correct.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterCombinedPadéHudsonLipkaMichon(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPadéJacobsenWaadeland(this Ellipse ellipse)
        {
            return PerimeterPadéJacobsenWaadeland(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterPadéJacobsenWaadeland(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((256 - (48 * h) - (21 * h * h)) / (256 - (112 * h) + 3 * h * h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPadé3_2(this Ellipse ellipse)
        {
            return PerimeterPadé3_2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterPadé3_2(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((3072 - (1280 * h) - (252 * h * h) + (33 * h * h * h)) / (3072 - (2048 * h) + 212 * h * h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterPadé3_3(this Ellipse ellipse)
        {
            return PerimeterPadé3_3(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterPadé3_3(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) *
                ((135168 - (85760 * h) - (5568 * h * h) + (3867 * h * h * h))
                / (135168 - (119552 * h) + (22208 * h * h) - (345 * h * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterOptimizedPeano(this Ellipse ellipse)
        {
            return PerimeterOptimizedPeano(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterOptimizedPeano(double a, double b)
        {
            double p = 1.32;
            return 2 * Math.PI * (p * ((a + b) / 2) + (1 - p) * Math.Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterOptimizedQuadratic1(this Ellipse ellipse)
        {
            return PerimeterOptimizedQuadratic1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterOptimizedQuadratic1(double a, double b)
        {
            double w = 0.7966106;
            return 2 * Math.PI * Math.Sqrt(w * ((a * a + b * b) / 2) + (1 - w) * a * b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterOptimizedQuadratic2(this Ellipse ellipse)
        {
            return PerimeterOptimizedQuadratic2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterOptimizedQuadratic2(double a, double b)
        {
            return Math.PI * Math.Sqrt(2 * (a * a + b * b) + (a - b) * (a - b) / 2.458338);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterOptimizedRamanujan1(this Ellipse ellipse)
        {
            return PerimeterOptimizedRamanujan1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterOptimizedRamanujan1(double a, double b)
        {
            double p = 3.0273;
            double w = 3;
            return 2 * Math.PI * (p * ((a + b) / 2) + (1 - p) * Math.Sqrt((a + w * b) * (w * a + b)) / (1 + w));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterBartolomeuMichon(this Ellipse ellipse)
        {
            return PerimeterBartolomeuMichon(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterBartolomeuMichon(double a, double b)
        {
            return a == b ? 2 * Math.PI * a : Math.PI * ((a - b) / Math.Atan((a - b) / (a + b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCantrell2(this Ellipse ellipse)
        {
            return PerimeterCantrell2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterCantrell2(double a, double b)
        {
            double p = 0.410117;
            double w = 74;
            return 4 * (a + b) - ((8 - 2 * Math.PI) * a * b) /
                (p * (a + b) + (1 - 2 * p) * (Math.Sqrt((a + w * b) * (w * a + b)) / (1 + w)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterTakakazuSeki(this Ellipse ellipse)
        {
            return PerimeterTakakazuSeki(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterTakakazuSeki(double a, double b)
        {
            return 2 * Math.Sqrt(Math.PI * Math.PI * a * b + 4 * (a - b) * (a - b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterLockwood(this Ellipse ellipse)
        {
            return PerimeterLockwood(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterLockwood(double a, double b)
        {
            return 4 * (((b * b) / a) * Math.Atan(a / b) + ((a * a) / b) * Math.Atan(b / a));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterBartolomeu(this Ellipse ellipse)
        {
            return PerimeterBartolomeu(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterBartolomeu(double a, double b)
        {
            double t = (Math.PI / 4) * ((a - b) / b);
            return Math.PI * Math.Sqrt(2 * (a * a + b * b)) * (Math.Sin(t) / t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterRivera1(this Ellipse ellipse)
        {
            return PerimeterRivera1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterRivera1(double a, double b)
        {
            return 4 * a + 2 * (Math.PI - 2) * a * Math.Pow(b / a, 1.456);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterRivera2(this Ellipse ellipse)
        {
            return PerimeterRivera2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterRivera2(double a, double b)
        {
            return 4 * ((Math.PI * a * b + (a - b) * (a - b)) / (a + b)) - (89 / 146) * Math.Pow((b * Math.Sqrt(a) - a * Math.Sqrt(b)) / (a + b), 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCantrell(this Ellipse ellipse)
        {
            return PerimeterSykora(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterCantrell(double a, double b)
        {
            double s = Math.Log(2) / Math.Log(2 / (4 - Math.PI));
            return 4 * (a + b) - ((2 * (4 - Math.PI) * a * b) / Math.Pow(Math.Pow(a, s) + Math.Pow(b, s), 1 / s));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterSykora(this Ellipse ellipse)
        {
            return PerimeterSykora(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterSykora(double a, double b)
        {
            return 4 * (((Math.PI * a * b + (a - b) * (a - b))) / (a + b)) - 0.5 * ((a * b) / (a + b)) * (((a - b) * (a - b)) / (Math.PI * a * b + (a + b) * (a + b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCantrellRamanujan(this Ellipse ellipse)
        {
            return PerimeterCantrellRamanujan(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double PerimeterCantrellRamanujan(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * (1 + ((3 * h) / (10 + Math.Sqrt(4 - 3 * h))) + ((4 / Math.PI) - ((14) / (11))) * Math.Pow(h, 12));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterK13(this Ellipse ellipse)
        {
            return PerimeterK13(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double PerimeterK13(double a, double b)
        {
            return Math.PI * (((a + b) / 2) + Math.Sqrt((a * a + b * b) / 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterThomasBlankenhorn1(this Ellipse ellipse)
        {
            return PerimeterThomasBlankenhorn1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// This one is not as good with a circle. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://ellipse-circumference2.blogspot.com/2011/12/accurate-online-ellipse-circumference.html</remarks>
        private static double PerimeterThomasBlankenhorn1(double a, double b)
        {
            double X1 = a;
            double X2 = b;
            double HMX = Math.Max(X1, X2);
            double HMN = Math.Min(X1, X2);
            double H1 = HMN / HMX;
            return 2 * Math.PI * HMX * ((2 / Math.PI) + 0.0000122 * Math.Pow(H1, 0.6125) - 0.0021973 * Math.Pow(H1, 1.225) + 0.919315 * Math.Pow(H1, 1.8375) - 1.0359227 * Math.Pow(H1, 2.45) + 0.861913 * Math.Pow(H1, 3.0625) - 0.7274398 * Math.Pow(H1, 3.675) + 0.6352295 * Math.Pow(H1, 4.2875) - 0.436051 * Math.Pow(H1, 4.9) + 0.1818904 * Math.Pow(H1, 5.5125) - 0.0333691 * Math.Pow(H1, 6.125));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterThomasBlankenhorn8(this Ellipse ellipse)
        {
            return PerimeterThomasBlankenhorn8(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference3.blogspot.com/
        /// </remarks>
        private static double PerimeterThomasBlankenhorn8(double a, double b)
        {
            double X1 = a;
            double X2 = b;
            double HMX = Math.Max(X1, X2);
            double HMN = Math.Min(X1, X2);
            double H1 = HMN / HMX;
            return HMX * (4 + (3929 * Math.Pow(H1, 1.5) + 1639157 * Math.Pow(H1, 2) + 19407215 * Math.Pow(H1, 2.5) + 24302653 * Math.Pow(H1, 3) + 12892432 * Math.Pow(H1, 3.5)) / (86251 + 1924742 * Math.Pow(H1, 0.5) + 6612384 * Math.Pow(H1, 1) + 7291509 * Math.Pow(H1, 1.5) + 6436977 * Math.Pow(H1, 2) + 3158719 * Math.Pow(H1, 2.5)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterCantrell2006(this Ellipse ellipse)
        {
            return PerimeterCantrell2006(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox06.html
        /// </remarks>
        private static double PerimeterCantrell2006(double a, double b)
        {
            double p = 3.982901;
            double q = 66.71674;
            double s = 18.31287;
            double t = 23.39728;
            double r = 4 * ((4 - Math.PI) * (4 * s + t + 16) - (4 * p + q));
            return 4 * (a + b)
                - ((a * b) / (a + b))
                * ((p * (a + b) * (a + b) + q * a * b + r * ((a * b) / (a + b)) * ((a * b) / (a + b)))
                / ((a + b) * (a + b) + s * a * b + t * ((a * b) / (a + b)) * ((a * b) / (a + b))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double PerimeterAhmadi2006(this Ellipse ellipse)
        {
            return PerimeterAhmadi2006(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox06.html
        /// </remarks>
        private static double PerimeterAhmadi2006(double a, double b)
        {
            double c1 = Math.PI - 3;
            double c2 = Math.PI;
            double c3 = 0.5;
            double c4 = (Math.PI + 1) / 2;
            double c5 = 4;
            double k = 1 - ((c1 * a * b) / ((a * a + b * b) + c2 * Math.Sqrt(c3 * a * b * a * b + a * b * Math.Sqrt(a * b * (c4 * (a * a + b * b) + c5 * a * b)))));
            return 4 * ((Math.PI * a * b + k * (a - b) * (a - b)) / (a + b));
        }
    }
}
