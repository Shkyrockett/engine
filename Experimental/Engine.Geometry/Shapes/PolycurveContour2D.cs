// <copyright file="PolyCurveContour.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
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
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.PolycurveContour2D}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlType(TypeName = "path", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public struct PolycurveContour2D
        : IClosedShape, IPropertyCaching, IEquatable<PolycurveContour2D>, IEnumerable, IEnumerable<IShapeSegment>
    {
        #region Fields
        /// <summary>
        /// The items.
        /// </summary>
        private List<IShapeSegment> items;

        /// <summary>
        /// The closed.
        /// </summary>
        private bool closed;
        #endregion Fields

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour2D"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        public PolycurveContour2D(Point2D start)
            : this()
        {
            items = new List<IShapeSegment>
            {
                new Point2D(start)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour2D"/> class.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public PolycurveContour2D(PolygonContour2D polygon)
            : this()
        {
            items = new List<IShapeSegment>();
            IShapeSegment cursor = new Point2D(polygon[0]);
            items.Add(cursor);
            for (var i = 1; i < polygon.Count; i++)
            {
                cursor = new LineSegment2D(cursor, polygon[i]);
                Items.Add(cursor);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour2D"/> struct.
        /// </summary>
        /// <param name="items">The items.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D(List<IShapeSegment> items)
            : this()
        {
            this.items = items;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour2D"/> class.
        /// </summary>
        /// <param name="curves">The curves.</param>
        public PolycurveContour2D(CubicBezierSegment2D[] curves)
            : this()
        {
            if (curves is null)
            {
                throw new ArgumentNullException(nameof(curves));
            }

            items = new List<IShapeSegment> { new Point2D(curves[0].A) };
            foreach (var curve in curves)
            {
                AddCubicBezier(curve.B, curve.C, curve.D);
            }
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Deconstruct(out List<IShapeSegment> items) => items = Items;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type IShapeSegment.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IShapeSegment this[int index] => Items[index];
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The curves.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<IShapeSegment> Items
        {
            get { return items; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                items = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        /// <value>
        /// The first.
        /// </value>
        public IShapeSegment Head
        {
            get { return items[0]; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                items[0] = value;
                items[0].Trailing = items[^1].Leading;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the last.
        /// </summary>
        /// <value>
        /// The last.
        /// </value>
        public IShapeSegment Tail
        {
            get { return items[^1]; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                items[^1] = value;
                items[^1].Leading = items[0].Trailing;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        /// <value>
        /// The definition.
        /// </value>
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                Items = ParsePathDefString(value, CultureInfo.InvariantCulture).Item1;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a listing of all end nodes from the Figure.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Nodes => Items.Select(item => item.Tail.Value).ToList();

        /// <summary>
        /// Gets a listing of all end grips from the Figure.
        /// </summary>
        /// <value>
        /// The grips.
        /// </value>
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
        /// <value>
        ///   <c>true</c> if closed; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public bool Closed
        {
            get { return closed; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                closed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Rectangle2D Bounds
        {
            get
            {
                var curve = this;
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.PolycurveContourBounds(curve));
            }
        }

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        /// <value>
        /// The perimeter.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Perimeter
        {
            get
            {
                var items = Items;
                return (double)(this as IPropertyCaching).CachingProperty(() => items.Sum(p => p.Length));
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count => items.Count;

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        /// <value>
        /// The property cache.
        /// </value>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(PolycurveContour2D left, PolycurveContour2D right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(PolycurveContour2D left, PolycurveContour2D right) => !(left == right);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is PolycurveContour2D contour && Equals(contour);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] PolycurveContour2D other) => EqualityComparer<List<IShapeSegment>>.Default.Equals(Curves, other.Curves) && EqualityComparer<IShapeSegment>.Default.Equals(Head, other.Head) && EqualityComparer<IShapeSegment>.Default.Equals(Tail, other.Tail);
        #endregion

        #region Mutators
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
                case LineSegment2D p:
                    if (p.A == Items[^1].Tail.Value)
                    {
                        AddLineSegment(p.B);
                    }
                    else if (p.B == Items[^1].Tail.Value)
                    {
                        AddLineSegment(p.A);
                    }
                    else
                    {
                        AddLineSegment(p.A);
                        AddLineSegment(p.B);
                    }
                    break;
                case CircularArcSegment2D p:
                    AddArc(p.RX, p.RY, p.Angle, p.LargeArc, p.Sweep, p.Last);
                    break;
                case EllipticalArcSegment2D p:
                    AddArc(p.RX, p.RY, p.Angle, p.LargeArc, p.Sweep, p.Last);
                    break;
                case QuadraticBezierSegment2D p:
                    AddQuadraticBezier(p.Handle.Value, p.Last);
                    break;
                case CubicBezierSegment2D p:
                    AddCubicBezier(p.Handle1, p.Handle2.Value, p.Last);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Add the line segment.
        /// </summary>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddLineSegment(Point2D end)
        {
            var segment = new LineSegment2D(Items[^1].Tail.Value, end);
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
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddArc(double centerX, double centerY, double radius, double sweepAngle)
        {
            var arc = new CircularArcSegment2D(Items[^1], centerX, centerY, radius, sweepAngle);
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
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new CircularArcSegment2D(Items[^1], r1, r2, angle, largeArc, sweep, end);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        /// Add the quadratic bezier.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new QuadraticBezierSegment2D(Items[^1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        /// Add the quadratic beziers.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddQuadraticBeziers(QuadraticBezierSegment2D[] curves)
        {
            if (curves is null)
            {
                throw new ArgumentNullException(nameof(curves));
            }

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
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new CubicBezierSegment2D(items[^1], handle1, handle2, end);
            items.Add(cubic);
            return this;
        }

        /// <summary>
        /// Add the cubic beziers.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour2D AddCubicBeziers(CubicBezierSegment2D[] curves)
        {
            if (curves is null)
            {
                throw new ArgumentNullException(nameof(curves));
            }

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
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal PolycurveContour2D AddCardinalCurve(IList<Point2D> nodes)
        {
            var cardinal = new CardinalSegment2D(Items[^1], nodes);
            Items.Add(cardinal);
            return this;
        }

        /// <summary>
        /// Close.
        /// </summary>
        /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
        public PolycurveContour2D Close()
        {
            if (Items[0].Head.Value != Items[^1].Tail.Value)
            {
                AddLineSegment(Items[0].Head.Value);
            }

            closed = true;
            return this;
        }
        #endregion

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="Tuple{T1, T2}"/>.</returns>
        /// <remarks>
        /// <para>http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t</para>
        /// </remarks>
        public static (List<IShapeSegment>, bool) ParsePathDefString(string pathDefinition) => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="Tuple{T1, T2}"/>.</returns>
        /// <remarks>
        /// <para>http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t</para>
        /// </remarks>
        public static (List<IShapeSegment>, bool) ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            var figure = new List<IShapeSegment>();
            var closed = false;

            var relitive = false;
            Point2D? startPoint = null;
            IShapeSegment item = null;

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
                var args = Regex.Split(token.Substring(1), argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg, NumberStyles.Float, CultureInfo.InvariantCulture)).ToArray();

                switch (cmd)
                {
                    case 'm': // Relative Svg moveto
                        relitive = true;
                        goto case 'M';
                    case 'M': // Svg moveto
                        item = new PointSegment2D(item, relitive, args);
                        startPoint = item.Leading;
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'z': // Relative closepath
                        relitive = true;
                        goto case 'Z';
                    case 'Z': // Svg closepath
                        item = new PointSegment2D(item, relitive, startPoint.Value);
                        closed = true;
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'l': // Relative Svg lineto
                        relitive = true;
                        goto case 'L';
                    case 'L': // Svg lineto
                        item = new LineSegment(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'h': // Relative Svg horizontal-lineto
                        relitive = true;
                        goto case 'H';
                    case 'H': // Svg horizontal-lineto
                        item = new LineSegment(item, relitive, item.Tail.Value.X, args[0]);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'v': // Relative Svg vertical-lineto
                        relitive = true;
                        goto case 'V';
                    case 'V': // Svg vertical-lineto
                        item = new LineSegment(item, relitive, args[0], item.Tail.Value.Y);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'c': // Relative Svg Cubic Bézier curveto
                        relitive = true;
                        goto case 'C';
                    case 'C': // Svg Cubic Bézier curveto
                        item = new CubicBezierSegment2D(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 's': // Relative smooth-curveto
                        relitive = true;
                        goto case 'S';
                    case 'S': // Svg smooth-curveto
                        item = new CubicBezierSegment2D(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 'q': // Relative quadratic-bezier-curveto
                        relitive = true;
                        goto case 'Q';
                    case 'Q': // Svg quadratic-bezier-curveto
                        item = new QuadraticBezierSegment2D(item, relitive, args);
                        figure.Add(item);
                        item.Relitive = relitive;
                        relitive = false;
                        break;
                    case 't': // Relative smooth-quadratic-bezier-curveto
                        relitive = true;
                        goto case 'T';
                    case 'T': // Svg smooth-quadratic-bezier-curveto
                        item = new QuadraticBezierSegment2D(item, relitive, args)
                        {
                            Relitive = relitive
                        };
                        relitive = false;
                        break;
                    case 'a': // Relative Svg elliptical-arc
                        relitive = true;
                        goto case 'A';
                    case 'A': // Svg elliptical-arc
                        item = new ArcSegment2D(item, relitive, args);
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
        private string ToPathDefString() => ToPathDefString(string.Empty, CultureInfo.InvariantCulture);

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
                    case Point2D t when t.Leading is null:
                        // ToDo: Figure out how to separate M from Z.
                        output.Append(t.Relitive ? $"m{t.Head.Value.X.ToString(format, provider)}{sep}{t.Head.Value.Y.ToString(format, provider)} " : $"M{t.Head.Value.X.ToString(format, provider)}{sep}{t.Head.Value.Y.ToString(format, provider)} ");
                        break;
                    case Point2D t:
                        output.Append(t.Relitive ? $"z{t.Head.Value.X.ToString(format, provider)}{sep}{t.Head.Value.Y.ToString(format, provider)} " : $"Z{t.Head.Value.X.ToString(format, provider)}{sep}{t.Head.Value.Y.ToString(format, provider)} ");
                        break;
                    case LineSegment2D t:
                        // L is a general line.
                        var l = t.Relitive ? 'l' : 'L';
                        var coords = $"{t.Tail.Value.X.ToString(format, provider)}{sep}{t.Tail.Value.Y.ToString(format, provider)}";
                        if (t.Head.Value.X == t.Tail.Value.X)
                        {
                            // H is a horizontal line, so the x-coordinate can be omitted.
                            coords = $"{t.Tail.Value.Y.ToString(format, provider)}";
                            l = t.Relitive ? 'h' : 'H';
                        }
                        else if (t.Head.Value.Y == t.Tail.Value.Y)
                        {
                            // V is a horizontal line, so the y-coordinate can be omitted.
                            coords = $"{t.Tail.Value.X.ToString(format, provider)}";
                            l = t.Relitive ? 'v' : 'V';
                        }
                        output.Append($"{l}{coords} ");
                        break;
                    case CubicBezierSegment2D t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"c{t.Handle1.X.ToString(format, provider)}{sep}{t.Handle1.Y.ToString(format, provider)}{sep}{t.Handle2.Value.X.ToString(format, provider)}{sep}{t.Handle2.Value.Y.ToString(format, provider)}{sep}{t.Trailing.Value.X.ToString(format, provider)}{sep}{t.Trailing.Value.Y.ToString(format, provider)} " : $"C{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)}v{t.Handle2.Value.X.ToString(format, provider)}{sep}{t.Handle2.Value.Y.ToString(format, provider)}{sep}{t.Trailing.Value.X.ToString(format, provider)}{sep}{t.Trailing.Value.Y.ToString(format, provider)} ");
                        break;
                    case QuadraticBezierSegment2D t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"q{t.Handle.Value.X.ToString(format, provider)}{sep}{t.Handle.Value.X.ToString(format, provider)}{sep}{t.Trailing.Value.X.ToString(format, provider)}{sep}{t.Trailing.Value.Y.ToString(format, provider)} " : $"Q{t.Handle.Value.X.ToString(format, provider)}{sep}{t.Handle.Value.X.ToString(format, provider)}{sep}{t.Trailing.Value.X.ToString(format, provider)}{sep}{t.Trailing.Value.Y.ToString(format, provider)} ");
                        break;
                    case CircularArcSegment2D t:
                        // Arc definition.
                        var largearc = t.LargeArc ? 1 : 0;
                        var sweep = t.Sweep ? 1 : 0;
                        output.Append(t.Relitive ? $"a{t.RX.ToString(format, provider)}{sep}{t.RY.ToString(format, provider)}{sep}{t.Angle.ToString(format, provider)}{sep}{largearc}{sep}{sweep}{sep}{t.Tail.Value.X.ToString(format, provider)}{sep}{t.Tail.Value.Y.ToString(format, provider)} " : $"A{t.RX.ToString(format, provider)}{sep}{t.RY.ToString(format, provider)}{sep}{t.Angle.ToString(format, provider)}{sep}{largearc}{sep}{sweep}{sep}{t.Tail.Value.X.ToString(format, provider)}{sep}{t.Tail.Value.Y.ToString(format, provider)} ");
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
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D Interpolate(double t)
        {
            if (t == 0)
            {
                return Items[0].Head.Value;
            }

            if (t == 1)
            {
                return Items[^1].Tail.Value;
            }

            var weights = new (double length, double accumulated)[Items.Count];
            var cursor = Items[0].Tail.Value;
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

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

        #region Standard Methods
        /// <summary>
        /// Raises the property changing event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanging([CallerMemberName] string name = "") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Items, Head.Tail, Tail.Head);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<IShapeSegment> GetEnumerator() => items.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Creates a string representation of this <see cref="PolycurveContour2D"/> struct based on the format string
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
        public string ToString(string format, IFormatProvider provider) => (this == null) ? nameof(PolycurveContour2D) : $"{nameof(PolycurveContour2D)}{{{ToPathDefString(format, provider)}}}";
        #endregion
    }
}