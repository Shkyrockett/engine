using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct Triangle2D
        : IClosedShape
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(Point2D a, Point2D b, Point2D c)
            : this()
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(double aX, double aY, double bX, double bY, double cX, double cY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY), new Point2D(cX, cY))
        { }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Triangle2D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out Point2D A, out Point2D B, out Point2D C)
        {
            A = this.A;
            B = this.B;
            C = this.C;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = nameof(A)), XmlElement(nameof(A)), SoapElement(nameof(A))]
        public Point2D A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = nameof(B)), XmlElement(nameof(B)), SoapElement(nameof(B))]
        public Point2D B { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = nameof(C)), XmlElement(nameof(C)), SoapElement(nameof(C))]
        public Point2D C { get; set; }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        //[Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IShape.PropertyCache { get; set; }
        #endregion

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point2D point) => false;// PolygonContourContainsPointTests.PolygonContourContainsPoint(new List<(double X, double Y)> { A, B, C }, point.X, point.Y);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IShape"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IShape"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            //if (this is null)
            //{
            //    return nameof(Triangle2D);
            //}

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Triangle2D)}({string.Join(sep, new[] { A.ToString(format, formatProvider), B.ToString(format, formatProvider), C.ToString(format, formatProvider) })})";
        }
    }
}
