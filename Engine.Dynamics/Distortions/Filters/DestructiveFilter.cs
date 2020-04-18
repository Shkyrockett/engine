// <copyright file="DestructiveFilter.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The destructive filter class.
    /// </summary>
    public abstract class DestructiveFilter
        : IFilter
    {
        #region Properties
        /// <summary>
        /// Gets or sets the tolerence.
        /// </summary>
        public double Tolerence { get; set; } = 1;

        /// <summary>
        /// Gets or sets the sample distance.
        /// </summary>
        public double SampleDistance { get; set; } = 8;
        #endregion Properties

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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour2D Process(LineSegment2D line)
        {
            var result = new PolycurveContour2D(Process((line?.Points[0]).Value));

            var side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (line.Length * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(j, line.A, line.B)));
            }

            var curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

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
            var location = Process((line?.Location).Value);
            var result = new Line2D(location, Process(line.Location + line.Direction) - location);
            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="PointSet"/>.</returns>
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour2D Process(PolygonContour2D contour)
        {
            var result = new PolycurveContour2D(Process((contour?.Points[0]).Value));

            for (var i = 1; i < contour.Count; i++)
            {
                var side = new List<Point2D>();
                for (double j = 0; j < 1; j += 1d / (contour[^1].Distance(contour[0]) * SampleDistance))
                {
                    side.Add(Process(Interpolators.Linear(j, contour[i - 1], contour[i])));
                }
                foreach (var curve in new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence)))
                {
                    result.AddCubicBezier(curve.B, curve.C, curve.D);
                }
            }
            var vertex = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (contour[^1].Distance(contour[0]) * SampleDistance))
            {
                vertex.Add(Process(Interpolators.Linear(j, contour[^1], contour[0])));
            }
            foreach (var curve in new List<CubicBezier2D>(CurveFit.Fit(vertex, Tolerence)))
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
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
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour2D Process(Polyline2D contour)
        {
            var result = new PolycurveContour2D(Process((contour?.Points[0]).Value));
            for (var i = 1; i < contour.Count; i++)
            {
                var side = new List<Point2D>();
                for (double j = 0; j < 1; j += 1d / (Measurements.Distance(contour[^1], contour[0]) * SampleDistance))
                {
                    side.Add(Process(Interpolators.Linear(j, contour[i - 1], contour[i])));
                }
                var curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
                foreach (var curve in curves)
                {
                    result.AddCubicBezier(curve.B, curve.C, curve.D);
                }
            }

            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour2D Process(PolycurveContour2D contour)
        {
            var result = new PolycurveContour2D(Process((contour?.Items[0]).Head.Value));
            if (contour.Count > 1)
            {
                for (var i = 1; i < contour.Count; i++)
                {
                    var side = new List<Point2D>();
                    for (double j = 0; j < 1; j += 1d / (contour[i].Length * SampleDistance))
                    {
                        side.Add(Process(contour[i].Interpolate(j)));
                    }
                    var curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
                    foreach (var curve in curves)
                    {
                        result.AddCubicBezier(curve.B, curve.C, curve.D);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour2D Process(Rectangle2D rect)
        {
            var result = new PolycurveContour2D(Process((rect?.Location).Value));

            var side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.TopLeft, rect.TopRight) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(j, rect.Location, rect.TopRight)));
            }
            var curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.TopRight, rect.BottomRight) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(j, rect.TopRight, rect.BottomRight)));
            }
            curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.BottomRight, rect.BottomLeft) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(j, rect.BottomRight, rect.BottomLeft)));
            }
            curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.BottomLeft, rect.TopLeft) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(j, rect.BottomLeft, rect.Location)));
            }
            curves = new List<CubicBezier2D>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            return result;
        }
    }
}
