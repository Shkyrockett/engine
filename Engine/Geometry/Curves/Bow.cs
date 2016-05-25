// <copyright file="Bow.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks>
//  Class based on information found at: <see cref="http://paulbourke.net/geometry/bicorn/"/>. <br />
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
    /// Bow Curve (2D)
    /// </summary>
    /// <remarks>
    ///  Class based on information found at: <seealso href="http://paulbourke.net/geometry/bow2d/"/>. <br />
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Bow))]
    public class Bow
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
        public Bow()
            : this(new Point2D(), new Size2D())
        { }

        /// <summary>
        /// 
        /// </summary>
        public Bow(Point2D offset, Size2D multiplyer)
        {
            this.offset = offset;
            this.multiplyer = multiplyer;
            precision = 0.1;
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
                offset.X + ((1 - (Tan(index) * 2)) * Cos(index)) * multiplyer.Width,
                offset.Y + ((1 - (Tan(index) * 2)) * (2 * Sin(index))) * multiplyer.Height
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public override List<Point2D> InterpolatePoints(int precision)
        {
            List<Point2D> points = new List<Point2D>();
            for (double Index = (PI * -1); (Index <= PI); Index += (1d / precision))
            {
                points.Add(Interpolate(Index));
            }

            return points;
        }

        /// <summary>
        /// Bow Curve (2D)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        /// <remarks>
        ///  Also known as the "cocked hat", it was first documented by Sylvester around 
        ///  1864 and Cayley in 1867. 
        /// </remarks>
        public void DrawBowCurve2D(PaintEventArgs e, Pen DPen, double Precision, Size2D Offset, Size2D Multiplyer)
        {
            Point2D NewPoint = new Point2D(
                ((1 - (Tan((PI * -1)) * 2)) * Cos((PI * -1))) * Multiplyer.Width,
                ((1 - (Tan((PI * -1)) * 2)) * (2 * Sin((PI * -1)))) * Multiplyer.Height
                );

            Point2D LastPoint = NewPoint;

            for (double Index = (PI * -1); (Index <= PI); Index += Precision)
            {
                LastPoint = NewPoint;
                NewPoint = new Point2D(
                    ((1 - (Tan(Index) * 2)) * Cos(Index)) * Multiplyer.Width,
                    ((1 - (Tan(Index) * 2)) * (2 * Sin(Index))) * Multiplyer.Height
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
            if (this == null) return nameof(Bow);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4},{5}={6}}}", nameof(Bow), nameof(Offset), offset, nameof(Multiplyer), multiplyer, nameof(Precision), precision);
        }
    }
}
