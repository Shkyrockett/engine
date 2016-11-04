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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using static System.Math;

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
    [DisplayName(nameof(Agnesi))]
    public class Agnesi
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
        public Agnesi()
        {
            offset = new Point2D();
            multiplyer = new Size2D();
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
                OnPropertyChanged(nameof(multiplyer));
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
    (offset.X + (2 * Tan(t)) * multiplyer.Width),
    offset.Y + (2 * -Pow(Cos(t), 2)) * multiplyer.Height
    );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            var points = new List<Point2D>();
            for (double Index = (PI * -1); (Index < PI); Index = (Index + (1d / count)))
                points.Add(Interpolate(Index));

            return points;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Agnesi"/> struct based on the format string
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
            if (this == null) return nameof(Agnesi);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Agnesi)}{{{nameof(Offset)}={offset},{nameof(Multiplyer)}={multiplyer},{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
