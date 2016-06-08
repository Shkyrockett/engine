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
using System.Diagnostics.Contracts;
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
        : Shape, IClosedShape, IFormattable
    {
        #region Fields

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

        #endregion

        #region Constructors

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

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Multiplyer
        {
            get { return multiplyer; }
            set
            {
                multiplyer = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Precision
        {
            get { return precision; }
            set
            {
                precision = value;
                update?.Invoke();
            }
        }

        #endregion

        #region Interpolaters

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

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Bow"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Bow);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Bow)}{{{nameof(Offset)}={offset},{nameof(Multiplyer)}={multiplyer},{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
