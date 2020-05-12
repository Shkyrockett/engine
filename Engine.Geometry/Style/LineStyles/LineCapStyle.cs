// <copyright file="LineCapStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The line cap style struct.
    /// </summary>
    /// <seealso cref="IFormattable" />
    /// <seealso cref="IEquatable{T}" />
    [DataContract, Serializable]
    public struct LineCapStyle
        : IFormattable, IEquatable<LineCapStyle>
    {
        /// <summary>
        /// The flat (readonly). Value: new LineCapStyle(/*LineCap.Flat,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle Flat = new LineCapStyle(/*LineCap.Flat,*/ new PolycurveContour2D());

        /// <summary>
        /// The square (readonly). Value: new LineCapStyle(/*LineCap.Square,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle Square = new LineCapStyle(/*LineCap.Square,*/ new PolycurveContour2D());

        /// <summary>
        /// The round (readonly). Value: new LineCapStyle(/*LineCap.Round,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle Round = new LineCapStyle(/*LineCap.Round,*/ new PolycurveContour2D());

        /// <summary>
        /// The triangle (readonly). Value: new LineCapStyle(/*LineCap.Triangle2D,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle Triangle = new LineCapStyle(/*LineCap.Triangle2D,*/ new PolycurveContour2D());

        /// <summary>
        /// The no anchor (readonly). Value: new LineCapStyle(/*LineCap.NoAnchor,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle NoAnchor = new LineCapStyle(/*LineCap.NoAnchor,*/ new PolycurveContour2D());

        /// <summary>
        /// The square anchor (readonly). Value: new LineCapStyle(/*LineCap.SquareAnchor,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle SquareAnchor = new LineCapStyle(/*LineCap.SquareAnchor,*/ new PolycurveContour2D());

        /// <summary>
        /// The round anchor (readonly). Value: new LineCapStyle(/*LineCap.RoundAnchor,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle RoundAnchor = new LineCapStyle(/*LineCap.RoundAnchor,*/ new PolycurveContour2D());

        /// <summary>
        /// The diamond anchor (readonly). Value: new LineCapStyle(/*LineCap.DiamondAnchor,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle DiamondAnchor = new LineCapStyle(/*LineCap.DiamondAnchor,*/ new PolycurveContour2D());

        /// <summary>
        /// The arrow anchor (readonly). Value: new LineCapStyle(/*LineCap.ArrowAnchor,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle ArrowAnchor = new LineCapStyle(/*LineCap.ArrowAnchor,*/ new PolycurveContour2D());

        /// <summary>
        /// The anchor mask (readonly). Value: new LineCapStyle(/*LineCap.AnchorMask,*/ new PolycurveContour2D()).
        /// </summary>
        public static readonly LineCapStyle AnchorMask = new LineCapStyle(/*LineCap.AnchorMask,*/ new PolycurveContour2D());

        ///// <summary>
        ///// 
        ///// </summary>
        //private LineCap lineCap;

        /// <summary>
        /// The cap path.
        /// </summary>
        private PolycurveContour2D capPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineCapStyle" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public LineCapStyle(/*LineCap lineCap,*/ PolycurveContour2D path)
            : this()
        {
            //this.lineCap = lineCap;
            capPath = path;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //internal LineCap LineCap { get { return lineCap; } set { lineCap = value; } }

        /// <summary>
        /// Gets or sets the cap path.
        /// </summary>
        /// <value>
        /// The cap path.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolycurveContour2D CapPath { get { return capPath; } set { capPath = value; } }

        /// <summary>
        /// Gets or sets the cap path text.
        /// </summary>
        /// <value>
        /// The cap path text.
        /// </value>
        [Browsable(false)]
        [XmlAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CapPathText { get { return CapPath?.Definition; } set { CapPath.Definition = value; } }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(LineCapStyle left, LineCapStyle right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(LineCapStyle left, LineCapStyle right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is LineCapStyle style && Equals(style);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] LineCapStyle other) => EqualityComparer<PolycurveContour2D>.Default.Equals(capPath, other.capPath);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(capPath);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="LineCapStyle" /> struct.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="LineCapStyle" /> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="LineCapStyle" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
            => ConvertToString(format /* format string */, formatProvider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="LineDashStyle" /> inherited class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this LineStyle object.
        /// </returns>
        public string ConvertToString(string format, IFormatProvider formatProvider)
        {
            _ = format;
            _ = formatProvider;
            //if (this is null)
            //    return nameof(GraphicsObject);
            return capPath.Definition;
        }
    }
}
