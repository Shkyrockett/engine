// <copyright file="Polyline.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polyline))]
    public class Polyline
        : Shape, IOpenShape
    {
        #region Private Fields

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
        public Polyline(Polygon polygon)
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
            => Points = points as List<Point2D>;

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
        {
            points = this.points;
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
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => (double)CachingProperty(() => points.Zip(points.Skip(1), Measurements.Distance).Sum());

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.PolylineBounds(points));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public int Count
            => points.Count;

        #endregion

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
            if (t == 0) return points[0];
            if (t == 1) return points[points.Count - 1];

            var weights = new(double length, double accumulated)[points.Count];
            weights[0] = (0, 0);
            Point2D cursor = points[0];
            double accumulatedLength = 0;

            // Build up the weights map.
            for (int i = 1; i < points.Count; i++)
            {
                double curentLength = Measurements.Distance(cursor, points[i]);
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
                cursor = points[i];
            }

            double accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (int i = points.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated <= accumulatedLengthT)
                {
                    // Interpolate the position.
                    double th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Interpolaters.Linear(points[i], points[i + 1], th);
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
            List<Point2D> outPath = new List<Point2D>(path.points.Count);
            for (int i = 0; i < path.points.Count; i++)
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
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
                return nameof(Polyline);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polyline)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
