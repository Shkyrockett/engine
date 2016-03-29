// <copyright file="Butterfly.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <date></date>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>
// <remarks>
//  Class based on information found at: <see cref="http://csharphelper.com/blog/2014/11/draw-a-colored-butterfly-curve-in-c/"/>. <br />
// </remarks>

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
    [Serializable()]
    public class Butterfly
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private PointF offset;

        /// <summary>
        /// 
        /// </summary>
        private SizeF multiplyer;

        /// <summary>
        /// 
        /// </summary>
        private double precision;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        public Butterfly()
        {
            precision = 0.1;
            offset = new PointF();
            multiplyer = new SizeF();
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        public PointF Offset
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
        public SizeF Multiplyer
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
        public List<PointF> Handles
        {
            get
            {
                return new List<PointF> { offset, new PointF(multiplyer.Width + offset.X, multiplyer.Height + Offset.Y) };
            }
            set
            {
                if (value != null && value.Count >= 1)
                {
                    offset = value[0];
                    multiplyer = new SizeF(value[1].X - offset.X, value[1].Y - offset.Y);
                    points = InterpolatePoints(precision);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(double index)
        {
            return new PointF(
                 (float)(offset.X + (Math.Cos(index) * ((Math.Exp(Math.Cos(index)) - ((2 * Math.Cos((4 * index))) - Math.Pow(Math.Sin((index / 12)), 5))) * multiplyer.Width))),
                 (float)(offset.Y + ((Math.Sin(index) * (Math.Exp(Math.Cos(index)) - ((2 * Math.Cos((4 * index))) - Math.Pow(Math.Sin((index / 12)), 5)))) * multiplyer.Height))
                 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<PointF> InterpolatePoints(double precision)
        {
            const double n = 10000;
            double u = (0 * (24 * (Math.PI / n)));
            List<PointF> points = new List<PointF>();
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

            PointF NewPoint = new PointF(
                (float)(Math.Cos(U) * ((Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5))) * Multiplyer.Width)),
                (float)((Math.Sin(U) * (Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5)))) * Multiplyer.Height)
                );

            PointF LastPoint = NewPoint;

            for (double Index = 1; (Index <= N); Index = (Index + Precision))
            {
                LastPoint = NewPoint;
                U = (Index * (24 * (Math.PI / N)));

                NewPoint = new PointF(
                    (float)(Math.Cos(U) * ((Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5))) * Multiplyer.Width)),
                    (float)((Math.Sin(U) * (Math.Exp(Math.Cos(U)) - ((2 * Math.Cos((4 * U))) - Math.Pow(Math.Sin((U / 12)), 5)))) * Multiplyer.Height)
                    );

                e.Graphics.DrawLine(DPen, NewPoint, LastPoint);
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
