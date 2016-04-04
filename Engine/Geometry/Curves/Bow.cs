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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
    [DisplayName("Bow Curve")]
    public class Bow
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
        public Bow()
            : this(new PointF(), new SizeF())
        { }

        /// <summary>
        /// 
        /// </summary>
        public Bow(PointF offset, SizeF multiplyer)
        {
            this.offset = offset;
            this.multiplyer = multiplyer;
            precision = 0.1;
            points = InterpolatePoints(precision);
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
                points = InterpolatePoints(precision);
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
                (float)(offset.X + ((1 - (Math.Tan(index) * 2)) * Math.Cos(index)) * multiplyer.Width),
                (float)(offset.Y + ((1 - (Math.Tan(index) * 2)) * (2 * Math.Sin(index))) * multiplyer.Height)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<PointF> InterpolatePoints(double precision)
        {
            List<PointF> points = new List<PointF>();
            for (double Index = (Math.PI * -1); (Index <= Math.PI); Index += precision)
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
        public void DrawBowCurve2D(PaintEventArgs e, Pen DPen, double Precision, SizeF Offset, SizeF Multiplyer)
        {
            PointF NewPoint = new PointF(
                (float)(((1 - (Math.Tan((Math.PI * -1)) * 2)) * Math.Cos((Math.PI * -1))) * Multiplyer.Width),
                (float)(((1 - (Math.Tan((Math.PI * -1)) * 2)) * (2 * Math.Sin((Math.PI * -1)))) * Multiplyer.Height)
                );

            PointF LastPoint = NewPoint;

            for (double Index = (Math.PI * -1); (Index <= Math.PI); Index += Precision)
            {
                LastPoint = NewPoint;
                NewPoint = new PointF(
                    (float)(((1 - (Math.Tan(Index) * 2)) * Math.Cos(Index)) * Multiplyer.Width),
                    (float)(((1 - (Math.Tan(Index) * 2)) * (2 * Math.Sin(Index))) * Multiplyer.Height)
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
            if (this == null) return "Bow";
            return string.Format("{0}{{O={1},M={2},P={2}}}", "Bow", offset.ToString(), multiplyer.ToString(), precision.ToString());
        }
    }
}
