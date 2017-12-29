// <copyright file="PolyCurveContour.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    public class PolycurveContour
        : Shape, IEnumerable<CurveSegment>
    {
        #region Fields

        /// <summary>
        /// The items.
        /// </summary>
        List<CurveSegment> items;

        /// <summary>
        /// The closed.
        /// </summary>
        bool closed = false;
        //private CubicBezier[] cubicBezier;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        public PolycurveContour()
            : base()
        {
            Items = new List<CurveSegment>();
        }

        /// <summary>
        ///
        /// </summary>
        public PolycurveContour(Point2D start)
            : base()
        {
            Items = new List<CurveSegment>
            {
                new PointSegment(start)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        public PolycurveContour(PolygonContour polygon)
        {
            Items = new List<CurveSegment>();
            CurveSegment cursor = new PointSegment(polygon[0]);
            Items.Add(cursor);
            for (var i = 1; i < polygon.Count; i++)
            {
                cursor = new LineCurveSegment(cursor, polygon[i]);
                Items.Add(cursor);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public PolycurveContour(List<CurveSegment> items)
            : base()
        {
            Items = items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="curves"></param>
        public PolycurveContour(CubicBezier[] curves)
        {
            Items = new List<CurveSegment> { new PointSegment(curves[0].A) };
            foreach (var curve in curves)
            {
                AddCubicBezier(curve.B, curve.C, curve.D);
            }
        }

        #endregion

        #region Deconstructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        public void Deconstruct(out List<CurveSegment> items)
            => items = Items;

        #endregion

        #region Indexers

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CurveSegment this[int index]
                => Items[index];

        #endregion

        #region Properties

        /// <summary>
        ///
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
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.PolycurveContourBounds(this));

        /// <summary>
        ///
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Perimeter
            => (double)CachingProperty(() => Items.Sum(p => p.Length));

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count
            => items.Count;

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PolycurveContour)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PolycurveContour)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PolycurveContour)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PolycurveContour)} has been deserialized.");
        //}

        //#endregion

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
        public IEnumerator<CurveSegment> GetEnumerator()
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
                case Point2D p:
                    AddLineSegment(p);
                    break;
                case ScreenPoint p:
                    AddLineSegment(p.Point);
                    break;
                case LineSegment p:
                    if (p.A == Items[Items.Count - 1].End)
                        AddLineSegment(p.B);
                    else if (p.B == Items[Items.Count - 1].End)
                        AddLineSegment(p.A);
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
        ///
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public PolycurveContour AddLineSegment(Point2D end)
        {
            var segment = new LineCurveSegment(Items[Items.Count - 1], end);
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
        public PolycurveContour AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new ArcSegment(Items[Items.Count - 1], r1, r2, angle, largeArc, sweep, end);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public PolycurveContour AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new QuadraticBezierSegment(Items[Items.Count - 1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="curves"></param>
        /// <returns></returns>
        public PolycurveContour AddQuadraticBeziers(CubicBezier[] curves)
        {
            foreach (var curve in curves)
            {
                AddQuadraticBezier(curve.B, curve.C);
            }
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public PolycurveContour AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new CubicBezierSegment(Items[Items.Count - 1], handle1, handle2, end);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="curves"></param>
        /// <returns></returns>
        public PolycurveContour AddCubicBeziers(CubicBezier[] curves)
        {
            foreach (var curve in curves)
            {
                AddCubicBezier(curve.B, curve.C, curve.D);
            }
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        internal PolycurveContour AddCardinalCurve(List<Point2D> nodes)
        {
            var cardinal = new CardinalSegment(Items[Items.Count - 1], nodes);
            Items.Add(cardinal);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t
        /// </remarks>
        public static (List<CurveSegment>, bool) ParsePathDefString(string pathDefinition)
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
                    case 'c': // Relative Svg Cubic Bezier curveto
                        relitive = true;
                        goto case 'C';
                    case 'C': // Svg Cubic Bezier curveto
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
            var output = new StringBuilder();

            var sep = Tokenizer.GetNumericListSeparator(provider);

            foreach (var item in Items)
            {
                switch (item)
                {
                    case PointSegment t when (t.Previous is null):
                        // ToDo: Figure out how to separate M from Z.
                        output.Append(t.Relitive ? $"m{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} " : $"M{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PointSegment t:
                        output.Append(t.Relitive ? $"z{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} " : $"Z{t.Start.Value.X.ToString(format, provider)},{t.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case LineCurveSegment t:
                        // L is a general line.
                        var l = t.Relitive ? 'l' : 'L';
                        var coords = $"{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)}";
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
                        output.Append(t.Relitive ? $"c{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)},{t.Handle2.Value.X.ToString(format, provider)},{t.Handle2.Value.Y.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"C{t.Handle1.X.ToString(format, provider)},{t.Handle1.Y.ToString(format, provider)},{t.Handle2.Value.X.ToString(format, provider)},{t.Handle2.Value.Y.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case QuadraticBezierSegment t:
                        // ToDo: Figure out how to tell if a point can be omitted for the smooth version.
                        output.Append(t.Relitive ? $"q{t.Handle.Value.X.ToString(format, provider)},{t.Handle.Value.X.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} " : $"Q{t.Handle.Value.X.ToString(format, provider)},{t.Handle.Value.X.ToString(format, provider)},{t.End.Value.X.ToString(format, provider)},{t.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case ArcSegment t:
                        // Arc definition.
                        var largearc = t.LargeArc ? 1 : 0;
                        var sweep = t.Sweep ? 1 : 0;
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
            => (this == null) ? nameof(PolycurveContour) : $"{nameof(PolycurveContour)}{{{ToPathDefString(format, provider)}}}";

        #endregion
    }
}
