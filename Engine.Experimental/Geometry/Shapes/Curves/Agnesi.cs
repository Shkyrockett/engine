// <copyright file="Agnesi.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
//  Based on information found at: <see cref="http://paulbourke.net/geometry/agnesi/"/>. <br />
//  Agnesi curves were studied in 1748 by Maria Gaetana Agnesi and earlier by Fermat around 1666 and 
//  Grandi in 1703. Agnesi called the curve "versiera". 
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Agnesi Curve.
    /// </summary>
    /// <remarks>
    /// Class based on information found at: <seealso href="http://paulbourke.net/geometry/agnesi/"/>. <br />
    /// Agnesi curves were studied in 1748 by Maria Gaetana Agnesi and earlier by Fermat around 1666 and 
    /// Grandi in 1703. Agnesi called the curve "versiera". 
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Agnesi))]
    public class Agnesi
        : Shape
    {
        #region Fields
        /// <summary>
        /// The offset.
        /// </summary>
        private Point2D offset;

        /// <summary>
        /// The multiplyer.
        /// </summary>
        private Size2D multiplyer;

        /// <summary>
        /// The precision.
        /// </summary>
        private double precision;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Agnesi"/> class.
        /// </summary>
        public Agnesi()
        {
            offset = new Point2D();
            multiplyer = new Size2D();
            precision = 0.1;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the offset.
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
        /// Gets or sets the multiplyer.
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
        /// Gets or sets the precision.
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
        #endregion Properties

        #region Interpolators
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t) => new Point2D(
            offset.X + (2 * Tan(t) * multiplyer.Width),
            offset.Y + (2 * -Pow(Cos(t), 2) * multiplyer.Height)
            );

        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            var points = new List<Point2D>();
            for (var Index = PI * -1; Index < PI; Index += (1d / count))
            {
                points.Add(Interpolate(Index));
            }

            return points;
        }
        #endregion Interpolators

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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(Agnesi);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Agnesi)}{{{nameof(Offset)}={offset}{sep}{nameof(Multiplyer)}={multiplyer}{sep}{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
