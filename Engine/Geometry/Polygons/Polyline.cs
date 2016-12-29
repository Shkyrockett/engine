// <copyright file="Polyline.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

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
            => points.Zip(points.Skip(1), Distances.Distance).Sum();

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                double left = points[0].X;
                double top = points[0].Y;
                double right = points[0].X;
                double bottom = points[0].Y;

                foreach (Point2D point in points)
                {
                    // ToDo: Measure performance impact of overwriting each time.
                    left = point.X <= left ? point.X : left;
                    top = point.Y <= top ? point.Y : top;
                    right = point.X >= right ? point.X : right;
                    bottom = point.Y >= bottom ? point.Y : bottom;
                }

                return Rectangle2D.FromLTRB(left, top, right, bottom);
            }
        }

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
