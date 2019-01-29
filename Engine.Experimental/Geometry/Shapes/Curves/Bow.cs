// <copyright file="Bow.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Bow Curve (2D)
    /// </summary>
    /// <remarks>
    ///  Class based on information found at: <seealso href="http://paulbourke.net/geometry/bow2d/"/>. <br />
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Bow))]
    public class Bow
        : Shape, IFormattable
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
        /// Initializes a new instance of the <see cref="Bow"/> class.
        /// </summary>
        public Bow()
            : this(new Point2D(), new Size2D())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bow"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplyer">The multiplyer.</param>
        public Bow(Point2D offset, Size2D multiplyer)
        {
            this.offset = offset;
            this.multiplyer = multiplyer;
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
                OnPropertyChanged(nameof(Multiplyer));
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
            offset.X + ((1 - (Tan(t) * 2)) * Cos(t) * multiplyer.Width),
            offset.Y + ((1 - (Tan(t) * 2)) * (2 * Sin(t)) * multiplyer.Height)
        );

        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            var points = new List<Point2D>();
            for (var Index = PI * -1; Index <= PI; Index += 1d / count)
            {
                points.Add(Interpolate(Index));
            }

            return points;
        }
        #endregion Interpolators

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
            if (this is null)
            {
                return nameof(Bow);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Bow)}{{{nameof(Offset)}={offset}{sep}{nameof(Multiplyer)}={multiplyer}{sep}{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
