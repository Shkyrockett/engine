// <copyright file="LineCapStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The line cap style struct.
    /// </summary>
    [DataContract, Serializable]
    public struct LineCapStyle
        : IFormattable
    {
        /// <summary>
        /// The flat (readonly). Value: new LineCapStyle(/*LineCap.Flat,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle Flat = new LineCapStyle(/*LineCap.Flat,*/ new PolycurveContour());

        /// <summary>
        /// The square (readonly). Value: new LineCapStyle(/*LineCap.Square,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle Square = new LineCapStyle(/*LineCap.Square,*/ new PolycurveContour());

        /// <summary>
        /// The round (readonly). Value: new LineCapStyle(/*LineCap.Round,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle Round = new LineCapStyle(/*LineCap.Round,*/ new PolycurveContour());

        /// <summary>
        /// The triangle (readonly). Value: new LineCapStyle(/*LineCap.Triangle,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle Triangle = new LineCapStyle(/*LineCap.Triangle,*/ new PolycurveContour());

        /// <summary>
        /// The no anchor (readonly). Value: new LineCapStyle(/*LineCap.NoAnchor,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle NoAnchor = new LineCapStyle(/*LineCap.NoAnchor,*/ new PolycurveContour());

        /// <summary>
        /// The square anchor (readonly). Value: new LineCapStyle(/*LineCap.SquareAnchor,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle SquareAnchor = new LineCapStyle(/*LineCap.SquareAnchor,*/ new PolycurveContour());

        /// <summary>
        /// The round anchor (readonly). Value: new LineCapStyle(/*LineCap.RoundAnchor,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle RoundAnchor = new LineCapStyle(/*LineCap.RoundAnchor,*/ new PolycurveContour());

        /// <summary>
        /// The diamond anchor (readonly). Value: new LineCapStyle(/*LineCap.DiamondAnchor,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle DiamondAnchor = new LineCapStyle(/*LineCap.DiamondAnchor,*/ new PolycurveContour());

        /// <summary>
        /// The arrow anchor (readonly). Value: new LineCapStyle(/*LineCap.ArrowAnchor,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle ArrowAnchor = new LineCapStyle(/*LineCap.ArrowAnchor,*/ new PolycurveContour());

        /// <summary>
        /// The anchor mask (readonly). Value: new LineCapStyle(/*LineCap.AnchorMask,*/ new PolycurveContour()).
        /// </summary>
        public static readonly LineCapStyle AnchorMask = new LineCapStyle(/*LineCap.AnchorMask,*/ new PolycurveContour());

        ///// <summary>
        ///// 
        ///// </summary>
        //private LineCap lineCap;

        /// <summary>
        /// The cap path.
        /// </summary>
        private PolycurveContour capPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineCapStyle"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public LineCapStyle(/*LineCap lineCap,*/ PolycurveContour path)
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolycurveContour CapPath { get { return capPath; } set { capPath = value; } }

        /// <summary>
        /// Gets or sets the cap path text.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CapPathText { get { return CapPath?.Definition; } set { CapPath.Definition = value; } }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="LineCapStyle"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="LineCapStyle"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="LineCapStyle"/> struct based on the format string
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
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="LineDashStyle"/> inherited class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this LineStyle object.
        /// </returns>
        public string ConvertToString(string format, IFormatProvider provider)
            //if (this is null)
            //    return nameof(GraphicsObject);
            => capPath.Definition;
    }
}
