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
using System.Windows.Forms;

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
    [DisplayName("Butterfly")]
    public class Butterfly
        : Shape
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
        /// Interpolated points.
        /// </summary>
        private List<Point2D> points;

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
        [TypeConverter(typeof(PointFConverter))]
        public Point2D Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
                points = InterpolatePoints(0.1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Multiplyer
        {
            get
            {
                return multiplyer;
            }
            set
            {
                multiplyer = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Handles
        {
            get
            {
                return new List<Point2D>() { offset, new Point2D(multiplyer.Width + offset.X, multiplyer.Height + Offset.Y) };
            }
            set
            {
                if (value != null && value.Count >= 1)
                {
                    offset = value[0];
                    multiplyer = new Size2D(value[1].X - offset.X, value[1].Y - offset.Y);
                    points = InterpolatePoints(precision);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D Interpolate(double index)
        {
            return new Point2D(
                 (float)(offset.X + (Math.Cos(index) * ((Math.Exp(Math.Cos(index)) - ((2 * Math.Cos((4 * index))) - Math.Pow(Math.Sin((index / 12)), 5))) * multiplyer.Width))),
                 (float)(offset.Y + ((Math.Sin(index) * (Math.Exp(Math.Cos(index)) - ((2 * Math.Cos((4 * index))) - Math.Pow(Math.Sin((index / 12)), 5)))) * multiplyer.Height))
                 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints(double precision)
        {
            const double n = 10000;
            double u = (0 * (24 * (Math.PI / n)));
            List<Point2D> points = new List<Point2D>();
            for (double Index = 1; (Index <= n); Index = (Index + precision))
            {
                u = (Index * (24 * (Math.PI / n)));
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
            double U = (0 * (24 * (Math.PI / N)));

            Point2D NewPoint = new Point2D(
                (float)(Math.Cos(U) * ((Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5))) * Multiplyer.Width)),
                (float)((Math.Sin(U) * (Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5)))) * Multiplyer.Height)
                );

            Point2D LastPoint = NewPoint;

            for (double Index = 1; (Index <= N); Index = (Index + Precision))
            {
                LastPoint = NewPoint;
                U = (Index * (24 * (Math.PI / N)));

                NewPoint = new Point2D(
                    (float)(Math.Cos(U) * ((Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5))) * Multiplyer.Width)),
                    (float)((Math.Sin(U) * (Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5)))) * Multiplyer.Height)
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
            if (this == null) return "Butterfly";
            return string.Format("{0}{{O={1},M={2},P={2}}}", "Butterfly", offset.ToString(), multiplyer.ToString(), precision.ToString());
        }
    }
}
