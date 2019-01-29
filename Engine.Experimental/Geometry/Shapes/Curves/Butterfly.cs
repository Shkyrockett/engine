// <copyright file="Butterfly.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
//  Class based on information found at: <see cref="http://csharphelper.com/blog/2014/11/draw-a-colored-butterfly-curve-in-c/"/>. <br />
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The butterfly class.
    /// </summary>
    /// <remarks>
    /// Class based on information found at: <seealso href="http://csharphelper.com/blog/2014/11/draw-a-colored-butterfly-curve-in-c/"/>. <br />
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Butterfly))]
    public class Butterfly
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
        /// Initializes a new instance of the <see cref="Butterfly"/> class.
        /// </summary>
        public Butterfly()
        {
            precision = 0.1;
            offset = new Point2D();
            multiplyer = new Size2D();
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
             offset.X + (Cos(t) * ((Exp(Cos(t)) - ((2 * Cos(4 * t)) - Pow(Sin(t / 12), 5))) * multiplyer.Width)),
             offset.Y + (Sin(t) * (Exp(Cos(t)) - ((2 * Cos(4 * t)) - Pow(Sin(t / 12), 5))) * multiplyer.Height)
         );

        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public override List<Point2D> InterpolatePoints(int count)
        {
            const double n = 10000;
            var u = 0 * (24 * (PI / n));
            var points = new List<Point2D>() { Interpolate(u) };
            for (double Index = 1; Index <= n; Index += (1d / count))
            {
                u = Index * (24 * (PI / n));
                points.Add(Interpolate(u));
            }
            return points;
        }
        #endregion Interpolators

        #region Methods
        /// <summary>
        /// Creates a string representation of this <see cref="Butterfly"/> struct based on the format string
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
                return nameof(Butterfly);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Butterfly)}{{{nameof(Offset)}={offset}{sep}{nameof(Multiplyer)}={multiplyer}{sep}{nameof(Precision)}={precision}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
