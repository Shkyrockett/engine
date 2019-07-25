// <copyright file="LineDashStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The line dash style struct.
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct LineDashStyle
        : IFormattable
    {
        /// <summary>
        /// The solid (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1 }).
        /// </summary>
        public static readonly LineDashStyle Solid = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1 });

        /// <summary>
        /// The dot (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1, 1 }).
        /// </summary>
        public static readonly LineDashStyle Dot = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1, 1 });

        /// <summary>
        /// The dash (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1 }).
        /// </summary>
        public static readonly LineDashStyle Dash = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1 });

        /// <summary>
        /// The dash dot (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1 }).
        /// </summary>
        public static readonly LineDashStyle DashDot = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1 });

        /// <summary>
        /// The dash dot dot (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1, 1, 1 }).
        /// </summary>
        public static readonly LineDashStyle DashDotDot = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1, 1, 1 });

        ///// <summary>
        /////
        ///// </summary>
        //[NonSerialized()]
        //private DashStyle dashStyle;

        /// <summary>
        /// The dash pattern.
        /// </summary>
        private readonly float[] dashPattern;

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="dashPattern"></param>
        ///// <param name="dashOffset"></param>
        //public LineDashStyle(float[] dashPattern, float dashOffset = 0)
        //    : this(/*DashStyle.Custom,*/ dashPattern, dashOffset)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineDashStyle"/> class.
        /// </summary>
        /// <param name="dashPattern">The dashPattern.</param>
        /// <param name="dashOffset">The dashOffset.</param>
        internal LineDashStyle(/*DashStyle dashStyle,*/ float[] dashPattern, float dashOffset = 0)
            : this()
        {
            //this.dashStyle = dashStyle;
            this.dashPattern = dashPattern;
            DashOffset = dashOffset;
        }

        ///// <summary>
        /////
        ///// </summary>
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        //internal DashStyle DashStyle { get { return dashStyle; } set { dashStyle = value; } }

        ///// <summary>
        /////
        ///// </summary>
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        //public float[] DashPattern
        //{
        //    get { return dashPattern; }
        //    set
        //    {
        //        dashPattern = value;
        //        if (dashPattern is null) dashStyle = DashStyle.Solid;
        //        else if (dashPattern == Solid.dashPattern) dashStyle = DashStyle.Solid;
        //        else if (dashPattern == Dot.dashPattern) dashStyle = DashStyle.Dot;
        //        else if (dashPattern == Dash.dashPattern) dashStyle = DashStyle.Dash;
        //        else if (dashPattern == DashDot.dashPattern) dashStyle = DashStyle.DashDot;
        //        else if (dashPattern == DashDotDot.dashPattern) dashStyle = DashStyle.DashDotDot;
        //        else dashStyle = DashStyle.Custom;
        //    }
        //}

        /// <summary>
        /// Gets or sets the dash pattern text.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DashPatternText { get { return ToString(); } set { Parse(value); } }

        /// <summary>
        /// Gets or sets the dash offset.
        /// </summary>
        public float DashOffset { get; set; }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="LineDashStyle"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="LineDashStyle"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="LineDashStyle"/> struct based on the format string
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
            => ConvertToString(format /* format string */, provider /* format provider */);

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
            //if (this is null)
            //    return nameof(GraphicsObject);
            var output = new StringBuilder();
            foreach (var item in dashPattern)
            {
                output.Append($"{item.ToString(format, provider)},");
            }

            output.Replace(",-", "-");
            return output.ToString().Trim(',');
        }

        /// <summary>
        /// Parse.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        private static float[] Parse(string text)
        {
            const string argSeparators = @"[\s,]|(?=-)";
            return Regex.Split(text, argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => float.Parse(arg)).ToArray();
        }
    }
}
