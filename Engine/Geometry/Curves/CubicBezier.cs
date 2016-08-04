// <copyright file="CubicBezier.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// CubicBezier
    /// </summary>
    /// <structure>Engine.Geometry.CubicBezier2D</structure>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CubicBezier))]
    public class CubicBezier
        : Shape, IOpenShape
    {
        #region Private Fields

        /// <summary>
        /// Position 1.
        /// </summary>
        [XmlAttribute]
        private Point2D a;

        /// <summary>
        /// Tangent 1.
        /// </summary>
        [XmlAttribute]
        private Point2D b;

        /// <summary>
        /// Position 2.
        /// </summary>
        [XmlAttribute]
        private Point2D c;

        /// <summary>
        /// Tangent 2.
        /// </summary>
        [XmlAttribute]
        private Point2D d;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        public CubicBezier()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        public CubicBezier(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        #endregion

        #region Indexers

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public Point2D this[double index]
        //{
        //    get { return Experimental.InterpolateCubicBezier(this, index); }
        //} 

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute]
        public Point2D A
        {
            get { return a; }
            set
            {
                a = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute]
        public Point2D B
        {
            get { return b; }
            set
            {
                b = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute]
        public Point2D C
        {
            get { return c; }
            set
            {
                c = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute]
        public Point2D D
        {
            get { return d; }
            set
            {
                d = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public double Length
            => Perimeters.CubicBezierArcLength(a, b, c, d);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override double Perimeter
            => Length;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [XmlIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                var sortOfCloseLength = (int)Length;
                points = new List<Point2D>(InterpolatePoints(sortOfCloseLength));

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

        #region Interpolations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolaters.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, t));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolaters.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, Length));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="CubicBezier"/> struct based on the format string
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
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(CubicBezier);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CubicBezier)}={{{nameof(A)}={a}{sep}{nameof(B)}={b}{sep}{nameof(C)}={c}{sep}{nameof(D)}={d}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
