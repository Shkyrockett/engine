// <copyright file="LineDashStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
//using System.Drawing.Drawing2D;
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
    /// 
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct LineDashStyle
        : IFormattable
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly LineDashStyle Solid = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1 });

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineDashStyle Dot = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1, 1 });

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineDashStyle Dash = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1 });

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineDashStyle DashDot = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1 });

        /// <summary>
        /// 
        /// </summary>
        public static readonly LineDashStyle DashDotDot = new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1, 1, 1 });

        ///// <summary>
        ///// 
        ///// </summary>
        //[NonSerialized()]
        //private DashStyle dashStyle;

        /// <summary>
        /// 
        /// </summary>
        private float[] dashPattern;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dashPattern"></param>
        ///// <param name="dashOffset"></param>
        //public LineDashStyle(float[] dashPattern, float dashOffset = 0)
        //    : this(/*DashStyle.Custom,*/ dashPattern, dashOffset)
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dashPattern"></param>
        /// <param name="dashOffset"></param>
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
        //        if (dashPattern == null) dashStyle = DashStyle.Solid;
        //        else if (dashPattern == Solid.dashPattern) dashStyle = DashStyle.Solid;
        //        else if (dashPattern == Dot.dashPattern) dashStyle = DashStyle.Dot;
        //        else if (dashPattern == Dash.dashPattern) dashStyle = DashStyle.Dash;
        //        else if (dashPattern == DashDot.dashPattern) dashStyle = DashStyle.DashDot;
        //        else if (dashPattern == DashDotDot.dashPattern) dashStyle = DashStyle.DashDotDot;
        //        else dashStyle = DashStyle.Custom;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DashPatternText { get { return ToString(); } set { Parse(value); } }

        /// <summary>
        /// 
        /// </summary>
        public float DashOffset { get; set; }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="LineDashStyle"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

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
            => ConvertToString(null /* format string */, provider);

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
            var output = new StringBuilder();
            foreach (var item in dashPattern)
            {
                output.Append($"{item.ToString(format, provider)},");
            }

            output.Replace(",-", "-");
            return output.ToString().Trim(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private float[] Parse(string text)
        {
            var argSeparators = @"[\s,]|(?=-)";
            return Regex.Split(text, argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => float.Parse(arg)).ToArray();
        }
    }
}
