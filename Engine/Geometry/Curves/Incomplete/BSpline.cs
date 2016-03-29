// <copyright file="BSpline.cs" company="Shkyrockett">
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
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DisplayName("BSpline")]
    public class BSpline
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
        public BSpline()
        {
            nodePoints = new List<PointF> { PointF.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BSpline(List<PointF> points)
        {
            nodePoints = points;
            points = InterpolatePoints(1f / 100f);
        }

        /// <summary>
        /// 
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public List<PointF> NodePoints
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
        public List<PointF> Handles
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
        public  PointF Interpolate(double index)
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
        public static PointF Interpolate(List<PointF> points, double index)
        {
            int n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            double muk = 1;
            double munk = Math.Pow(1 - index, (double)n);

            PointF b = new PointF(0.0f, 0.0f);

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
                        blend /= (double)kn;
                        kn--;
                    }
                    if (nkn > 1)
                    {
                        blend /= (double)nkn;
                        nkn--;
                    }
                }

                b = new PointF(
                (float)(b.X + points[k].X * blend),
                (float)(b.Y + points[k].Y * blend)
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
        public PointF InterpolateCubicBSplinePoint(PointF[] Points, double Index)
        {
            int A = 0;
            int B = 1;
            int C = 2;
            int D = 3;
            double V1 = Index;
            PointF[] VPoints = new PointF[4];

            VPoints[0] = new PointF(
                ((Points[D].X - Points[C].X) - (Points[A].X - Points[B].X)),
                ((Points[D].Y - Points[C].Y) - (Points[A].Y - Points[B].Y))
                );

            VPoints[1] = new PointF(
                ((Points[A].X - Points[B].X) - VPoints[A].X),
                ((Points[A].Y - Points[B].Y) - VPoints[A].Y)
                );

            VPoints[2] = new PointF(
                (Points[C].X - Points[A].X),
                (Points[C].Y - Points[A].Y)
                );

            VPoints[3] = Points[1];

            return new PointF(
                (float)(VPoints[0].X * V1 * V1 * V1 + VPoints[1].X * V1 * V1 * V1 + VPoints[2].X * V1 + VPoints[3].X),
                (float)(VPoints[0].Y * V1 * V1 * V1 + VPoints[1].Y * V1 * V1 * V1 + VPoints[2].Y * V1 + VPoints[3].Y)
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "BSpline";
            StringBuilder pts = new StringBuilder();
            foreach (PointF pt in nodePoints)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            pts.Remove(pts.Length - 1, 1);
            return string.Format("{0}{{{1}}}", "BSpline", pts.ToString());
        }
    }
}
