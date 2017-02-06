// <copyright file="Bow.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
//  Class based on information found at: <see cref="http://paulbourke.net/geometry/bicorn/"/>. <br />
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Math;

namespace Engine
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
                OnPropertyChanged(nameof(Offset));
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
                OnPropertyChanged(nameof(Multiplyer));
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
                OnPropertyChanged(nameof(Precision));
            }
        }

        #endregion

        #region Interpolaters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t) => new Point2D(
    offset.X + ((1 - (Tan(t) * 2)) * Cos(t)) * multiplyer.Width,
    offset.Y + ((1 - (Tan(t) * 2)) * (2 * Sin(t))) * multiplyer.Height
    );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            var points = new List<Point2D>();
            for (double Index = (PI * -1); (Index <= PI); Index += (1d / count))
                points.Add(Interpolate(Index));

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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Bow);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Bow)}{{{nameof(Offset)}={offset},{nameof(Multiplyer)}={multiplyer},{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
