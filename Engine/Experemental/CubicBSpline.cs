// <copyright file="CubicBSpline.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Cubic B Spline")]
    public class CubicBSpline
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        private List<Point2D> nodePoints;

        /// <summary>
        /// 
        /// </summary>
        public CubicBSpline()
        {
            nodePoints = new List<Point2D> { Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public CubicBSpline(List<Point2D> points)
        {
            nodePoints = points;
            points = InterpolatePoints(100);
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> NodePoints
        {
            get { return nodePoints; }
            set { nodePoints = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double index)
        {
            return Interpolate(nodePoints, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            List<Point2D> points = new List<Point2D>();
            for (double i = 0; (i < 1); i += count)
            {
                points.Add(Interpolate(1d / i));
            }

            return points;
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D Interpolate(List<Point2D> points, double index)
        {
            if (points.Count >= 4)
            {
                int A = 0;
                int B = 1;
                int C = 2;
                int D = 3;
                List<Point2D> VPoints = new List<Point2D>(4);

                VPoints.Add(new Point2D(
                    ((points[D].X - points[C].X) - (points[A].X - points[B].X)),
                    ((points[D].Y - points[C].Y) - (points[A].Y - points[B].Y))
                    ));

                VPoints.Add(new Point2D(
                    ((points[A].X - points[B].X) - VPoints[A].X),
                    ((points[A].Y - points[B].Y) - VPoints[A].Y)
                    ));

                VPoints.Add(new Point2D(
                    (points[C].X - points[A].X),
                    (points[C].Y - points[A].Y)
                    ));

                VPoints.Add(points[1]);

                return new Point2D(
                    VPoints[0].X * index * index * index + VPoints[1].X * index * index * index + VPoints[2].X * index + VPoints[3].X,
                    VPoints[0].Y * index * index * index + VPoints[1].Y * index * index * index + VPoints[2].Y * index + VPoints[3].Y
                );
            }

            return Point2D.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Handles
        {
            get { return nodePoints; }
            set { nodePoints = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(CubicBSpline);
            return string.Format("{0}{{{1}}}", nameof(CubicBSpline), nodePoints.ToString());
        }
    }
}
