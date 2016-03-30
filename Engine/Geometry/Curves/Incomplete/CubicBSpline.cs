// <copyright file="CubicBSpline.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Cubic B Spline")]
    public class CubicBSpline
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute()]
        private List<PointF> nodePoints;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        public CubicBSpline()
        {
            nodePoints = new List<PointF> { PointF.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public CubicBSpline(List<PointF> points)
        {
            nodePoints = points;
            points = InterpolatePoints(1f / 100f);
        }

        /// <summary>
        /// 
        /// </summary>
        public List<PointF> NodePoints
        {
            get
            {
                return nodePoints;
            }
            set
            {
                nodePoints = value;
               points = InterpolatePoints(1f / 100f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(double index)
        {
            return Interpolate(nodePoints, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<PointF> InterpolatePoints(double count)
        {
            List<PointF> points = new List<PointF>();
            for (double i = 0; (i < 1); i += count)
            {
                points.Add(Interpolate(i));
            }

            return points;
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(List<PointF> points, double index)
        {
            if (points.Count >= 4)
            {
                int A = 0;
                int B = 1;
                int C = 2;
                int D = 3;
                List<PointF> VPoints = new List<PointF>(4);

                VPoints.Add(new PointF(
                    ((points[D].X - points[C].X) - (points[A].X - points[B].X)),
                    ((points[D].Y - points[C].Y) - (points[A].Y - points[B].Y))
                    ));

                VPoints.Add(new PointF(
                    ((points[A].X - points[B].X) - VPoints[A].X),
                    ((points[A].Y - points[B].Y) - VPoints[A].Y)
                    ));

                VPoints.Add(new PointF(
                    (points[C].X - points[A].X),
                    (points[C].Y - points[A].Y)
                    ));

                VPoints.Add(points[1]);

                return new PointF(
                    (float)(VPoints[0].X * index * index * index + VPoints[1].X * index * index * index + VPoints[2].X * index + VPoints[3].X),
                    (float)(VPoints[0].Y * index * index * index + VPoints[1].Y * index * index * index + VPoints[2].Y * index + VPoints[3].Y)
                );
            }

            return PointF.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public  List<PointF> Handles
        {
            get
            {
                return nodePoints;
            }
            set
            {
                nodePoints = value;
                points = InterpolatePoints(1f / 100f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "CubicBSpline";
        }
    }
}
