// <copyright file="CubicFilter.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
        public virtual Point2D Process(Point2D point)
            => point;

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public T Process<S, T>(S shape)
            where S : Shape
            where T : Shape
        {
            switch (shape)
            {
                case ScreenPoint t:
                    return new ScreenPoint(Process(t.Point)) as T;
                case Line t:
                    return Process(t) as T;
                case LineSegment t:
                    return Process(t) as T;
                case PointSet t:
                    return Process(t) as T;
                case Polygon t:
                    return Process(t) as T;
                case PolygonContour t:
                    return Process(t) as T;
                case PolylineSet t:
                    return Process(t) as T;
                case Polyline t:
                    return Process(t) as T;
                case PolycurveContour t:
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(LineSegment line)
        {
            var result = new PolycurveContour(Process(line.A));
            var curve = Conversions.LineSegmentToCubicBezier(line.A, line.B);
            result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The <see cref="Line"/>.</returns>
        public Line Process(Line line)
        {
            // ToDo: Figure out how to handle the infiniteness of lines.
            var location = Process(line.Location);
            var result = new Line(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="PointSet"/>.</returns>
        public PointSet Process(PointSet points)
        {
            var results = new PointSet();
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(Polygon polygon)
        {
            var result = new PolycurveContour();
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(PolygonContour contour)
        {
            var result = new PolycurveContour(Process(contour.Points[0]));
            for (var i = 1; i < contour.Count; i++)
            {
                var curve = Conversions.LineSegmentToCubicBezier(contour[i - 1], contour[i]);
                result.AddCubicBezier(Process(curve.B), Process(curve.C), Process(curve.D));
            }
            var ccurve = Conversions.LineSegmentToCubicBezier(contour[contour.Count - 1], contour[0]);
            result.AddCubicBezier(Process(ccurve.B), Process(ccurve.C), Process(ccurve.D));
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(PolylineSet polylines)
        {
            var result = new PolycurveContour();
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(Polyline contour)
        {
            var result = new PolycurveContour(Process(contour.Points[0]));
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(PolycurveContour contour)
        {
            var result = new PolycurveContour(Process(contour.Items[0].Head.Value));
            foreach (var side in contour)
            {
                switch (side)
                {
                    case PointSegment p:
                        if (p != result[0])
                        {
                            result.Add(Process(p.Head.Value));
                        }

                        continue;
                    case LineCurveSegment l:
                        {
                            var c = Conversions.LineSegmentToCubicBezier(l.Head.Value, l.Tail.Value);
                            result.AddCubicBezier(Process(c.B), Process(c.C), Process(c.D));
                            continue;
                        }
                    case ArcSegment a:
                        {
                            var c = Conversions.ToCubicBeziers(a.ToEllipticalArc());
                            foreach (var e in c)
                            {
                                result.AddCubicBezier(Process(e.B), Process(e.C), Process(e.D));
                            }
                            continue;
                        }
                    case QuadraticBezierSegment q:
                        {
                            var c = q.ToQuadtraticBezier().ToCubicBezier();
                            result.AddCubicBezier(Process(c.B), Process(c.C), Process(c.D));
                            continue;
                        }
                    case CubicBezierSegment b:
                        {
                            result.AddCubicBezier(Process(b.Handle1), Process(b.Handle2.Value), Process(b.Tail.Value));
                            continue;
                        }
                    case CardinalSegment s:
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(Rectangle2D rect)
        {
            var result = new PolycurveContour(Process(rect.Location));
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
