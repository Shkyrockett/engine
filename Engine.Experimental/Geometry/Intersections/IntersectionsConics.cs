using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public enum ConicType
    {
        /// <summary>
        /// The line
        /// </summary>
        Line,

        /// <summary>
        /// The parabola
        /// </summary>
        Parabola,

        /// <summary>
        /// The circle
        /// </summary>
        Circle,

        /// <summary>
        /// The ellipse
        /// </summary>
        Ellipse,

        /// <summary>
        /// The rectangular hyperbola
        /// </summary>
        RectangularHyperbola,

        /// <summary>
        /// The hyperbola
        /// </summary>
        Hyperbola
    }

    /// <summary>
    /// 
    /// </summary>
    public static partial class IntersectionsConics
    {
        /// <summary>
        /// Converts the circle to conic section sextic.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="r">a.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/calculate-the-formula-for-an-ellipse-selected-by-the-user-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) CircleToConicSection(double h, double k, double r)
        {
            var r2 = r * r;
            var h2 = h * h;
            var k2 = k * k;

            var c0 = 1d / r2;
            var c1 = 1d / r2;
            var c2 = 0d;
            var c3 = -2d * h / r2;
            var c4 = -2d * k / r2;
            var c5 = h2 / r2 + k2 / r2 - 1d;
            return (c0, c1, c2, c3, c4, c5);
        }

        /// <summary>
        /// Converts the ellipse to conic section sextic.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/calculate-the-formula-for-an-ellipse-selected-by-the-user-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) ConvertEllipseToConicSectionSextic(double a, double b, double h, double k)
        {
            var a2 = a * a;
            var b2 = b * b;
            var h2 = h * h;
            var k2 = k * k;

            var c0 = 1d / a2;
            var c1 = 1d / b2;
            var c2 = 0d;
            var c3 = -2d * h / a2;
            var c4 = -2d * k / b2;
            var c5 = h2 / a2 + k2 / b2 - 1d;
            return (c0, c1, c2, c3, c4, c5);
        }

        /// <summary>
        /// Converts the ellipse to conic section sextic.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="theta">The theta.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/2989928
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) ConvertEllipseToConicSectionSextic(double a, double b, double h, double k, double theta)
            => ConvertEllipseToConicSectionSextic(a, b, h, k, Cos(theta), Sin(theta));

        /// <summary>
        /// Converts the ellipse to conic section sextic.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="cos">The cos.</param>
        /// <param name="sin">The sin.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/2989928
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) ConvertEllipseToConicSectionSextic(double a, double b, double h, double k, double cos, double sin)
        {
            var cos2 = cos * cos;
            var sin2 = sin * sin;
            var sin2t = 2d * sin * cos;
            var a2 = a * a;
            var b2 = b * b;
            var h2 = h * h;
            var k2 = k * k;

            var c0 = cos2 / a2 + sin2 / b2;
            var c1 = sin2 / a2 + cos2 / b2;
            var c2 = sin2t / a2 - sin2t / b2;
            var c3 = -(2d * h * cos2) / a2 - k * sin2t / a2 - 2d * h * sin2 / b2 + k * sin2t / b2;
            var c4 = -(h * sin2t) / a2 - 2d * k * sin2 / a2 + h * sin2t / b2 - 2d * k * cos2 / b2;
            var c5 = h2 * cos2 / a2 + h * k * sin2t / a2 + k2 * sin2 / a2 + h2 * sin2 / b2 - h * k * sin2t / b2 + k2 * cos2 / b2 - 1d;

            return (c0, c1, c2, c3, c4, c5);
        }

        /// <summary>
        /// Finds the conic section.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) FindConicSectionSextic(Point2D a, Point2D b, Point2D c, Point2D d, Point2D e)
        {
            const int rows = 5;
            const int cols = 5;

            // Build the augmented matrix.
            var points = new[] { a, b, c, d, e };
            var arr = new double[rows, cols + 2];
            for (var row = 0; row < rows; row++)
            {
                arr[row, 0] = points[row].X * points[row].X;
                arr[row, 1] = points[row].X * points[row].Y;
                arr[row, 2] = points[row].Y * points[row].Y;
                arr[row, 3] = points[row].X;
                arr[row, 4] = points[row].Y;
                arr[row, 5] = -1;
                arr[row, 6] = 0;
            }

            // Perform Gaussian elimination.
            for (var r = 0; r < rows - 1; r++)
            {
                // Zero out all entries in column r after this row.
                // See if this row has a non-zero entry in column r.
                if (Abs(arr[r, r]) < double.Epsilon)
                {
                    // Too close to zero. Try to swap with a later row.
                    for (var r2 = r + 1; r2 < rows; r2++)
                    {
                        if (Abs(arr[r2, r]) > double.Epsilon)
                        {
                            // This row will work. Swap them.
                            for (var j = 0; j <= cols; j++)
                            {
                                var tmp = arr[r, j];
                                arr[r, j] = arr[r2, j];
                                arr[r2, j] = tmp;
                            }
                            break;
                        }
                    }
                }

                // If this row has a non-zero entry in column r, use it.
                if (Abs(arr[r, r]) > double.Epsilon)
                {
                    // Zero out this column in later rows.
                    for (var r2 = r + 1; r2 < rows; r2++)
                    {
                        var factor = -arr[r2, r] / arr[r, r];
                        for (var j = r; j <= cols; j++)
                        {
                            arr[r2, j] = arr[r2, j] + factor * arr[r, j];
                        }
                    }
                }
            }

            // See if we have a solution.
            if (arr[rows - 1, cols - 1] == 0)
            {
                // We have no solution.
                // See if all of the entries in this row are 0.
                var all_zeros = true;
                for (var j = 0; j <= cols + 1; j++)
                {
                    if (arr[rows - 1, j] != 0)
                    {
                        all_zeros = false;
                        break;
                    }
                }
                if (all_zeros)
                {
                    //MessageBox.Show("The solution is not unique");
                }
                else
                {
                    //MessageBox.Show("There is no solution");
                }
                return (0, 0, 0, 0, 0, 0);
            }
            else
            {
                // Back solve.
                for (var r = rows - 1; r >= 0; r--)
                {
                    var tmp = arr[r, cols];
                    for (var r2 = r + 1; r2 < rows; r2++)
                    {
                        tmp -= arr[r, r2] * arr[r2, cols + 1];
                    }

                    arr[r, cols + 1] = tmp / arr[r, r];
                }

                // Return the results.
                return (arr[0, cols + 1], arr[1, cols + 1], arr[2, cols + 1], arr[3, cols + 1], arr[4, cols + 1], 1);
            }
        }

        /// <summary>
        /// Rescales the sextic.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) RescaleSextic(double a, double b, double c, double d, double e, double f)
        {
            var min = SexticAbsMin(a, b, c, d, e, f);
            var scale = 1d / min;
            return (a *= scale, b *= scale, c *= scale, d *= scale, e *= scale, f *= scale);
        }

        /// <summary>
        /// Find the nearest value to 0 in a Sextic polynomial.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SexticAbsMin(double a, double b, double c, double d, double e, double f)
        {
            var min = Abs(a);
            min = Min(min, Abs(b));
            min = Min(min, Abs(c));
            min = Min(min, Abs(d));
            min = Min(min, Abs(e));
            min = Min(min, Abs(f));
            return min;
        }

        /// <summary>
        /// Gets the type of the conic.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConicType GetConicType(double a, double b, double c)
        {
            // Calculate the determinant.
            var determinant = b * b - 4d * a * c;
            return Abs(determinant) < double.Epsilon
                ? ConicType.Parabola
                : determinant < 0d
                    ? (Abs(a) < double.Epsilon) && (Abs(b) < double.Epsilon)
                    ? ConicType.Circle : ConicType.Ellipse
                    : Abs(a + c) < double.Epsilon
                    ? ConicType.RectangularHyperbola : ConicType.Hyperbola;
        }

        /// <summary>
        /// Calculate G(x).
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="f1">The f1.</param>
        /// <param name="sign1">The sign1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="f2">The f2.</param>
        /// <param name="sign2">The sign2.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InterpolateConicDifferenceY(double x,
            double a1, double b1, double c1, double d1, double e1, double f1, int sign1,
            double a2, double b2, double c2, double d2, double e2, double f2, int sign2)
            => InterpolateConicY(x, a1, b1, c1, d1, e1, f1, sign1) - InterpolateConicY(x, a2, b2, c2, d2, e2, f2, sign2);

        /// <summary>
        /// Calculate G1(x).
        /// root_sign is -1 or 1.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="rootSign">The root sign.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InterpolateConicY(double x, double a, double b, double c, double d, double e, double f, int rootSign)
        {
            var result = b * x + e;
            result *= result;
            result -= 4d * c * (a * x * x + d * x + f);
            result = rootSign * Sqrt(result);
            result = -(b * x + e) + result;
            result = result / 2d / c;
            return result;
        }

        /// <summary>
        /// Calculate G'(x).
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="f1">The f1.</param>
        /// <param name="sign1">The sign1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="f2">The f2.</param>
        /// <param name="sign2">The sign2.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InterpolateConicDifferencePrimeY(double x,
            double a1, double b1, double c1, double d1, double e1, double f1, int sign1,
            double a2, double b2, double c2, double d2, double e2, double f2, int sign2)
            => InterpolateConicPrimeY(x, a1, b1, c1, d1, e1, f1, sign1) - InterpolateConicPrimeY(x, a2, b2, c2, d2, e2, f2, sign2);

        /// <summary>
        /// Calculate G1'(x).
        /// root_sign is -1 or 1.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="rootSign">The root sign.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InterpolateConicPrimeY(double x, double a, double b, double c, double d, double e, double f, int rootSign)
        {
            var numerator = 2d * (b * x + e) * b - 4d * c * (2d * a * x + d);
            var denominator = 2d * Sqrt((b * x + e) * (b * x + e) - 4d * c * (a * x * x + d * x + f));
            var result = -b + rootSign * numerator / denominator;
            result = result / 2d / c;

            return result;
        }

        /// <summary>
        /// Find the points of intersection between
        /// a conic section and a line.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-a-line-intersects-a-conic-section-in-c/
        /// </acknowledgment>
        public static List<Point2D> IntersectConicAndLine(
            double a, double b, double c, double d, double e, double f,
            double x1, double y1, double x2, double y2)
        {
            // Get dx and dy;
            var dx = x2 - x1;
            var dy = y2 - y1;

            // Calculate the coefficients of the intersection for the quadratic formula.
            var a1 = a * dx * dx + b * dx * dy + c * dy * dy;
            var b1 = a * 2d * x1 * dx + b * x1 * dy + b * y1 * dx + c * 2d * y1 * dy + d * dx + e * dy;
            var c1 = a * x1 * x1 + b * x1 * y1 + c * y1 * y1 + d * x1 + e * y1 + f;

            // Check the determinant to see how many solutions there are.
            var solutions = new List<Point2D>();
            var det = b1 * b1 - 4d * a1 * c1;

            if (det == 0)
            {
                var t = -b1 / (2d * a1);
                solutions.Add(new Point2D(x1 + t * dx, y1 + t * dy));
            }
            else if (det > 0)
            {
                var root = Sqrt(b1 * b1 - 4d * a1 * c1);
                var t1 = (-b1 + root) / (2d * a1);
                solutions.Add(new Point2D(x1 + t1 * dx, y1 + t1 * dy));
                var t2 = (-b1 - root) / (2d * a1);
                solutions.Add(new Point2D(x1 + t2 * dx, y1 + t2 * dy));
            }

            return solutions;
        }

        /// <summary>
        /// Finds the points of intersection.
        /// </summary>
        /// <param name="xmin">The xmin.</param>
        /// <param name="xmax">The xmax.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="f1">The f1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="f2">The f2.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> FindPointsOfIntersection(double xmin, double xmax,
            double a1, double b1, double c1, double d1, double e1, double f1,
            double a2, double b2, double c2, double d2, double e2, double f2)
        {
            var Roots = new List<Point2D>();
            var RootSign1 = new List<int>();
            var RootSign2 = new List<int>();

            // Find roots for each of the difference equations.
            int[] signs = { +1, -1 };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    var points = FindRootsUsingBinaryDivision(
                        xmin, xmax,
                        a1, b1, c1, d1, e1, f1, sign1,
                        a2, b2, c2, d2, e2, f2, sign2);
                    if (points.Count > 0)
                    {
                        Roots.AddRange(points);
                        for (var i = 0; i < points.Count; i++)
                        {
                            RootSign1.Add(sign1);
                            RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            var pointsOfIntersection = new List<Point2D>();
            for (var i = 0; i < Roots.Count; i++)
            {
                var y1 = InterpolateConicY(Roots[i].X, a1, b1, c1, d1, e1, f1, RootSign1[i]);
                //var y2 = InterpolateConicY(Roots[i].X, a2, b2, c2, d2, e2, f2, RootSign2[i]);
                // Validation.
                //Debug.Assert(Math.Abs(y1 - y2) < small);
                pointsOfIntersection.Add(new Point2D(Roots[i].X, y1));
            }

            return pointsOfIntersection;
        }

        /// <summary>
        /// Finds the roots using binary division.
        /// </summary>
        /// <param name="xmin">The xmin.</param>
        /// <param name="xmax">The xmax.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="f1">The f1.</param>
        /// <param name="sign1">The sign1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="f2">The f2.</param>
        /// <param name="sign2">The sign2.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> FindRootsUsingBinaryDivision(double xmin, double xmax,
            double a1, double b1, double c1, double d1, double e1, double f1, int sign1,
            double a2, double b2, double c2, double d2, double e2, double f2, int sign2)
        {
            var roots = new List<Point2D>();
            const int num_tests = 100;
            var delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            var x0 = xmin;
            for (var i = 0; i < num_tests; i++)
            {
                // Try to find a root in this range.
                (var x, var y) = UseBinaryDivision(x0, delta_x,
                    a1, b1, c1, d1, e1, f1, sign1,
                    a2, b2, c2, d2, e2, f2, sign2);

                // See if we have already found this root.
                if (Operations.IsValid(y))
                {
                    var is_new = true;
                    foreach (var pt in roots)
                    {
                        if (Abs(pt.X - x) < double.Epsilon)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1)
                        {
                            return roots;
                        }
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find a root by using binary division.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="deltaX">The delta x.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="f1">The f1.</param>
        /// <param name="sign1">The sign1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="f2">The f2.</param>
        /// <param name="sign2">The sign2.</param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) UseBinaryDivision(double x0, double deltaX,
            double a1, double b1, double c1, double d1, double e1, double f1, int sign1,
            double a2, double b2, double c2, double d2, double e2, double f2, int sign2)
        {
            const int num_trials = 200;
            const int sgn_nan = -2;

            // Get G(x) for the bounds.
            var xmin = x0;
            var g_xmin = InterpolateConicDifferenceY(xmin,
                a1, b1, c1, d1, e1, f1, sign1,
                a2, b2, c2, d2, e2, f2, sign2);
            if (Abs(g_xmin) < double.Epsilon)
            {
                return (xmin, g_xmin);
            }

            var xmax = xmin + deltaX;
            var g_xmax = InterpolateConicDifferenceY(xmax,
                a1, b1, c1, d1, e1, f1, sign1,
                a2, b2, c2, d2, e2, f2, sign2);
            if (Abs(g_xmax) < double.Epsilon)
            {
                return (xmax, g_xmax);
            }

            // Get the sign of the values.
            int sgn_min, sgn_max;
            if (Operations.IsValid(g_xmin))
            {
                sgn_min = Sign(g_xmin);
            }
            else
            {
                sgn_min = sgn_nan;
            }

            if (Operations.IsValid(g_xmax))
            {
                sgn_max = Sign(g_xmax);
            }
            else
            {
                sgn_max = sgn_nan;
            }

            // If the two values have the same sign,
            // then there is no root here.
            if (sgn_min == sgn_max)
            {
                return (1, float.NaN);
            }

            // Use binary division to find the point of intersection.
            double xmid = 0d, g_xmid = 0d;
            for (var i = 0; i < num_trials; i++)
            {
                // Get values for the midpoint.
                xmid = (xmin + xmax) / 2d;
                g_xmid = InterpolateConicDifferenceY(xmid,
                    a1, b1, c1, d1, e1, f1, sign1,
                    a2, b2, c2, d2, e2, f2, sign2);
                int sgn_mid;
                if (Operations.IsValid(g_xmid))
                {
                    sgn_mid = Sign(g_xmid);
                }
                else
                {
                    sgn_mid = sgn_nan;
                }

                // If sgn_mid is 0, gxmid is 0 so this is the root.
                if (sgn_mid == 0)
                {
                    break;
                }

                // See which half contains the root.
                if (sgn_mid == sgn_min)
                {
                    // The min and mid values have the same sign.
                    // Search the right half.
                    xmin = xmid;
                    g_xmin = g_xmid;
                }
                else if (sgn_mid == sgn_max)
                {
                    // The max and mid values have the same sign.
                    // Search the left half.
                    xmax = xmid;
                    g_xmax = g_xmid;
                }
                else
                {
                    // The three values have different signs.
                    // Assume min or max is NaN.
                    if (sgn_min == sgn_nan)
                    {
                        // Value g_xmin is NaN. Use the right half.
                        xmin = xmid;
                        g_xmin = g_xmid;
                    }
                    else if (sgn_max == sgn_nan)
                    {
                        // Value g_xmax is NaN. Use the right half.
                        xmax = xmid;
                        g_xmax = g_xmid;
                    }
                    else
                    {
                        // This is a weird case. Just trap it.
                        //throw new InvalidOperationException(
                        //    "Unexpected difference curve. " +
                        //    "Cannot find a root between X = " +
                        //    xmin + " and X = " + xmax);
                    }
                }
            }

            if (Operations.IsValid(g_xmid) && (Abs(g_xmid) < double.Epsilon))
            {
                return (xmid, g_xmid);
            }
            else if (Operations.IsValid(g_xmin) && (Abs(g_xmin) < double.Epsilon))
            {
                return (xmin, g_xmin);
            }
            else if (Operations.IsValid(g_xmax) && (Abs(g_xmax) < double.Epsilon))
            {
                return (xmax, g_xmax);
            }
            else
            {
                return (xmid, float.NaN);
            }
        }
    }
}
