// <copyright file="Triangle.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Triangle))]
    public class Triangle
         : Polygon, IClosedShape
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Triangle()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Triangle(Point2D a, Point2D b, Point2D c)
            : base(new List<Point2D> { a, b, c })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public Triangle(Polygon polygon)
            : base(polygon.Points)
        {
            if (polygon.Points.Count > 3) throw new IndexOutOfRangeException();
            if (polygon.Points.Count < 3) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        public Triangle(Polyline polyline)
            : base(polyline.Points)
        {
            if (polyline.Points.Count > 3) throw new IndexOutOfRangeException();
            if (polyline.Points.Count < 3) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Triangle(params Point2D[] points)
            : this(new List<Point2D>(points))
        {
            if (points.Length > 3) throw new IndexOutOfRangeException();
            if (points.Length < 3) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Triangle(IEnumerable<Point2D> points)
            : base(points)
        {
            if ((points as List<Point2D>).Count > 3) throw new IndexOutOfRangeException();
            if ((points as List<Point2D>).Count < 3) throw new IndexOutOfRangeException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D A
        {
            get { return Points[0]; }
            set
            {
                Points[0] = value;
                OnPropertyChanged(nameof(A));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D B
        {
            get { return Points[1]; }
            set
            {
                Points[1] = value;
                OnPropertyChanged(nameof(B));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D C
        {
            get { return Points[2]; }
            set
            {
                Points[2] = value;
                OnPropertyChanged(nameof(C));
                update?.Invoke();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Polygon"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Triangle);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Triangle)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
