// <copyright file="CubicFilter.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// The destructive cubic Bezier filter class.
    /// </summary>
    public abstract class CubicFilter
        : IFilter
    {
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public virtual Point2D Process(Point2D point) => point;

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public T Process<S, T>(S shape)
            where S : Shape2D
            where T : Shape2D
        {
            switch (shape)
            {
                case ScreenPoint2D t:
                    return new ScreenPoint2D(Process(t.Point)) as T;
                case Line2D t:
                    return Process(t) as T;
                case LineSegment2D t:
                    return Process(t) as T;
                case PointSet2D t:
                    return Process(t) as T;
                case Polygon2D t:
                    return Process(t) as T;
                case PolygonContour2D t:
                    return Process(t) as T;
                case PolylineSet2D t:
                    return Process(t) as T;
                case Polyline2D t:
                    return Process(t) as T;
                case PolycurveContour2D t:
                    return Process(t) as T;
                case Rectangle2D t:
                    return Process(t) as T;
                default:
                    break;
            }

            // Shape not supported.
            return shape as T;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(LineSegment2D line)
        {
            var result = new PolycurveContour2D(Process(line.A));
            var curve = Conversions.LineSegmentToCubicBezier(line.A, line.B);
            result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The <see cref="Line2D"/>.</returns>
        public Line2D Process(Line2D line)
        {
            // ToDo: Figure out how to handle the infiniteness of lines.
            var location = Process(line.Location);
            var result = new Line2D(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="PointSet2D"/>.</returns>
        public PointSet2D Process(PointSet2D points)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            var results = new PointSet2D();
            foreach (var point in points)
            {
                results.Add(Process(point));
            }
            return results;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(Polygon2D polygon)
        {
            if (polygon is null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            var result = new PolycurveContour2D();
            foreach (var contour in polygon)
            {
                result.Add(Process(contour));
            }
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(PolygonContour2D contour)
        {
            var result = new PolycurveContour2D(Process(contour.Points[0]));
            for (var i = 1; i < contour.Count; i++)
            {
                var curve = Conversions.LineSegmentToCubicBezier(contour[i - 1], contour[i]);
                result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            }
            var ccurve = Conversions.LineSegmentToCubicBezier(contour[^1], contour[0]);
            result.AddCubicBezier(Process(ccurve.B), Process(ccurve.C), Process(ccurve.D));
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(PolylineSet2D polylines)
        {
            if (polylines is null)
            {
                throw new ArgumentNullException(nameof(polylines));
            }

            var result = new PolycurveContour2D();
            foreach (var contour in polylines)
            {
                result.Add(Process(contour));
            }
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(Polyline2D contour)
        {
            var result = new PolycurveContour2D(Process(contour.Points[0]));
            for (var i = 1; i < contour.Count; i++)
            {
                var curve = Conversions.LineSegmentToCubicBezier(contour[i - 1], contour[i]);
                result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            }
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(PolycurveContour2D contour)
        {
            var result = new PolycurveContour2D(Process(contour.Items[0].Head));
            foreach (var side in contour)
            {
                switch (side)
                {
                    case PointSegment2D p:
                        if (p != result[0])
                        {
                            result.Add(Process(p.Head));
                        }

                        continue;
                    case LineCurveSegment2D l:
                        {
                            var c = Conversions.LineSegmentToCubicBezier(l.Head, l.Tail);
                            result.AddCubicBezier(Process(c.B), Process(c.C), Process(c.D));
                            continue;
                        }
                    case ArcSegment2D a:
                        {
                            var c = Conversions.ToCubicBeziers(a.ToEllipticalArc());
                            foreach (var e in c)
                            {
                                result.AddCubicBezier(Process(e.B), Process(e.C), Process(e.D));
                            }
                            continue;
                        }
                    case QuadraticBezierSegment2D q:
                        {
                            var c = q.ToQuadtraticBezier().ToCubicBezier();
                            result.AddCubicBezier(Process(c.B), Process(c.C), Process(c.D));
                            continue;
                        }
                    case CubicBezierSegment2D b:
                        {
                            result.AddCubicBezier(Process(b.Handle1), Process(b.Handle2), Process(b.Tail));
                            continue;
                        }
                    case CardinalSegment2D _:
                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Process(Rectangle2D rect)
        {
            var result = new PolycurveContour2D(Process(rect.Location));
            var curve = Conversions.LineSegmentToCubicBezier(rect.Location, rect.TopRight);
            result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            curve = Conversions.LineSegmentToCubicBezier(rect.TopRight, rect.BottomRight);
            result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            curve = Conversions.LineSegmentToCubicBezier(rect.BottomRight, rect.BottomLeft);
            result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            curve = Conversions.LineSegmentToCubicBezier(rect.BottomLeft, rect.Location);
            result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            return result;
        }
    }
}
