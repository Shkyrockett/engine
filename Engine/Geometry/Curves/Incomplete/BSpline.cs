// <copyright file="BSpline.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks>
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("BSpline")]
    public class BSpline
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute()]
        private List<Point2D> nodePoints;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<Point2D> points = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public BSpline()
        {
            nodePoints = new List<Point2D> { Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BSpline(List<Point2D> points)
        {
            nodePoints = points;
            points = InterpolatePoints(1f / 100f);
        }

        /// <summary>
        /// 
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public List<Point2D> NodePoints
        {
            get
            {
                return nodePoints;
            }
            set
            {
                nodePoints = value;
                Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public List<Point2D> Handles
        {
            get
            {
                return nodePoints;
            }
            set
            {
                nodePoints = value;
                Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            points.Clear();
            points.AddRange(InterpolatePoints(1f / 100f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public  Point2D Interpolate(double index)
        {
            return Interpolate(nodePoints, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints(double count)
        {
            List<Point2D> points = new List<Point2D>();
            for (double i = 0; (i <= 1); i += count)
            {
                points.Add(Interpolate(i));
            }
            points.Add(nodePoints[nodePoints.Count - 1]);
            return points;
        }

        /// <summary>
        /// General Bezier curve Number of control points is n+1 0 less than or equal to mu less than 1    IMPORTANT, the last point is not computed.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point2D Interpolate(List<Point2D> points, double index)
        {
            int n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            double muk = 1;
            double munk = Math.Pow(1 - index, n);

            Point2D b = new Point2D(0.0f, 0.0f);

            for (int k = 0; k <= n; k++)
            {
                nn = n;
                kn = k;
                nkn = n - k;
                blend = muk * munk;
                muk *= index;
                munk /= (1 - index);
                while (nn >= 1)
                {
                    blend *= nn;
                    nn--;
                    if (kn > 1)
                    {
                        blend /= kn;
                        kn--;
                    }
                    if (nkn > 1)
                    {
                        blend /= nkn;
                        nkn--;
                    }
                }

                b = new Point2D(
                b.X + points[k].X * blend,
                b.Y + points[k].Y * blend
                    );
            }

            return (b);
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline 
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Point2D InterpolateCubicBSplinePoint(Point2D[] Points, double Index)
        {
            int A = 0;
            int B = 1;
            int C = 2;
            int D = 3;
            double V1 = Index;
            Point2D[] VPoints = new Point2D[4];

            VPoints[0] = new Point2D(
                ((Points[D].X - Points[C].X) - (Points[A].X - Points[B].X)),
                ((Points[D].Y - Points[C].Y) - (Points[A].Y - Points[B].Y))
                );

            VPoints[1] = new Point2D(
                ((Points[A].X - Points[B].X) - VPoints[A].X),
                ((Points[A].Y - Points[B].Y) - VPoints[A].Y)
                );

            VPoints[2] = new Point2D(
                (Points[C].X - Points[A].X),
                (Points[C].Y - Points[A].Y)
                );

            VPoints[3] = Points[1];

            return new Point2D(
                VPoints[0].X * V1 * V1 * V1 + VPoints[1].X * V1 * V1 * V1 + VPoints[2].X * V1 + VPoints[3].X,
                VPoints[0].Y * V1 * V1 * V1 + VPoints[1].Y * V1 * V1 * V1 + VPoints[2].Y * V1 + VPoints[3].Y
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(BSpline);
            StringBuilder pts = new StringBuilder();
            foreach (Point2D pt in nodePoints)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            pts.Remove(pts.Length - 1, 1);
            return string.Format("{0}{{{1}}}", nameof(BSpline), pts.ToString());
        }
    }
}
