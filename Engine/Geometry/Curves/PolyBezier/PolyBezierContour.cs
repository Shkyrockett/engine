// <copyright file="PolyBezierContour.cs" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// A path shape item constructed with various BezierCurves.
    /// Based roughly on the SVG Path.
    /// </summary>
    [DataContract, Serializable]
    [DisplayName(nameof(PolyBezier))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlType(TypeName = "polybeziercontour", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public class PolyBezierContour
        : Shape
    {
        #region Fields
        /// <summary>
        /// The items.
        /// </summary>
        private List<BezierSegmentX> items;

        /// <summary>
        /// The closed.
        /// </summary>
        private bool closed = false;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierContour"/> class.
        /// </summary>
        public PolyBezierContour()
        {
            items = new List<BezierSegmentX>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierContour"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        public PolyBezierContour(Point2D start)
        {
            items = new List<BezierSegmentX>
            {
                new BezierSegmentX(start)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierContour"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public PolyBezierContour(List<BezierSegmentX> items)
        {
            this.items = items;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Deconstruct(out List<BezierSegmentX> items)
            => items = this.items;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type BezierSegmentX.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BezierSegmentX this[int index]
                => items[index];
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<BezierSegmentX> Items
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
                items = ParsePathDefString(value);
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
        {
            get
            {
                return (Rectangle2D)CachingProperty(() => bounds(this));
                static Rectangle2D bounds(PolyBezierContour contour)
                {
                    var box = contour.items[0].Bounds;
                    foreach (var item in contour.items)
                    {
                        box = box.Union(item.Bounds);
                    }

                    return box;
                }
            }
        }

        ///// <summary>
        /////
        ///// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        //public override double Perimeter
        //    => (double)CachingProperty(() => Items.Sum(p => p.Length));
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

        /// <summary>
        /// The reverse.
        /// </summary>
        public static void Reverse()
        {
        }

        #region Methods
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override bool Contains(Point2D point)
        //    => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// Add the line segment.
        /// </summary>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolyBezierContour"/>.</returns>
        public PolyBezierContour AddLineSegment(Point2D end)
        {
            var segment = new BezierSegmentX(Items[Items.Count - 1], end);
            Items.Add(segment);
            return this;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="r1"></param>
        ///// <param name="r2"></param>
        ///// <param name="angle"></param>
        ///// <param name="largeArc"></param>
        ///// <param name="sweep"></param>
        ///// <param name="end"></param>
        ///// <returns></returns>
        //public PolyBezier AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        //{
        //    var arc = new PathArc(Items[Items.Count - 1], r1, r2, angle, largeArc, sweep, end);
        //    Items.Add(arc);
        //    return this;
        //}

        /// <summary>
        /// Add the quadratic bezier.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolyBezierContour"/>.</returns>
        public PolyBezierContour AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new BezierSegmentX(Items[Items.Count - 1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        /// Add the cubic bezier.
        /// </summary>
        /// <param name="handle1">The handle1.</param>
        /// <param name="handle2">The handle2.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="PolyBezierContour"/>.</returns>
        public PolyBezierContour AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new BezierSegmentX(Items[Items.Count - 1], handle1, handle2, end);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<BezierSegmentX> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<BezierSegmentX> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // These letters are valid PolyBezier commands. Split the tokens at these.
            const string separators = @"(?=[MZLCQ])";

            // Discard whitespace and comma but keep the - minus sign.
            var argSeparators = $@"[\s{Tokenizer.GetNumericListSeparator(provider)}]|(?=-)";

            var contour = new PolyBezierContour();
            var segment = new BezierSegmentX();
            var startPoint = new Point2D();

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                // Retrieve the values.
                var args = Regex.Split(token.Substring(1), argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg)).ToArray();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contour = new PolyBezierContour(new Point2D(args[0], args[1]));
                        startPoint = segment.Start.Value;
                        break;
                    case 'Z':
                        // Z is End of Path.
                        contour.Closed = true;
                        break;
                    case 'L':
                        // L is a linear curve.
                        contour.AddLineSegment(new Point2D(args[0], args[1]));
                        break;
                    case 'Q':
                        // Q is for Quadratic.
                        contour.AddQuadraticBezier(new Point2D(args[0], args[1]), new Point2D(args[2], args[3]));
                        break;
                    case 'C':
                        // C is for Cubic.
                        contour.AddCubicBezier(new Point2D(args[0], args[1]), new Point2D(args[2], args[3]), new Point2D(args[4], args[5]));
                        break;
                    default:
                        // Unknown element.
                        break;
                }

            }

            return contour.Items;
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
        internal string ToPathDefString(string format, IFormatProvider provider)
        {
            var output = new StringBuilder();

            var sep = Tokenizer.GetNumericListSeparator(provider);

            foreach (var item in Items)
            {
                switch (item.Degree)
                {
                    case PolynomialDegree.Constant:
                        // M is Move to.
                        output.Append($"M{item.Start.Value.X.ToString(format, provider)}{sep}{item.Start.Value.Y.ToString(format, provider)} ");
                        break;
                    case PolynomialDegree.Linear:
                        // L is a linear curve.
                        output.Append($"L{item.End.Value.X.ToString(format, provider)}{sep}{item.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case PolynomialDegree.Quadratic:
                        // Q is for Quadratic.
                        output.Append($"Q{item.Handles[0].X.ToString(format, provider)}{sep}{item.Handles[0].X.ToString(format, provider)}{sep}{item.End.Value.X.ToString(format, provider)}{sep}{item.End.Value.Y.ToString(format, provider)} ");
                        break;
                    case PolynomialDegree.Cubic:
                        // C is for Cubic.
                        output.Append($"C{item.Handles[0].X.ToString(format, provider)}{sep}{item.Handles[0].Y.ToString(format, provider)}{sep}{item.Handles[1].X.ToString(format, provider)}{sep}{item.Handles[1].Y.ToString(format, provider)}{sep}{item.End.Value.X.ToString(format, provider)}{sep}{item.End.Value.Y.ToString(format, provider)} ");
                        break;
                    default:
                        break;
                }
            }
            // Z is End of Path.
            output.Append($"Z ");

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
            => (this is null) ? nameof(PolyBezierContour) : $"{nameof(PolyBezierContour)}{{{ToPathDefString(format, provider)}}}";
        #endregion Methods
    }
}
