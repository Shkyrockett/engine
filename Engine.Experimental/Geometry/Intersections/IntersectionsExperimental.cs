using static System.Math;

namespace Engine;

/// <summary>
/// The intersections experimental class.
/// </summary>
public static partial class IntersectionsExperimental
{
    /// <summary>
    /// public domain function by Darel Rex Finley, 2006
    /// http://alienryderflex.com/polyspline/
    /// </summary>
    /// <param name="poly"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <returns></returns>
    public static bool PointInPolySpline(double[] poly, double X, double Y)
    {
        ArgumentNullException.ThrowIfNull(poly);

        var SPLINE = 2d;
        var NEW_LOOP = 3d;
        var END = -2d;

        double Sx;
        double Sy;
        double Ex;
        double Ey;
        var oddNodes = false;

        //  prevent the need for special tests when F is exactly 0 or 1
        Y += 0.000001d;

        var i = 0;
        var start = 0;
        while (poly[i] != END)
        {
            var j = i + 2;

            if (poly[i] == SPLINE)
            {
                j++;
            }

            if (poly[j] == END || poly[j] == NEW_LOOP)
            {
                j = start;
            }

            if (poly[i] != SPLINE && poly[j] != SPLINE)
            {
                //  STRAIGHT LINE
                if (poly[i + 1] < Y && poly[j + 1] >= Y || poly[j + 1] < Y && poly[i + 1] >= Y)
                {
                    if (poly[i] + ((Y - poly[i + 1]) / (poly[j + 1] - poly[i + 1]) * (poly[j] - poly[i])) < X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
            }

            else if (poly[j] == SPLINE)
            {
                //  SPLINE CURVE
                var a = poly[j + 1];
                var b = poly[j + 2];
                var k = j + 3;

                if (poly[k] == END || poly[k] == NEW_LOOP)
                {
                    k = start;
                }

                if (poly[i] != SPLINE)
                {
                    Sx = poly[i]; Sy = poly[i + 1];
                }
                else
                {
                    //  Interpolate hard corner
                    Sx = (poly[i + 1] + poly[j + 1]) / 2d;
                    Sy = (poly[i + 2] + poly[j + 2]) / 2d;
                }

                if (poly[k] != SPLINE)
                {
                    Ex = poly[k];
                    Ey = poly[k + 1];
                }
                else
                {
                    //  Interpolate hard corner
                    Ex = (poly[j + 1] + poly[k + 1]) / 2d;
                    Ey = (poly[j + 2] + poly[k + 2]) / 2d;
                }

                var bottomPart = 2d * (Sy + Ey - b - b);
                if (bottomPart == 0d)
                {
                    //  Prevent division-by-zero
                    b += 0.0001;
                    bottomPart = -0.0004;
                }

                var sRoot = 2d * (b - Sy);
                sRoot *= sRoot;
                sRoot -= 2d * bottomPart * (Sy - Y);

                if (sRoot >= 0d)
                {
                    sRoot = Sqrt(sRoot);
                    var topPart = 2d * (Sy - b);
                    for (var plusOrMinus = -1d; plusOrMinus < 1.1d; plusOrMinus += 2d)
                    {
                        var F = (topPart + (plusOrMinus * sRoot)) / bottomPart;
                        if (F >= 0d && F <= 1d)
                        {
                            var xPart = Sx + (F * (a - Sx));
                            if (xPart + (F * (a + (F * (Ex - a)) - xPart)) < X)
                            {
                                oddNodes = !oddNodes;
                            }
                        }
                    }
                }
            }

            if (poly[i] == SPLINE)
            {
                i++;
            }

            i += 2;
            if (poly[i] == NEW_LOOP)
            {
                i++; start = i;
            }
        }

        return oddNodes;
    }

    /// <summary>
    ///  public-domain code by Darel Rex Finley, 2010.
    ///  See diagrams at http://alienryderflex.com/point_left_of_ray
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="raySx"></param>
    /// <param name="raySy"></param>
    /// <param name="rayEx"></param>
    /// <param name="rayEy"></param>
    /// <returns></returns>
    public static bool PointIsLeftOfRay(double x, double y, double raySx, double raySy, double rayEx, double rayEy)
        => (y - raySy) * (rayEx - raySx) > (x - raySx) * (rayEy - raySy);
}
