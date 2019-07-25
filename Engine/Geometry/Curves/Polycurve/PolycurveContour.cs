// <copyright file="PolyCurveContour.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
    [DataContract, Serializable]
    [DisplayName("PolyCurve Contour")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlType(TypeName = "path", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public class PolycurveContour
        : Shape, IEnumerable<CurveSegment>
    {
        #region Fields
        /// <summary>
        /// The items.
        /// </summary>
        private List<CurveSegment> items;

        /// <summary>
        /// The closed.
        /// </summary>
        private bool closed = false;

        //private CubicBezier[] cubicBezier;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour"/> class.
        /// </summary>
        public PolycurveContour()
        {
            items = new List<CurveSegment>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        public PolycurveContour(Point2D start)
        {
            items = new List<CurveSegment>
            {
                new PointSegment(start)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour"/> class.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public PolycurveContour(PolygonContour polygon)
        {
            items = new List<CurveSegment>();
            CurveSegment cursor = new PointSegment(polygon[0]);
            items.Add(cursor);
            for (var i = 1; i < polygon.Count; i++)
            {
                cursor = new LineCurveSegment(cursor, polygon[i]);
                Items.Add(cursor);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public PolycurveContour(List<CurveSegment> items)
        {
            this.items = items;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour"/> class.
        /// </summary>
        /// <param name="curves">The curves.</param>
        public PolycurveContour(CubicBezier[] curves)
        {
            items = new List<CurveSegment> { new PointSegment(curves[0].A) };
            foreach (var curve in curves)
            {
                AddCubicBezier(curve.B, curve.C, curve.D);
            }
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Deconstruct(out List<CurveSegment> items)
            => items = Items;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type CurveSegment.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CurveSegment this[int index]
                => Items[index];
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<CurveSegment> Items
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
        /// Gets or sets the definition.
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Nodes
            => Items.Select(item => item.End.Value).ToList();

        /// <summary>
        /// Gets a listing of all end grips from the Figure.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
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
        /// Gets or sets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.PolycurveContourBounds(this));

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Perimeter
            => (double)CachingProperty(() => Items.Sum(p => p.Length));

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count
            => items.Count;
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
        {
            if (t == 0)
            {
                return Items[0].Start.Value;
            }

            if (t == 1)
            {
                return Items[Items.Count - 1].End.Value;
            }

            var weights = new (double length, double accumulated)[Items.Count];
            var cursor = Items[0].End.Value;
            double accumulatedLength = 0;

            // Build up the weights map.
            for (var i = 0; i < Items.Count; i++)
            {
                var curentLength = Items[i].Length;
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
            }

            var accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (var i = Items.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated < accumulatedLengthT)
                {
                    // Interpolate the position.
                    var th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Items[i + 1].Interpolate(th);
                    break;
                }
            }

            return cursor;
        }

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusions.Outside;

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<CurveSegment> GetEnumerator()
            => items.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="o">The o.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(object o)
        {
            switch (o)
            {
                case Point2D p:
                    AddLineSegment(p);
                    break;
                case ScreenPoint p:
                    AddLineSegment(p.Point);
                    break;
                case LineSegment p:
                    if (p.A == Items[Items.Count - 1].End)
                    {
                        AddLineSegment(p.B);
                    }
                    else if (p.B == Items[Items.Count - 1].End)
                    {
                        AddLineSegment(p.A);
                    }
                    else
                    {
                        AddLineSegment(p.A);
                        AddLineSegment(p.B);
                    }
                    break;
                case LineCurveSegment p:
                    AddLineSegment(p.End.Value);
                    break;
                case ArcSegment p:
                    AddArc(p.RX, p.RY, p.Angle, p.LargeArc, p.Sweep, p.End.Value);
                    break;
                case QuadraticBezierSegment p:
                    AddQuadraticBezier(p.Handle.Value, p.End.Value);
                    break;
                case CubicBezierSegment p:
                    AddCubicBezier(p.Handle1, p.Handle2.Value, p.End.Value);
                    break;
                case CardinalSegment p:
                    AddCardinalCurve(p.Nodes);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Add the line segment.
        /// </summary>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddLineSegment(Point2D end)
        {
            var segment = new LineCurveSegment(Items[Items.Count - 1], end);
            Items.Add(segment);
            return this;
        }

        /// <summary>
        /// Add the arc.
        /// </summary>
        /// <param name="centerX">The centerX.</param>
        /// <param name="centerY">The centerY.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddArc(double centerX, double centerY, double radius, double sweepAngle)
        {
            var arc = new ArcSegment(Items[Items.Count - 1], centerX, centerY, radius, sweepAngle);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        /// Add the arc.
        /// </summary>
        /// <param name="r1">The r1.</param>
        /// <param name="r2">The r2.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="largeArc">The largeArc.</param>
        /// <param name="sweep">The sweep.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new ArcSegment(Items[Items.Count - 1], r1, r2, angle, largeArc, sweep, end);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        /// Add the quadratic bezier.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new QuadraticBezierSegment(Items[Items.Count - 1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        /// Add the quadratic beziers.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddQuadraticBeziers(QuadraticBezier[] curves)
        {
            foreach (var curve in curves)
            {
                AddQuadraticBezier(curve.B, curve.C);
            }
            return this;
        }

        /// <summary>
        /// Add the cubic bezier.
        /// </summary>
        /// <param name="handle1">The handle1.</param>
        /// <param name="handle2">The handle2.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new CubicBezierSegment(items[items.Count - 1], handle1, handle2, end);
            items.Add(cubic);
            return this;
        }

        /// <summary>
        /// Add the cubic beziers.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour AddCubicBeziers(CubicBezier[] curves)
        {
            foreach (var curve in curves)
            {
                AddCubicBezier(curve.B, curve.C, curve.D);
            }
            return this;
        }

        /// <summary>
        /// Add the cardinal curve.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal PolycurveContour AddCardinalCurve(List<Point2D> nodes)
        {
            var cardinal = new CardinalSegment(Items[Items.Count - 1], nodes);
            Items.Add(cardinal);
            return this;
        }

        /// <summary>
        /// Close.
        /// </summary>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour Close()
        {
            if (Items[0].Start.Value != Items[Items.Count - 1].End.Value)
            {
                AddLineSegment(Items[0].Start.Value);
            }

            closed = true;
            return this;
        }

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="Tuple{T1, T2}"/>.</returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t
        /// </remarks>
        public static (List<CurveSegment>, bool) ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="Tuple{T1, T2}"/>.</returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t
        /// </remarks>
        public static (List<CurveSegment>, bool) ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            var figure = new List<CurveSegment>();
            var closed = false;

            var relitive = false;
            Point2D? startPoint = null;
            CurveSegment item = null;

            // These letters are valid SVG commands. Split the tokens at these.
            const string separators = @"(?=[MZLHVCSQTAmzlhvcsqta])";

            var sep = Tokenizer.GetNumericListSeparator(provider);

            // Discard whitespace and comma but keep the - minus sign.
            var argSeparators = $@"[\s{sep}]|(?=-)";

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
                        item = new PointSegment(item, relitive, args);
                        startPoint = item.Start;
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'z': // Relative closepath
                        relitive = true;
                        goto case 'Z';
                    case 'Z': // Svg closepath
                        item = new PointSegment(item, relitive, startPoint.Value);
                        closed = true;
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'l': // Relative Svg lineto
                        relitive = true;
                        goto case 'L';
                    case 'L': // Svg lineto
                        item = new LineCurveSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'h': // Relative Svg horizontal-lineto
                        relitive = true;
                        goto case 'H';
                    case 'H': // Svg horizontal-lineto
                        item = new LineCurveSegment(item, relitive, item.End.Value.X, args[0]);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'v': // Relative Svg vertical-lineto
                        relitive = true;
                        goto case 'V';
                    case 'V': // Svg vertical-lineto
                        item = new LineCurveSegment(item, relitive, args[0], item.End.Value.Y);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'c': // Relative Svg Cubic Bézier curveto
                        relitive = true;
                        goto case 'C';
                    case 'C': // Svg Cubic Bézier curveto
                        item = new CubicBezierSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 's': // Relative smooth-curveto
                        relitive = true;
                        goto case 'S';
                    case 'S': // Svg smooth-curveto
                        item = new CubicBezierSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'q': // Relative quadratic-bezier-curveto
                        relitive = true;
                        goto case 'Q';
                    case 'Q': // Svg quadratic-bezier-curveto
                        item = new QuadraticBezierSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 't': // Relative smooth-quadratic-bezier-curveto
                        relitive = true;
                        goto case 'T';
                    case 'T': // Svg smooth-quadratic-bezier-curveto
                        item = new QuadraticBezierSegment(item, relitive, args)
                        {
                            Relitive = relitive
                        };
                        relitive = false;
                        break;
                    case 'a': // Relative Svg elliptical-arc
                        relitive = true;
                        goto case 'A';
                    case 'A': // Svg elliptical-arc
                        item = new ArcSegment(item, relitive, args);
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
        /// The to path def string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString()
            => ToPathDefString(string.Empty, CultureInfo.InvariantCulture);

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString(string format, IFormatProvider provider)
        {
            var output = new StringBuilder();

            var sep = Tokenizer.GetNumericListSeparator(provider);

            foreach (var item in Items)
            {
                switch (item)
                {
                    case PointSegment t when t.Previous is null:
                        // ToDo: Figure out how to separate M from Z.
                        output.Append(t.Relitive ? $"m{t.Start.Value.X.ToString(format, provider)}{sep}{t.Start.Value.Y.ToString(format, provider)} " : $"M{t.Start.Value.X.ToString(format, provider)}{sep}{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PointSegment t:
                        output.Append(t.Relitive ? $"z{t.Start.Value.X.ToString(format, provider)}{sep}{t.Start.Value.Y.ToString(format, provider)} " : $"Z{t.Start.Value.X.ToString(format, provider)}{sep}{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case LineCurveSegment t:
                        // L is a general line.
                        var l = t.Relitive ? 'l' : 'L';
                        var coords = $"{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)}";
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
                    case CubicBezierSegment t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"c{t.Handle1.X.ToString(format, provider)}{sep}{t.Handle1.Y.ToString(format, provider)}{sep}{t.Handle2.Value.X.ToString(format, provider)}{sep}{t.Handle2.Value.Y.ToString(format, provider)}{sep}{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)} " : $"C{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)}v{t.Handle2.Value.X.ToString(format, provider)}{sep}{t.Handle2.Value.Y.ToString(format, provider)}{sep}{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case QuadraticBezierSegment t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"q{t.Handle.Value.X.ToString(format, provider)}{sep}{t.Handle.Value.X.ToString(format, provider)}{sep}{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)} " : $"Q{t.Handle.Value.X.ToString(format, provider)}{sep}{t.Handle.Value.X.ToString(format, provider)}{sep}{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case ArcSegment t:
                        // Arc definition.
                        var largearc = t.LargeArc ? 1 : 0;
                        var sweep = t.Sweep ? 1 : 0;
                        output.Append(t.Relitive ? $"a{t.RX.ToString(format, provider)}{sep}{t.RY.ToString(format, provider)}{sep}{t.Angle.ToString(format, provider)}{sep}{largearc}{sep}{sweep}{sep}{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)} " : $"A{t.RX.ToString(format, provider)}{sep}{t.RY.ToString(format, provider)}{sep}{t.Angle.ToString(format, provider)}{sep}{largearc}{sep}{sweep}{sep}{t.End.Value.X.ToString(format, provider)}{sep}{t.End.Value.Y.ToString(format, provider)} ");
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
        /// Creates a string representation of this <see cref="PolycurveContour"/> struct based on the format string
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
            => (this is null) ? nameof(PolycurveContour) : $"{nameof(PolycurveContour)}{{{ToPathDefString(format, provider)}}}";
        #endregion Methods
    }
}
