// <copyright file="Bicorn.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Bicorn Curve
    /// </summary>
    /// <remarks>
    /// <para>Class based on information found at: <seealso href="http://paulbourke.net/geometry/bicorn/"/>. <br />
    /// Bicorn Curve, also known as the "cocked hat", it was first documented by Sylvester around 
    /// 1864 and Cayley in 1867.</para> 
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Bicorn))]
    public class Bicorn
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
        /// Initializes a new instance of the <see cref="Bicorn"/> class.
        /// </summary>
        public Bicorn()
            : this(new Point2D(), new Size2D())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bicorn"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplyter">The multiplyter.</param>
        public Bicorn(Point2D offset, Size2D multiplyter)
        {
            this.offset = offset;
            multiplyer = multiplyter;
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

        #region Interpolations
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t) => new Point2D(
            offset.X + (2 * Sin(t) * multiplyer.Width),
            offset.Y + (Cos(t) * (2 * ((2 + Cos(t)) / (3 + (Sin(t) * 2)))) * -1 * multiplyer.Height)
        );

        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            var points = new List<Point2D>();
            for (var Index = PI * -1; Index <= PI; Index += 1d / count)
            {
                points.Add(Interpolate(Index));
            }

            return points;
        }
        #endregion Interpolations

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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(Bicorn);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Bicorn)}{{{nameof(Offset)}={offset}{sep}{nameof(Multiplyer)}={multiplyer}{sep}{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
