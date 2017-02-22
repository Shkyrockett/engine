// <copyright file="Triangle.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Triangle))]
    public class Triangle
         : Contour, IClosedShape
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
        public Triangle(Contour polygon)
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
        [XmlElement]
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
        [XmlElement]
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
        [XmlElement]
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

        #region Methods

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
