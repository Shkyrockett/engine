// <copyright file="DestructiveFilter.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
            var result = new PolycurveContour(Process(line.Points[0]));

            var side = new List<Point2D>();
            for (double j = 0; j < 1; j += (1d / (line.Length * SampleDistance)))
            {
                side.Add(Process(Interpolators.Linear(line.A, line.B, j)));
            }

            var curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
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
                var side = new List<Point2D>();
                for (double j = 0; j < 1; j += 1d / (contour[contour.Count - 1].Distance(contour[0]) * SampleDistance))
                {
                    side.Add(Process(Interpolators.Linear(contour[i - 1], contour[i], j)));
                }
                foreach (var curve in new List<CubicBezier>(CurveFit.Fit(side, Tolerence)))
                {
                    result.AddCubicBezier(curve.B, curve.C, curve.D);
                }
            }
            var vertex = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (contour[contour.Count - 1].Distance(contour[0]) * SampleDistance))
            {
                vertex.Add(Process(Interpolators.Linear(contour[contour.Count - 1], contour[0], j)));
            }
            foreach (var curve in new List<CubicBezier>(CurveFit.Fit(vertex, Tolerence)))
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
                var side = new List<Point2D>();
                for (double j = 0; j < 1; j += 1d / (Measurements.Distance(contour[contour.Count - 1], contour[0]) * SampleDistance))
                {
                    side.Add(Process(Interpolators.Linear(contour[i - 1], contour[i], j)));
                }
                var curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
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
        public PolycurveContour Process(PolycurveContour contour)
        {
            var result = new PolycurveContour(Process(contour.Items[0].Start.Value));
            if (contour.Count > 1)
            {
                for (var i = 1; i < contour.Count; i++)
                {
                    var side = new List<Point2D>();
                    for (double j = 0; j < 1; j += 1d / (contour[i].Length * SampleDistance))
                    {
                        side.Add(Process(contour[i].Interpolate(j)));
                    }
                    var curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
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
        public PolycurveContour Process(Rectangle2D rect)
        {
            var result = new PolycurveContour(Process(rect.Location));

            var side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.TopLeft, rect.TopRight) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(rect.Location, rect.TopRight, j)));
            }
            var curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.TopRight, rect.BottomRight) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(rect.TopRight, rect.BottomRight, j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.BottomRight, rect.BottomLeft) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(rect.BottomRight, rect.BottomLeft, j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j += 1d / (Measurements.Distance(rect.BottomLeft, rect.TopLeft) * SampleDistance))
            {
                side.Add(Process(Interpolators.Linear(rect.BottomLeft, rect.Location, j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            return result;
        }
    }
}
