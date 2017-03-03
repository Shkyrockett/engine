// <copyright file="Polygon.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Contour))]
    [XmlType(TypeName = "polygon", Namespace = "http://www.w3.org/2000/svg")]
    public class Contour
        : Shape, IEnumerable<Point2D>
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Contour()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public Contour(Contour polygon)
            : this(polygon.points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        public Contour(Polyline polyline)
            : this(polyline.Points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Contour(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Contour(IEnumerable<Point2D> points)
        {
            this.points = points as List<Point2D>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public Contour(IEnumerable<Polyline> polylines)
        {
            points = new List<Point2D>();
            foreach (Polyline polyline in polylines)
                points.Concat(polyline.Points);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D this[int index]
        {
            get { return (points as List<Point2D>)[index]; }
            set
            {
                (points as List<Point2D>)[index] = value;
                update?.Invoke();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public List<Point2D> Points
        {
            get { return points; }
            set
            {
                points = value;
                ClearCache();
                OnPropertyChanged(nameof(Points));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("points"), SoapAttribute("points")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Definition
        {
            get { return ToPathDefString(); }
            set
            {
                points = ParsePathDefString(value);
                ClearCache();
                OnPropertyChanged(nameof(Definition));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
            => points.Count;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => (double)CachingProperty(() => Measurements.PolygonPerimeter(points));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (points != null) ? (Rectangle2D)CachingProperty(() => Measurements.PolygonBounds(points)) : null;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Area
            => (double)CachingProperty(() => Math.Abs(Measurements.SignedPolygonArea(points)));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double SignedArea
            => (double)CachingProperty(() => Measurements.SignedPolygonArea(points));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RotationDirections Orientation
            => (RotationDirections)CachingProperty(() => (RotationDirections)Math.Sign(Measurements.SignedPolygonArea(points)));

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected new void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected new void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected new void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected new void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public Contour Add(Point2D point)
        {
            Points.Add(point);
            ClearCache();
            OnPropertyChanged(nameof(Add));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public Contour Reverse()
        {
            Points.Reverse();
            ClearCache();
            OnPropertyChanged(nameof(Reverse));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public Contour Translate(Point2D delta)
            => Translate(this, delta);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static Contour Translate(Contour path, Point2D delta)
        {
            List<Point2D> outPath = new List<Point2D>(path.points.Count);
            for (int i = 0; i < path.points.Count; i++)
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
            return new Contour(outPath);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
        {
            if (t == 0 || t == 1) return points[0];

            var weights = new(double length, double accumulated)[points.Count + 1];
            weights[0] = (0, 0);
            Point2D cursor = points[0];
            double accumulatedLength = 0;

            // Build up the weights map.
            for (int i = 1; i < points.Count + 1; i++)
            {
                double curentLength = Measurements.Distance(cursor, (i == points.Count) ? points[0] : points[i]);
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
                cursor = (i == points.Count) ? points[0] : points[i];
            }

            double accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (int i = points.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated <= accumulatedLengthT)
                {
                    // Interpolate the position.
                    double th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Interpolaters.Linear(points[i], (i == points.Count - 1) ? points[0] : points[i + 1], th);
                    break;
                }
            }

            return cursor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public LineSegment Segment(int index)
            => (index == points.Count - 1)
                ? new LineSegment(points[points.Count - 1], points[0])
                : new LineSegment(points[index], points[index + 1]);

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Contour Clone()
            => new Contour(points.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Contour Offset(double offset)
            => Offsets.Offset(this, offset);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point2D> GetEnumerator()
            => points.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <returns></returns>
        public static List<Point2D> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static List<Point2D> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // Discard whitespace and comma but keep the - minus sign.
            string separators = $@"[\s{Tokenizer.GetNumericListSeparator(provider)}]|(?=-)";

            var poly = new List<Point2D>();

            // Split the definition string into shape tokens.
            var list = Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg)).ToArray();

            if (list.Length % 2 != 0) throw new Exception("Polygon definitions must be in sets of two numbers.");

            for (int i = 0; i < list.Length - 1; i = i = 2)
            {
                poly.Add(new Point2D(list[i], list[i + 1]));
            }

            return poly;
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

            foreach (var item in points)
            {
                // M is Move to.
                output.Append($"{item.X},{item.Y} ");
            }

            // Minus signs are valid separators in SVG path definitions which can be
            // used in place of commas to shrink the length of the string. 
            output.Replace(",-", "-");
            return output.ToString().TrimEnd();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Contour"/> struct based on the format string
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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Contour);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Contour)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
