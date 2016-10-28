// <copyright file="QuadraticBezier.cs" >
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
    /// QuadraticBezier2D
    /// </summary>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(QuadraticBezier))]
    public class QuadraticBezier
        : Shape, IOpenShape
    {
        #region Private Fields

        /// <summary>
        /// The starting node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlAttribute]
        private Point2D a;

        /// <summary>
        /// The middle tangent control node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlAttribute]
        private Point2D b;

        /// <summary>
        /// The closing node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlAttribute]
        private Point2D c;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points = new List<Point2D>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        public QuadraticBezier()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public QuadraticBezier(Point2D a, Point2D b, Point2D c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the starting node for the <see cref="QuadraticBezier"/> curve.
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
                OnPropertyChanged(nameof(A));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the middle tangent control node for the <see cref="QuadraticBezier"/> curve.
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
                OnPropertyChanged(nameof(B));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the closing node for the <see cref="QuadraticBezier"/> curve.
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
                OnPropertyChanged(nameof(C));
                update?.Invoke();
            }
        }

        /// <summary>
        /// An approximation of the length of a <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlIgnore]
        public double Length
            => Distances.QuadraticBezierArcLengthByIntegral(a, b, c);

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
            => Boundings.QuadraticBezier(a, b, c);

        #endregion

        #region Interpolations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolaters.QuadraticBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, t));

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="QuadraticBezier"/> struct based on the format string
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
            if (this == null) return nameof(QuadraticBezier);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(QuadraticBezier)}={{{nameof(A)}={a}{sep}{nameof(B)}={b}{sep}{nameof(C)}={c}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
