// <copyright file="Agnesi.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks>
//  Based on information found at: <see cref="http://paulbourke.net/geometry/agnesi/"/>. <br />
//  Agnesi curves were studied in 1748 by Maria Gaetana Agnesi and earlier by Fermat around 1666 and 
//  Grandi in 1703. Agnesi called the curve "versiera". 
// </remarks>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// Agnesi Curve.
    /// </summary>
    /// <remarks>
    /// Class based on information found at: <seealso href="http://paulbourke.net/geometry/agnesi/"/>. <br />
    /// Agnesi curves were studied in 1748 by Maria Gaetana Agnesi and earlier by Fermat around 1666 and 
    /// Grandi in 1703. Agnesi called the curve "versiera". 
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Agnesi Curve")]
    public class Agnesi
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
        public Agnesi()
        {
            offset = new PointF();
            multiplyer = new SizeF();
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
            get            {                return offset;            }
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
            get            {                return multiplyer;            }
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
            get            {                return precision;            }
            set
            {
                precision = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Functional")]
        [Description("The array of grab handles for this shape.")]
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
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(double index)
        {
            return new PointF(
                (float)(offset.X + (2 * Math.Tan(index)) * multiplyer.Width),
                (float)(offset.Y + (2 * -Math.Pow(Math.Cos(index), 2)) * multiplyer.Height)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<PointF> InterpolatePoints(double precision)
        {
            points = new List<PointF>();
            for (double Index = (Math.PI * -1); (Index < Math.PI); Index = (Index + precision))
            {
                points.Add(Interpolate(Index));
            }

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Agnesi";
            return string.Format("{0}{{O={1},M={2},P={2}}}", "Agnesi", offset.ToString(), multiplyer.ToString(), precision.ToString());
        }
    }
}
