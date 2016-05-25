// <copyright file="Butterfly.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks>
//  Class based on information found at: <see cref="http://csharphelper.com/blog/2014/11/draw-a-colored-butterfly-curve-in-c/"/>. <br />
// </remarks>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Class based on information found at: <seealso href="http://csharphelper.com/blog/2014/11/draw-a-colored-butterfly-curve-in-c/"/>. <br />
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Butterfly))]
    public class Butterfly
        : Shape, IClosedShape
    {
        /// <summary>
        /// 
        /// </summary>
        private Point2D offset;

        /// <summary>
        /// 
        /// </summary>
        private Size2D multiplyer;

        /// <summary>
        /// 
        /// </summary>
        private double precision;

        /// <summary>
        /// 
        /// </summary>
        public Butterfly()
        {
            precision = 0.1;
            offset = new Point2D();
            multiplyer = new Size2D();
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Multiplyer
        {
            get { return multiplyer; }
            set { multiplyer = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Handles
        {
            get { return new List<Point2D>() { offset, new Point2D(multiplyer.Width + offset.X, multiplyer.Height + Offset.Y) }; }
            set
            {
                if (value != null && value.Count >= 1)
                {
                    offset = value[0];
                    multiplyer = new Size2D(value[1].X - offset.X, value[1].Y - offset.Y);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double index)
        {
            return new Point2D(
                 offset.X + (Cos(index) * ((Exp(Cos(index)) - ((2 * Cos((4 * index))) - Pow(Sin((index / 12)), 5))) * multiplyer.Width)),
                 offset.Y + ((Sin(index) * (Exp(Cos(index)) - ((2 * Cos((4 * index))) - Pow(Sin((index / 12)), 5)))) * multiplyer.Height)
                 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public override List<Point2D> InterpolatePoints(int precision)
        {
            const double n = 10000;
            double u = (0 * (24 * (PI / n)));
            List<Point2D> points = new List<Point2D>();
            for (double Index = 1; (Index <= n); Index = (Index + (1d / precision)))
            {
                u = (Index * (24 * (PI / n)));
                points.Add(Interpolate(u));
            }
            return points;
        }

        /// <summary>
        /// Butterfly Curve
        /// </summary>
        /// <param name="e"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        public void DrawButterflyCurve2D(PaintEventArgs e, Pen DPen, double Precision, SizeF Offset, SizeF Multiplyer)
        {
            const double N = 10000;
            double U = (0 * (24 * (PI / N)));

            Point2D NewPoint = new Point2D(
                Cos(U) * ((Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5))) * Multiplyer.Width),
                (Sin(U) * (Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5)))) * Multiplyer.Height
                );

            Point2D LastPoint = NewPoint;

            for (double Index = 1; (Index <= N); Index = (Index + Precision))
            {
                LastPoint = NewPoint;
                U = (Index * (24 * (PI / N)));

                NewPoint = new Point2D(
                    Cos(U) * ((Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5))) * Multiplyer.Width),
                    (Sin(U) * (Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5)))) * Multiplyer.Height
                    );

                e.Graphics.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Butterfly);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4},{5}={6}}}", nameof(Butterfly), nameof(Offset), offset, nameof(Multiplyer), multiplyer, nameof(Precision), precision);
        }
    }
}
