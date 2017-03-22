// <copyright file="PreservedFilter.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
        /// <param name="shape"></param>
        /// <returns></returns>
        public Shape Process(Shape shape)
        {
            switch (shape)
            {
                case ScreenPoint t:
                    return new ScreenPoint(Process(t.Point));
                case PointSet t:
                    return Process(t);
                case LineSegment t:
                    return Process(t);
                case Polygon t:
                    return Process(t);
                case Contour t:
                    return Process(t);
                case PolylineSet t:
                    return Process(t);
                case Polyline t:
                    return Process(t);
                case PolycurveContour t:
                    return Process(t);
                case Rectangle2D t:
                    return Process(t);
                default:
                    break;
            }
            return shape;
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
        public Contour Process(Contour contour)
        {
            var results = new Contour();
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
                    case PathPoint t:
                        break;
                    case PathLineSegment t:
                        result.AddLineSegment(Process(t.End.Value));
                        break;
                    case PathArc t:
                        result.AddArc(t.RX, t.RY, t.Angle, t.LargeArc, t.Sweep, Process(t.End.Value));
                        break;
                    case PathCardinal t:
                        result.AddCardinalCurve(Process(t.CentralPoints));
                        break;
                    case PathQuadraticBezier t:
                        result.AddQuadraticBezier(Process(t.Handle.Value), Process(t.End.Value));
                        break;
                    case PathCubicBezier t:
                        result.AddCubicBezier(Process(t.Handle1), Process(t.Handle2.Value), Process(t.End.Value));
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public Contour Process(Rectangle2D rect)
        {
            return new Contour() { Process(rect.TopLeft), Process(rect.TopRight), Process(rect.BottomRight), Process(rect.BottomLeft) };
        }
    }
}
