// <copyright file="Star.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>http://csharphelper.com/blog/2014/08/draw-a-star-with-a-given-number-of-points-in-c/</summary>
// <remarks></remarks>

using System;
using System.Drawing;
using System.Runtime.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The star class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    public class Star
        : PolygonContour2D
    {
        //// Draw the indicated star in the rectangle.
        ///// <summary>
        ///// The draw star.
        ///// </summary>
        ///// <param name="gr">The gr.</param>
        ///// <param name="the_pen">The the_pen.</param>
        ///// <param name="the_brush">The the_brush.</param>
        ///// <param name="num_points">The num_points.</param>
        ///// <param name="skip">The skip.</param>
        ///// <param name="rect">The rect.</param>
        //private void DrawStar(Graphics gr, Pen the_pen, Brush the_brush, int num_points, int skip, Rectangle rect)
        //{
        //    // Get the star's points.
        //    var star_points = MakeStarPoints(-PI / 2, num_points, skip, rect);

        //    // Draw the star.
        //    gr.FillPolygon(the_brush, star_points);
        //    gr.DrawPolygon(the_pen, star_points);
        //}

        // Generate the points for a star.
        /// <summary>
        /// The make star points.
        /// </summary>
        /// <param name="start_theta">The start_theta.</param>
        /// <param name="num_points">The num_points.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="rect">The rect.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static PointF[] MakeStarPoints(double start_theta, int num_points, int skip, Rectangle rect)
        {
            double theta, dtheta;
            PointF[] result;
            var cx = rect.Width / 2f;
            var cy = rect.Height / 2f;

            // If this is a polygon, don't bother with concave points.
            if (skip == 1)
            {
                result = new PointF[num_points];
                theta = start_theta;
                dtheta = 2 * PI / num_points;
                for (var i = 0; i < num_points; i++)
                {
                    result[i] = new PointF(
                        (float)(cx + (cx * Cos(theta))),
                        (float)(cy + (cy * Sin(theta))));
                    theta += dtheta;
                }
                return result;
            }

            // Find the radius for the concave vertices.
            var concave_radius = CalculateConcaveRadius(num_points, skip);

            // Make the points.
            result = new PointF[2 * num_points];
            theta = start_theta;
            dtheta = PI / num_points;
            for (var i = 0; i < num_points; i++)
            {
                result[2 * i] = new PointF(
                    (float)(cx + (cx * Cos(theta))),
                    (float)(cy + (cy * Sin(theta))));
                theta += dtheta;
                result[(2 * i) + 1] = new PointF(
                    (float)(cx + (cx * Cos(theta) * concave_radius)),
                    (float)(cy + (cy * Sin(theta) * concave_radius)));
                theta += dtheta;
            }
            return result;
        }

        // Calculate the inner star radius.
        /// <summary>
        /// Calculate the concave radius.
        /// </summary>
        /// <param name="num_points">The num_points.</param>
        /// <param name="skip">The skip.</param>
        /// <returns>The <see cref="double"/>.</returns>
        private static double CalculateConcaveRadius(int num_points, int skip)
        {
            // For really small numbers of points.
            if (num_points < 5)
            {
                return 0.33d;
            }

            // Calculate angles to key points.
            var dtheta = 2d * PI / num_points;
            var theta00 = -PI / 2d;
            var theta01 = theta00 + (dtheta * skip);
            var theta10 = theta00 + dtheta;
            var theta11 = theta10 - (dtheta * skip);

            // Find the key points.
            var pt00 = new Point2D((float)Cos(theta00), (float)Sin(theta00));
            var pt01 = new Point2D((float)Cos(theta01), (float)Sin(theta01));
            var pt10 = new Point2D((float)Cos(theta10), (float)Sin(theta10));
            var pt11 = new Point2D((float)Cos(theta11), (float)Sin(theta11));
            // See where the segments connecting the points intersect.
            FindIntersection(pt00, pt01, pt10, pt11, out _, out _, out var intersection, out _, out _);

            // Calculate the distance between the
            // point of intersection and the center.
            return Sqrt(
                (intersection.X * intersection.X)
                + (intersection.Y * intersection.Y));
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        /// <summary>
        /// Find the intersection.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <param name="lines_intersect">The lines_intersect.</param>
        /// <param name="segments_intersect">The segments_intersect.</param>
        /// <param name="intersection">The intersection.</param>
        /// <param name="close_p1">The close_p1.</param>
        /// <param name="close_p2">The close_p2.</param>
        private static void FindIntersection(Point2D p1, Point2D p2, Point2D p3, Point2D p4,
            out bool lines_intersect, out bool segments_intersect,
            out Point2D intersection, out Point2D close_p1, out Point2D close_p2)
        {
            // Get the segments' parameters.
            var dx12 = p2.X - p1.X;
            var dy12 = p2.Y - p1.Y;
            var dx34 = p4.X - p3.X;
            var dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            var denominator = (dy12 * dx34) - (dx12 * dy34);

            double t1;
            try
            {
                t1 = (((p1.X - p3.X) * dy34) + ((p3.Y - p1.Y) * dx34)) / denominator;
            }
            catch (DivideByZeroException)
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new Point2D(float.NaN, float.NaN);
                close_p1 = new Point2D(float.NaN, float.NaN);
                close_p2 = new Point2D(float.NaN, float.NaN);
                return;
            }

            lines_intersect = true;

            var t2 = (((p3.X - p1.X) * dy12) + ((p1.Y - p3.Y) * dx12)) / -denominator;

            // Find the point of intersection.
            intersection = new Point2D(p1.X + (dx12 * t1), p1.Y + (dy12 * t1));

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = (t1 >= 0d) && (t1 <= 1d) && (t2 >= 0d) && (t2 <= 1d);

            // Find the closest points on the segments.
            if (t1 < 0d)
            {
                t1 = 0d;
            }
            else if (t1 > 1d)
            {
                t1 = 1d;
            }

            if (t2 < 0d)
            {
                t2 = 0d;
            }
            else if (t2 > 1d)
            {
                t2 = 1d;
            }

            close_p1 = new Point2D(p1.X + (dx12 * t1), p1.Y + (dy12 * t1));
            close_p2 = new Point2D(p3.X + (dx34 * t2), p3.Y + (dy34 * t2));
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return nameof(Star);
            }

            return nameof(Star);
        }
    }
}
