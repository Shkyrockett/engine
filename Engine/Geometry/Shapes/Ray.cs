// <copyright file="Ray.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The ray class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Ray))]
    [DebuggerDisplay("{ToString()}")]
    public class Ray
        : Shape
    {
        #region Fields
        /// <summary>
        /// The location.
        /// </summary>
        private Point2D location;

        /// <summary>
        /// The direction.
        /// </summary>
        private Vector2D direction;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> class.
        /// </summary>
        public Ray()
            : this(Point2D.Empty, Vector2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="direction">The direction.</param>
        public Ray(Point2D location, Vector2D direction)
        {
            this.location = location;
            this.direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public Ray(double x, double y, double i, double j)
            : this(new Point2D(x, y), new Vector2D(i, j))
        { }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public void Deconstruct(out double x, out double y, out double i, out double j)
        {
            x = Location.X;
            y = Location.Y;
            i = Direction.I;
            j = Direction.J;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D Location
        {
            get { return location; }
            set
            {
                location = value;
                ClearCache();
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Vector2D Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                ClearCache();
                OnPropertyChanged(nameof(Direction));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Rectangle2D Bounds
            => new Rectangle2D(location, location + direction);
        #endregion Properties

        #region Methods
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => Interpolators.Linear(t, location, location + direction);

        /// <summary>
        /// The to line.
        /// </summary>
        /// <returns>The <see cref="Line"/>.</returns>
        public Line ToLine()
            => new Line(location, direction);

        /// <summary>
        /// Creates a string representation of this <see cref="Ray"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return $"{nameof(Ray)}";
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Ray)}={{{nameof(Location)}={Location.ToString(format, provider)}{sep}{nameof(Direction)}={Direction.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}
