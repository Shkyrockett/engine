﻿// <copyright file="Arc.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// Bicorn Curve
    /// </summary>
    /// <remarks>
    /// Class based on information found at: <seealso href="http://paulbourke.net/geometry/bicorn/"/>. <br />
    /// Bicorn Curve, also known as the "cocked hat", it was first documented by Sylvester around 
    /// 1864 and Cayley in 1867. 
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Bicorn))]
    public class Bicorn
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
        public Bicorn()
            : this(new Point2D(), new Size2D())
        { }

        /// <summary>
        /// 
        /// </summary>
        public Bicorn(Point2D offset, Size2D multiplyter)
        {
            this.offset = offset;
            multiplyer = multiplyter;
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
        [Category("Functional")]
        [Description("The array of grab handles for this shape.")]
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
                    offset.X + ((2 * Sin(index)) * multiplyer.Width),
                    offset.Y + (((Cos(index) * (2 * ((2 + Cos(index)) / (3 + (Sin(index) * 2))))) * -1) * multiplyer.Height)
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
            for (double Index = (PI * -1); (Index <= PI); Index = (Index + (1d / precision)))
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
            if (this == null) return nameof(Bicorn);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4},{5}={6}}}", nameof(Bicorn), nameof(Offset), offset, nameof(Multiplyer), multiplyer, nameof(Precision), precision);
        }
    }
}
