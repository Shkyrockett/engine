// <copyright file="GeometryPath.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// A path shape item constructed with various sub shapes.
    /// Based roughly on then SVG Path.
    /// </summary>
    [Serializable]
    [DisplayName("Geometry Path")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlType(TypeName = "path", Namespace = "http://www.w3.org/2000/svg")]
    public class GeometryPath
        : Shape
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public GeometryPath()
            => Items = new List<PathItem>();

        /// <summary>
        /// 
        /// </summary>
        public GeometryPath(Point2D start)
            => Items.Add(new PathPoint(start));

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PathItem this[int index]
                => Items[index];

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ListConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PathItem> Items { get; set; } = new List<PathItem>();

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Deffinition { get => ToPathDefString(); set => Items = ParsePathDefString(value).Item1; }

        /// <summary>
        /// Gets a listing of all end nodes from the Figure.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public List<Point2D> Nodes
            => Items.Select(item => item.End.Value).ToList();

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public bool Closed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => Boundings.GeometryPath(this);

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [XmlIgnore, SoapIgnore]
        public override double Perimeter => Items.Sum(p => p.Length);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Containings.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public GeometryPath AddLineSegment(Point2D end)
        {
            var segment = new PathLineSegment(Items[Items.Count - 1], end);
            Items.Add(segment);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GeometryPath AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new PathArc(Items[Items.Count - 1], r1, r2, angle, largeArc, sweep, end);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GeometryPath AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new PathQuadraticBezier(Items[Items.Count - 1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GeometryPath AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new PathCubicBezier(Items[Items.Count - 1], handle1, handle2, end);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private GeometryPath AddCardinalCurve(List<Point2D> nodes)
        {
            var cubic = new PathCardinal(Items[Items.Count - 1], nodes);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t
        /// </remarks>
        public (List<PathItem>, bool) ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t
        /// </remarks>
        public (List<PathItem>, bool) ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            List<PathItem> figure = new List<PathItem>();
            bool closed = false;

            bool relitive = false;
            Point2D? startPoint = null;
            PathItem item = null;

            // These letters are valid SVG commands. Split the tokens at these.
            string separators = @"(?=[MZLHVCSQTAmzlhvcsqta])";

            char sep = Tokenizer.GetNumericListSeparator(provider);

            // Discard whitespace and comma but keep the - minus sign.
            string argSeparators = $@"[\s{sep}]|(?=-)";

            // Split the deffinition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                // Retrieve the values.
                var args = Regex.Split(token.Substring(1), argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg)).ToArray();

                switch (cmd)
                {
                    case 'm': // Reletive Svg moveto
                        relitive = true;
                        goto case 'M';
                    case 'M': // Svg moveto
                        item = new PathPoint(item, relitive, args);
                        startPoint = item.Start;
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'z': // Reletive closepath
                        relitive = true;
                        goto case 'Z';
                    case 'Z': // Svg closepath
                        item = new PathPoint(item, relitive, startPoint.Value);
                        closed = true;
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'l': // Reletive Svg lineto
                        relitive = true;
                        goto case 'L';
                    case 'L': // Svg lineto
                        item = new PathLineSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'h': // Reletive Svg horizontal-lineto
                        relitive = true;
                        goto case 'H';
                    case 'H': // Svg horizontal-lineto
                        item = new PathLineSegment(item, relitive, item.End.Value.X, args[0]);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'v': // Reletive Svg vertical-lineto
                        relitive = true;
                        goto case 'V';
                    case 'V': // Svg vertical-lineto
                        item = new PathLineSegment(item, relitive, args[0], item.End.Value.Y);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'c': // Reletive Svg Cubic Bezier curveto
                        relitive = true;
                        goto case 'C';
                    case 'C': // Svg Cubic Bezier curveto
                        item = new PathCubicBezier(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 's': // Reletive smooth-curveto
                        relitive = true;
                        goto case 'S';
                    case 'S': // Svg smooth-curveto
                        item = new PathCubicBezier(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'q': // Reletive quadratic-bezier-curveto
                        relitive = true;
                        goto case 'Q';
                    case 'Q': // Svg quadratic-bezier-curveto
                        item = new PathQuadraticBezier(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 't': // Reletive smooth-quadratic-bezier-curveto
                        relitive = true;
                        goto case 'T';
                    case 'T': // Svg smooth-quadratic-bezier-curveto
                        item = new PathQuadraticBezier(item, relitive, args)
                        {
                            Relitive = relitive
                        };
                        relitive = false;
                        break;
                    case 'a': // Reletive Svg elliptical-arc
                        relitive = true;
                        goto case 'A';
                    case 'A': // Svg elliptical-arc
                        item = new PathArc(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    default: // Unknown element.
                        break;
                }
            }

            return (figure, closed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private String ToPathDefString()
            => ToPathDefString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        private String ToPathDefString(string format, IFormatProvider provider)
        {
            StringBuilder output = new StringBuilder();

            char sep = Tokenizer.GetNumericListSeparator(provider);

            foreach (var item in Items)
            {
                switch (item)
                {
                    case PathPoint t when (t.Previous is null):
                        // ToDo: Figure out how to seporate M from Z.
                        output.Append(t.Relitive ? $"m{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} " : $"M{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathPoint t:
                        output.Append(t.Relitive ? $"z{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} " : $"Z{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathLineSegment t:
                        // L is ageneral line.
                        char l = t.Relitive ? 'l' : 'L';
                        string coords = $"{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)}";
                        if (t.Start.Value.X == t.End.Value.X)
                        {
                            // H is a horizontal line, so the x-coordinate can be ommited.
                            coords = $"{t.End.Value.Y.ToString(format, provider)}";
                            l = t.Relitive ? 'h' : 'H';
                        }
                        else if (t.Start.Value.Y == t.End.Value.Y)
                        {
                            // V is a horizontal line, so the y-coordinate can be ommited.
                            coords = $"{t.End.Value.X.ToString(format, provider)}";
                            l = t.Relitive ? 'v' : 'V';
                        }
                        output.Append($"{l}{coords} ");
                        break;
                    case PathCubicBezier t:
                        // ToDo: Figure out how to tell if a point can be ommited for the smooth version.
                        output.Append(t.Relitive ? $"c{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)},{t.Handle2.Value.X.ToString(format, provider)},{t.Handle2.Value.Y.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"C{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)},{t.Handle2.Value.X.ToString(format, provider)},{t.Handle2.Value.Y.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathQuadraticBezier t:
                        // ToDo: Figure out how to tell if a point can be ommited for the smooth version.
                        output.Append(t.Relitive ? $"q{t.Handle.Value.X.ToString(format, provider)},{t.Handle.Value.X.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"Q{t.Handle.Value.X.ToString(format, provider)},{t.Handle.Value.X.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathArc t:
                        // Arc deffinition. 
                        int largearc = t.LargeArc ? 1 : 0;
                        int sweep = t.Sweep ? 1 : 0;
                        output.Append(t.Relitive ? $"a{t.RX.ToString(format, provider)},{t.RY.ToString(format, provider)},{t.Angle.ToString(format, provider)},{largearc},{sweep},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"A{t.RX.ToString(format, provider)},{t.RY.ToString(format, provider)},{t.Angle.ToString(format, provider)},{largearc},{sweep},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    default:
                        break;
                }
            }

            // Minus signs are valid seperators in SVG path deffinitions which can be
            // used in place of commas to shrink the length of the string. 
            output.Replace(",-", "-");
            return output.ToString().TrimEnd();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="GeometryPath"/> struct based on the format string
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
            => (this == null) ? nameof(GeometryPath) : $"{nameof(GeometryPath)}{{{ToPathDefString(format, provider)}}}";

        #endregion
    }
}
