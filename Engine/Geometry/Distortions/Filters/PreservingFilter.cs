// <copyright file="PreservedFilter.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The preserving filter class.
    /// </summary>
    public abstract class PreservingFilter
        : IFilter
    {
        #region Properties
        /// <summary>
        /// Gets or sets the tolerance.
        /// </summary>
        public double Tolerence { get; set; } = 1;

        /// <summary>
        /// Gets or sets the sample distance.
        /// </summary>
        public double SampleDistance { get; set; } = 8;
        #endregion Properties

        /// <summary>
        /// Process a <see cref="Point2D"/> structure with a distortion filter.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public virtual Point2D Process(Point2D point)
            => point;

        /// <summary>
        /// Process a <see cref="List{Point2D}"/> structure with a distortion filter.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public List<Point2D> Process(List<Point2D> contour)
        {
            var results = new List<Point2D>();
            foreach (var point in contour)
            {
                results.Add(Process(point));
            }
            return results;
        }

        /// <summary>
        /// Process a <see cref="S"/> structure with a distortion filter.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns>The <see cref="T"/>.</returns>
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
                case Ray t:
                    return Process(t) as T;
                case LineSegment t:
                    return Process(t) as T;
                case QuadraticBezier t:
                    return Process(t) as T;
                case CubicBezier t:
                    return Process(t) as T;
                case PointSet t:
                    return Process(t) as T;
                case Triangle t:
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
        /// Process a <see cref="Line"/> structure with a distortion filter.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The <see cref="Line"/>.</returns>
        public Line Process(Line line)
        {
            var location = Process(line.Location);
            var result = new Line(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// Process a <see cref="Ray"/> structure with a distortion filter.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The <see cref="Ray"/>.</returns>
        public Ray Process(Ray line)
        {
            var location = Process(line.Location);
            var result = new Ray(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// Process a <see cref="LineSegment"/> structure with a distortion filter.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        public LineSegment Process(LineSegment line)
        {
            var result = new LineSegment(Process(line.A), Process(line.B));
            return result;
        }

        /// <summary>
        /// Process a <see cref="QuadraticBezier"/> structure with a distortion filter.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <returns>The <see cref="QuadraticBezier"/>.</returns>
        public QuadraticBezier Process(QuadraticBezier bezier)
        {
            var result = new QuadraticBezier(Process(bezier.A), Process(bezier.B), Process(bezier.C));
            return result;
        }

        /// <summary>
        /// Process a <see cref="CubicBezier"/> structure with a distortion filter.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <returns>The <see cref="CubicBezier"/>.</returns>
        public CubicBezier Process(CubicBezier bezier)
        {
            var result = new CubicBezier(Process(bezier.A), Process(bezier.B), Process(bezier.C), Process(bezier.D));
            return result;
        }

        /// <summary>
        /// Process a <see cref="PointSet"/> structure with a distortion filter.
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
        /// Process a <see cref="Triangle"/> structure with a distortion filter.
        /// </summary>
        /// <param name="triangle">The triangle.</param>
        /// <returns>The <see cref="Triangle"/>.</returns>
        public Triangle Process(Triangle triangle)
            => new Triangle(Process(triangle.A), Process(triangle.B), Process(triangle.C));

        /// <summary>
        /// Process a <see cref="Rectangle2D"/> structure with a distortion filter.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        public PolygonContour Process(Rectangle2D rect)
            => new PolygonContour { Process(rect.TopLeft), Process(rect.TopRight), Process(rect.BottomRight), Process(rect.BottomLeft) };

        /// <summary>
        /// Process a <see cref="Polygon"/> structure with a distortion filter.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public Polygon Process(Polygon polygon)
        {
            var result = new Polygon();
            foreach (var contour in polygon)
            {
                result.Add(Process(contour));
            }
            return result;
        }

        /// <summary>
        /// Process a <see cref="PolygonContour"/> structure with a distortion filter.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        public PolygonContour Process(PolygonContour contour)
        {
            var results = new PolygonContour();
            foreach (var point in contour)
            {
                results.Add(Process(point));
            }
            return results;
        }

        /// <summary>
        /// Process a <see cref="PolylineSet"/> structure with a distortion filter.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        /// <returns>The <see cref="PolylineSet"/>.</returns>
        public PolylineSet Process(PolylineSet polylines)
        {
            var result = new PolylineSet();
            foreach (var contour in polylines)
            {
                result.Add(Process(contour));
            }
            return result;
        }

        /// <summary>
        /// Process a <see cref="Polyline"/> structure with a distortion filter.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="Polyline"/>.</returns>
        public Polyline Process(Polyline contour)
        {
            var results = new Polyline();
            foreach (var point in contour)
            {
                results.Add(Process(point));
            }
            return results;
        }

        /// <summary>
        /// Process a <see cref="PolycurveContour"/> structure with a distortion filter.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Process(PolycurveContour contour)
        {
            var result = new PolycurveContour(Process(contour.Items[0].Start.Value));
            foreach (var item in contour)
            {
                switch (item)
                {
                    case PointSegment t:
                        break;
                    case LineCurveSegment t:
                        result.AddLineSegment(Process(t.End.Value));
                        break;
                    case ArcSegment t:
                        result.AddArc(t.RX, t.RY, t.Angle, t.LargeArc, t.Sweep, Process(t.End.Value));
                        break;
                    case CardinalSegment t:
                        result.AddCardinalCurve(Process(t.CentralPoints));
                        break;
                    case QuadraticBezierSegment t:
                        result.AddQuadraticBezier(Process(t.Handle.Value), Process(t.End.Value));
                        break;
                    case CubicBezierSegment t:
                        result.AddCubicBezier(Process(t.Handle1), Process(t.Handle2.Value), Process(t.End.Value));
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Process a <see cref="Envelope"/> structure with a distortion filter.
        /// </summary>
        /// <param name="envelope">The contour.</param>
        /// <returns>The <see cref="Polyline"/>.</returns>
        public Envelope Process(Envelope envelope)
        {
            var results = new Envelope
            {
                ControlPointTopLeft = new CubicControlPoint(
                    Process(envelope.ControlPointTopLeft.Point),
                    Process(envelope.ControlPointTopLeft.AnchorAGlobal),
                    Process(envelope.ControlPointTopLeft.AnchorBGlobal), true),
                ControlPointTopRight = new CubicControlPoint(
                    Process(envelope.ControlPointTopRight.Point),
                    Process(envelope.ControlPointTopRight.AnchorAGlobal),
                    Process(envelope.ControlPointTopRight.AnchorBGlobal), true),
                ControlPointBottomRight = new CubicControlPoint(
                    Process(envelope.ControlPointBottomRight.Point),
                    Process(envelope.ControlPointBottomRight.AnchorAGlobal),
                    Process(envelope.ControlPointBottomRight.AnchorBGlobal), true),
                ControlPointBottomLeft = new CubicControlPoint(
                    Process(envelope.ControlPointBottomLeft.Point),
                    Process(envelope.ControlPointBottomLeft.AnchorAGlobal),
                    Process(envelope.ControlPointBottomLeft.AnchorBGlobal), true)
            };

            return results;
        }
    }
}
