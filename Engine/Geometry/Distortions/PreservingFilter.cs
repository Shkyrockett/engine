// <copyright file="PreservedFilter.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public abstract class PreservingFilter
        : IFilter
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double Tolerence { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public double SampleDistance { get; set; } = 8;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual Point2D Process(Point2D point)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Line Process(Line line)
        {
            var location = Process(line.Location);
            var result = new Line(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Ray Process(Ray line)
        {
            var location = Process(line.Location);
            var result = new Ray(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public LineSegment Process(LineSegment line)
        {
            var result = new LineSegment(Process(line.A), Process(line.B));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        public QuadraticBezier Process(QuadraticBezier bezier)
        {
            var result = new QuadraticBezier(Process(bezier.A), Process(bezier.B), Process(bezier.C));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        public CubicBezier Process(CubicBezier bezier)
        {
            var result = new CubicBezier(Process(bezier.A), Process(bezier.B), Process(bezier.C), Process(bezier.D));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public Triangle Process(Triangle rect)
            => new Triangle(Process(rect.A), Process(rect.B), Process(rect.C));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public PolygonContour Process(Rectangle2D rect)
            => new PolygonContour() { Process(rect.TopLeft), Process(rect.TopRight), Process(rect.BottomRight), Process(rect.BottomLeft) };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
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
    }
}
