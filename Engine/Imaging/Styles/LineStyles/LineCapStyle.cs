// <copyright file="LineCapStyle.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct LineCapStyle
        : IFormattable
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle Flat = new LineCapStyle(LineCap.Flat, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle Square = new LineCapStyle(LineCap.Square, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle Round = new LineCapStyle(LineCap.Round, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle Triangle = new LineCapStyle(LineCap.Triangle, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle NoAnchor = new LineCapStyle(LineCap.NoAnchor, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle SquareAnchor = new LineCapStyle(LineCap.SquareAnchor, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle RoundAnchor = new LineCapStyle(LineCap.RoundAnchor, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle DiamondAnchor = new LineCapStyle(LineCap.DiamondAnchor, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle ArrowAnchor = new LineCapStyle(LineCap.ArrowAnchor, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineCapStyle AnchorMask = new LineCapStyle(LineCap.AnchorMask, new GeometryPath());

        /// <summary>
        /// 
        /// </summary>
        private LineCap lineCap;

        /// <summary>
        /// 
        /// </summary>
        private GeometryPath capPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineCap"></param>
        /// <param name="path"></param>
        public LineCapStyle(LineCap lineCap, GeometryPath path)
            : this()
        {
            this.lineCap = lineCap;
            this.capPath = path;
        }

        /// <summary>
        /// 
        /// </summary>
        internal LineCap LineCap { get { return lineCap; } set { lineCap = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public GeometryPath CapPath { get { return capPath; } set { capPath = value; } }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CapPathText { get { return CapPath?.Deffinition; } set { CapPath.Deffinition = value; } }

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
        string IFormattable.ToString(string format, IFormatProvider provider)
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
        {
            //if (this == null)
            //    return nameof(GraphicsObject);
            return capPath.Deffinition;
        }
    }
}
