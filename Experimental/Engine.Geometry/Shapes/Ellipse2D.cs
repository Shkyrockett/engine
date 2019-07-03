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
    /// The circle struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct Ellipse2D
        : IClosedShape
    {
        /// <summary>
        /// The empty.
        /// </summary>
        public static Ellipse2D Empty = new Ellipse2D(0, 0, 0, 0);

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radiusA">The radius a.</param>
        /// <param name="radiusB">The radius b.</param>
        /// <param name="angle">The angle the <see cref="Ellipse2D"/> is rotated about the center.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D(double x, double y, double radiusA, double radiusB, double angle = 0)
            : this()
        {
            X = x;
            Y = y;
            RadiusA = radiusA;
            RadiusB = radiusB;
            Angle = angle;
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="RadiusA"></param>
        /// <param name="RadiusB"></param>
        /// <param name="Angle"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double RadiusA, out double RadiusB, out double Angle)
        {
            X = this.X;
            Y = this.Y;
            RadiusA = this.RadiusA;
            RadiusB = this.RadiusB;
            Angle = this.Angle;
        }

        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="X">The left.</param>
        /// <param name="Y">The top.</param>
        /// <param name="RadiusA">The radius a.</param>
        /// <param name="RadiusB">The radius b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double RadiusA, out double RadiusB)
        {
            X = this.X;
            Y = this.Y;
            RadiusA = this.RadiusA;
            RadiusB = this.RadiusB;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the center <see cref="X"/> coordinate.
        /// </summary>
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the center <see cref="Y"/> coordinate.
        /// </summary>
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the a radius.
        /// </summary>
        [DataMember(Name = nameof(RadiusA)), XmlAttribute(nameof(RadiusA)), SoapAttribute(nameof(RadiusA))]
        public double RadiusA { get; set; }

        /// <summary>
        /// Gets or sets the b radius.
        /// </summary>
        [DataMember(Name = nameof(RadiusB)), XmlAttribute(nameof(RadiusB)), SoapAttribute(nameof(RadiusB))]
        public double RadiusB { get; set; }

        /// <summary>
        /// Gets or sets the angle the <see cref="Ellipse2D"/> is rotated about the center.
        /// </summary>
        [DataMember(Name = nameof(Angle)), XmlAttribute(nameof(Angle)), SoapAttribute(nameof(Angle))]
        public double Angle { get; set; }
        public double CosAngle { get; internal set; }
        public double SinAngle { get; internal set; }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IShape.PropertyCache { get; set; }
        public Point2D Center { get; internal set; }
        #endregion

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Ellipse2D && Equals(this, (Ellipse2D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Ellipse2D left, Ellipse2D right) => left.X == right.X && left.Y == right.Y && left.RadiusA == right.RadiusA && left.RadiusB == right.RadiusB && left.Angle == right.Angle;

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, RadiusA, RadiusB, Angle);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IShape"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IShape"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            //if (this is null)
            //{
            //    return nameof(Ellipse2D);
            //}

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Ellipse2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(RadiusA)}: {RadiusA.ToString(format, formatProvider)}{sep} {nameof(RadiusB)}: {RadiusB.ToString(format, formatProvider)}{sep} {nameof(Angle)}: {Angle.ToString(format, formatProvider)})";
        }
    }
}
