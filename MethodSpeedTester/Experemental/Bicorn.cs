﻿// <copyright file="Bicorn.cs" >
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
using System.Diagnostics.Contracts;
using static System.Math;

namespace Engine
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
        public Bicorn()
            : this(new Point2D(), new Size2D())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Bicorn(Point2D offset, Size2D multiplyter)
        {
            this.offset = offset;
            multiplyer = multiplyter;
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

        #region Interpolations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t) => new Point2D(
        offset.X + ((2 * Sin(t)) * multiplyer.Width),
        offset.Y + (((Cos(t) * (2 * ((2 + Cos(t)) / (3 + (Sin(t) * 2))))) * -1) * multiplyer.Height)
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            var points = new List<Point2D>();
            for (double Index = (PI * -1); (Index <= PI); Index = (Index + (1d / count)))
                points.Add(Interpolate(Index));

            return points;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Bicorn"/> struct based on the format string
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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Bicorn);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Bicorn)}{{{nameof(Offset)}={offset},{nameof(Multiplyer)}={multiplyer},{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
