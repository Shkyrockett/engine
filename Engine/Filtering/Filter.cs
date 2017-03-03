// <copyright file="Filter.cs" company="Shkyrockett" >
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
    public abstract class Filter
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
                case PathContour t:
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
        public PathContour Process(LineSegment line)
        {
            var result = new PathContour(Process(line.Points[0]));

            List<Point2D> side = null;
            List<CubicBezier> curves = null;
            side = new List<Point2D>();
            for (double j = 0; j < 1; j = j + 1d / (line.Length * SampleDistance))
            {
                side.Add(Process(Interpolaters.Linear(line.A, line.B, j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public PathContour Process(Polygon polygon)
        {
            var result = new PathContour();
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
        public PathContour Process(Contour contour)
        {
            var result = new PathContour(Process(contour.Points[0]));

            List<Point2D> side = null;
            List<CubicBezier> curves = null;
            for (int i = 1; i < contour.Count; i++)
            {
                side = new List<Point2D>();
                for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(contour[contour.Count - 1], contour[0]) * SampleDistance))
                {
                    side.Add(Process(Interpolaters.Linear(contour[i - 1], contour[i], j)));
                }
                curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
                foreach (var curve in curves)
                {
                    result.AddCubicBezier(curve.B, curve.C, curve.D);
                }
            }
            side = new List<Point2D>();
            for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(contour[contour.Count - 1], contour[0]) * SampleDistance))
            {
                side.Add(Process(Interpolaters.Linear(contour[contour.Count - 1], contour[0], j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        /// <returns></returns>
        public PathContour Process(PolylineSet polylines)
        {
            var result = new PathContour();
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
        public PathContour Process(Polyline contour)
        {
            var result = new PathContour(Process(contour.Points[0]));

            List<Point2D> side = null;
            List<CubicBezier> curves = null;
            for (int i = 1; i < contour.Count; i++)
            {
                side = new List<Point2D>();
                for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(contour[contour.Count - 1], contour[0]) * SampleDistance))
                {
                    side.Add(Process(Interpolaters.Linear(contour[i - 1], contour[i], j)));
                }
                curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
                foreach (var curve in curves)
                {
                    result.AddCubicBezier(curve.B, curve.C, curve.D);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
        public PathContour Process(PathContour contour)
        {
            var result = new PathContour(Process(contour.Items[0].Start.Value));

            List<Point2D> side = null;
            List<CubicBezier> curves = null;
            if (contour.Count > 1)
                for (int i = 1; i < contour.Count; i++)
                {
                    side = new List<Point2D>();
                    for (double j = 0; j < 1; j = j + 1d / (contour[i].Length * SampleDistance))
                    {
                        side.Add(Process(contour[i].Interpolate(j)));
                    }
                    curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
                    foreach (var curve in curves)
                    {
                        result.AddCubicBezier(curve.B, curve.C, curve.D);
                    }
                }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public PathContour Process(Rectangle2D rect)
        {
            var result = new PathContour(Process(rect.Location));

            var side = new List<Point2D>();
            for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(rect.TopLeft, rect.TopRight) * SampleDistance))
            {
                side.Add(Process(Interpolaters.Linear(rect.Location, rect.TopRight, j)));
            }
            var curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(rect.TopRight, rect.BottomRight) * SampleDistance))
            {
                side.Add(Process(Interpolaters.Linear(rect.TopRight, rect.BottomRight, j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(rect.BottomRight, rect.BottomLeft) * SampleDistance))
            {
                side.Add(Process(Interpolaters.Linear(rect.BottomRight, rect.BottomLeft, j)));
            }
            curves = new List<CubicBezier>(CurveFit.Fit(side, Tolerence));
            foreach (var curve in curves)
            {
                result.AddCubicBezier(curve.B, curve.C, curve.D);
            }

            side = new List<Point2D>();
            for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(rect.BottomLeft, rect.TopLeft) * SampleDistance))
            {
                side.Add(Process(Interpolaters.Linear(rect.BottomLeft, rect.Location, j)));
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
