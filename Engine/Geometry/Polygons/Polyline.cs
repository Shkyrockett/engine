// <copyright file="Polyline.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polyline))]
    [XmlType(TypeName = "polyline", Namespace = "http://www.w3.org/2000/svg")]
    public class Polyline
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
        public Polyline()
            : this(new List<Point2D>())
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        public Polyline(PolygonContour polygon)
            : this(polygon.Points)
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polyline"></param>
        public Polyline(Polyline polyline)
            : this(polyline.points)
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="points"></param>
        public Polyline(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="points"></param>
        public Polyline(IEnumerable<Point2D> points)
        {
            Points = points as List<Point2D>;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polylines"></param>
        public Polyline(IEnumerable<Polyline> polylines)
        {
            points = new List<Point2D>();
            foreach (Polyline polyline in polylines)
                points.AddRange(polyline.Points);
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void Deconstruct(out List<Point2D> points)
            => points = this.points;

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
            get { return points[index]; }
            set
            {
                points[index] = value;
                update?.Invoke();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        [XmlArray]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged(nameof(Points));
                update?.Invoke();
            }
        }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => (double)CachingProperty(() => points.Zip(points.Skip(1), Measurements.Distance).Sum());

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.PolylineBounds(points));

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public int Count
            => points.Count;

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polyline)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polyline)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polyline)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polyline)} has been deserialized.");
        //}

        //#endregion

        #region Mutators

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        public Polyline Add(Point2D point)
        {
            Points.Add(point);
            OnPropertyChanged(nameof(Add));
            update?.Invoke();
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        public Polyline Reverse()
        {
            Points.Reverse();
            OnPropertyChanged(nameof(Reverse));
            update?.Invoke();
            return this;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
        {
            switch (t)
            {
                case 0:
                    return points[0];
                case 1:
                    return points[points.Count - 1];
                default:
                    break;
            }

            var weights = new(double length, double accumulated)[points.Count];
            weights[0] = (0, 0);
            var cursor = points[0];
            double accumulatedLength = 0;

            // Build up the weights map.
            for (var i = 1; i < points.Count; i++)
            {
                var curentLength = Measurements.Distance(cursor, points[i]);
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
                cursor = points[i];
            }

            var accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (var i = points.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated <= accumulatedLengthT)
                {
                    // Interpolate the position.
                    var th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Interpolators.Linear(points[i], points[i + 1], th);
                    break;
                }
            }

            return cursor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public Polyline Translate(Point2D delta)
            => Translate(this, delta);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static Polyline Translate(Polyline path, Point2D delta)
        {
            var outPath = new List<Point2D>(path.points.Count);
            for (var i = 0; i < path.points.Count; i++)
            {
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
            }

            return new Polyline(outPath);
        }

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polyline Clone()
            => new Polyline(Points.ToArray());

        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polyline Offset(double offset)
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
        /// Creates a string representation of this <see cref="Polyline"/> struct based on the format string
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
            if (this == null)
            {
                return nameof(Polyline);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polyline)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
