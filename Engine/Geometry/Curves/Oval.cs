// <copyright file="Oval.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Oval))]
    public class Oval
        : Shape, IClosedShape, IFormattable
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Point2D location;

        /// <summary>
        /// 
        /// </summary>
        private Size2D size;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Oval(Point2D location, Size2D size)
        {
            this.location = location;
            this.size = size;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D(location, size); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Oval"/> struct.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Oval"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Oval"/> struct based on the format string
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
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Oval"/> struct based on the format string
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
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Oval);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Oval)}{{{nameof(Location)}={location}{sep}{nameof(Size)}={size}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
