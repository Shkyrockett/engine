// <copyright file="Triangle.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2019 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The triangle class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Triangle))]
    [DebuggerDisplay("{ToString()}")]
    public class Triangle
         : PolygonContour
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        public Triangle()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        public Triangle(Point2D a, Point2D b, Point2D c)
            : base(new List<Point2D> { a, b, c })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Triangle(PolygonContour polygon)
            : base(polygon.Points)
        {
            if (polygon.Points.Count > 3)
            {
                throw new IndexOutOfRangeException();
            }

            if (polygon.Points.Count < 3)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Triangle(Polyline polyline)
            : base(polyline.Points)
        {
            if (polyline.Points.Count > 3)
            {
                throw new IndexOutOfRangeException();
            }

            if (polyline.Points.Count < 3)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Triangle(params Point2D[] points)
            : this(new List<Point2D>(points))
        {
            if (points.Length > 3)
            {
                throw new IndexOutOfRangeException();
            }

            if (points.Length < 3)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Triangle(IEnumerable<Point2D> points)
            : base(points)
        {
            if ((points as List<Point2D>).Count > 3)
            {
                throw new IndexOutOfRangeException();
            }

            if ((points as List<Point2D>).Count < 3)
            {
                throw new IndexOutOfRangeException();
            }
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        public void Deconstruct(out double aX, out double aY, out double bX, out double bY, out double cX, out double cY)
        {
            aX = A.X;
            aY = A.Y;
            bX = B.X;
            bY = B.Y;
            cX = C.X;
            cY = C.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the a.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
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
        /// Gets or sets the b.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
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
        /// Gets or sets the c.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
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
        #endregion Properties

        #region Methods
        /// <summary>
        /// Creates a string representation of this <see cref="PolygonContour"/> struct based on the format string
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
            if (this is null)
            {
                return nameof(Triangle);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Triangle)}{{{string.Join(sep.ToString(provider), Points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
