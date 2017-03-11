// <copyright file="PathContour.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
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
    /// A path shape item constructed with various sub shapes.
    /// Based roughly on the SVG Path.
    /// </summary>
    [Serializable]
    [DisplayName("Geometry Path")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlType(TypeName = "path", Namespace = "http://www.w3.org/2000/svg")]
    public class PathContour
        : Shape, IEnumerable<PathItem>
    {
        #region Fields

        List<PathItem> items;

        bool closed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathContour()
            : base()
        {
            Items = new List<PathItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public PathContour(Point2D start)
            : base()
        {
            Items = new List<PathItem>
            {
                new PathPoint(start)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public PathContour(Contour polygon)
        {
            Items = new List<PathItem>();
            PathItem cursor = new PathPoint(polygon[0]);
            Items.Add(cursor);
            for (int i = 1; i < polygon.Count; i++)
            {
                cursor = new PathLineSegment(cursor, polygon[i]);
                Items.Add(cursor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PathContour(List<PathItem> items)
            : base()
        {
            Items = items;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void Deconstruct(out List<PathItem> items)
            => items = this.Items;

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
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ListConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PathItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                ClearCache();
                OnPropertyChanged(nameof(Definition));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d"), SoapAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Definition
        {
            get { return ToPathDefString(); }
            set
            {
                Items = ParsePathDefString(value).Item1;
                ClearCache();
                OnPropertyChanged(nameof(Definition));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets a listing of all end nodes from the Figure.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public List<Point2D> Nodes
            => Items.Select(item => item.End.Value).ToList();

        /// <summary>
        /// Gets a listing of all end grips from the Figure.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public List<Point2D> Grips
        {
            get
            {
                var result = new List<Point2D>();
                foreach (var item in Items)
                {
                    result.AddRange(item.Grips);
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.GeometryPathBounds(this));

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [XmlIgnore, SoapIgnore]
        public override double Perimeter
            => (double)CachingProperty(() => Items.Sum(p => p.Length));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public int Count => items.Count;

        #endregion

        #region Serialization

        /// <summary>
        /// Sends an event indicating that this value went into the data file during serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        private void OnSerializing(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(PathContour)} is being serialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was reset after serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        private void OnSerialized(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(PathContour)} has been serialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was set during deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        private void OnDeserializing(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(PathContour)} is being deserialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was set after deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        private void OnDeserialized(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(PathContour)} has been deserialized.");
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
        {
            if (t == 0) return Items[0].Start.Value;
            if (t == 1) return Items[Items.Count - 1].End.Value;

            var weights = new(double length, double accumulated)[Items.Count];
            Point2D cursor = Items[0].End.Value;
            double accumulatedLength = 0;

            // Build up the weights map.
            for (int i = 0; i < Items.Count; i++)
            {
                double curentLength = Items[i].Length;
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
            }

            double accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated < accumulatedLengthT)
                {
                    // Interpolate the position.
                    double th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Items[i + 1].Interpolate(th);
                    break;
                }
            }

            return cursor;
        }

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PathItem> GetEnumerator()
            => items.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        public void Add(object o)
        {
            switch (o)
            {
                case PathLineSegment p:
                    AddLineSegment(p.End.Value);
                    break;
                case PathArc p:
                    AddArc(p.RX, p.RY, p.Angle, p.LargeArc, p.Sweep, p.End.Value);
                    break;
                case PathQuadraticBezier p:
                    AddQuadraticBezier(p.Handle.Value, p.End.Value);
                    break;
                case PathCubicBezier p:
                    AddCubicBezier(p.Handle1, p.Handle2.Value, p.End.Value);
                    break;
                case PathCardinal p:
                    AddCardinalCurve(p.Nodes);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public PathContour AddLineSegment(Point2D end)
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
        public PathContour AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
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
        public PathContour AddQuadraticBezier(Point2D handle, Point2D end)
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
        public PathContour AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
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
        internal PathContour AddCardinalCurve(List<Point2D> nodes)
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

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                // Retrieve the values.
                var args = Regex.Split(token.Substring(1), argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg)).ToArray();

                switch (cmd)
                {
                    case 'm': // Relative Svg moveto
                        relitive = true;
                        goto case 'M';
                    case 'M': // Svg moveto
                        item = new PathPoint(item, relitive, args);
                        startPoint = item.Start;
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'z': // Relative closepath
                        relitive = true;
                        goto case 'Z';
                    case 'Z': // Svg closepath
                        item = new PathPoint(item, relitive, startPoint.Value);
                        closed = true;
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'l': // Relative Svg lineto
                        relitive = true;
                        goto case 'L';
                    case 'L': // Svg lineto
                        item = new PathLineSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'h': // Relative Svg horizontal-lineto
                        relitive = true;
                        goto case 'H';
                    case 'H': // Svg horizontal-lineto
                        item = new PathLineSegment(item, relitive, item.End.Value.X, args[0]);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'v': // Relative Svg vertical-lineto
                        relitive = true;
                        goto case 'V';
                    case 'V': // Svg vertical-lineto
                        item = new PathLineSegment(item, relitive, args[0], item.End.Value.Y);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'c': // Relative Svg Cubic Bezier curveto
                        relitive = true;
                        goto case 'C';
                    case 'C': // Svg Cubic Bezier curveto
                        item = new PathCubicBezier(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 's': // Relative smooth-curveto
                        relitive = true;
                        goto case 'S';
                    case 'S': // Svg smooth-curveto
                        item = new PathCubicBezier(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'q': // Relative quadratic-bezier-curveto
                        relitive = true;
                        goto case 'Q';
                    case 'Q': // Svg quadratic-bezier-curveto
                        item = new PathQuadraticBezier(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 't': // Relative smooth-quadratic-bezier-curveto
                        relitive = true;
                        goto case 'T';
                    case 'T': // Svg smooth-quadratic-bezier-curveto
                        item = new PathQuadraticBezier(item, relitive, args)
                        {
                            Relitive = relitive
                        };
                        relitive = false;
                        break;
                    case 'a': // Relative Svg elliptical-arc
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
                        // ToDo: Figure out how to separate M from Z.
                        output.Append(t.Relitive ? $"m{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} " : $"M{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathPoint t:
                        output.Append(t.Relitive ? $"z{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} " : $"Z{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathLineSegment t:
                        // L is a general line.
                        char l = t.Relitive ? 'l' : 'L';
                        string coords = $"{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)}";
                        if (t.Start.Value.X == t.End.Value.X)
                        {
                            // H is a horizontal line, so the x-coordinate can be omitted.
                            coords = $"{t.End.Value.Y.ToString(format, provider)}";
                            l = t.Relitive ? 'h' : 'H';
                        }
                        else if (t.Start.Value.Y == t.End.Value.Y)
                        {
                            // V is a horizontal line, so the y-coordinate can be omitted.
                            coords = $"{t.End.Value.X.ToString(format, provider)}";
                            l = t.Relitive ? 'v' : 'V';
                        }
                        output.Append($"{l}{coords} ");
                        break;
                    case PathCubicBezier t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"c{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)},{t.Handle2.Value.X.ToString(format, provider)},{t.Handle2.Value.Y.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"C{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)},{t.Handle2.Value.X.ToString(format, provider)},{t.Handle2.Value.Y.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathQuadraticBezier t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"q{t.Handle.Value.X.ToString(format, provider)},{t.Handle.Value.X.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"Q{t.Handle.Value.X.ToString(format, provider)},{t.Handle.Value.X.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case PathArc t:
                        // Arc definition. 
                        int largearc = t.LargeArc ? 1 : 0;
                        int sweep = t.Sweep ? 1 : 0;
                        output.Append(t.Relitive ? $"a{t.RX.ToString(format, provider)},{t.RY.ToString(format, provider)},{t.Angle.ToString(format, provider)},{largearc},{sweep},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"A{t.RX.ToString(format, provider)},{t.RY.ToString(format, provider)},{t.Angle.ToString(format, provider)},{largearc},{sweep},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    default:
                        break;
                }
            }

            // Minus signs are valid separators in SVG path definitions which can be
            // used in place of commas to shrink the length of the string. 
            output.Replace(",-", "-");
            return output.ToString().TrimEnd();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="PathContour"/> struct based on the format string
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
            => (this == null) ? nameof(PathContour) : $"{nameof(PathContour)}{{{ToPathDefString(format, provider)}}}";

        #endregion
    }
}
