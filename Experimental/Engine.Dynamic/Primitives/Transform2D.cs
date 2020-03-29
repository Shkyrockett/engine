// <copyright file="Transform.cs" company="Shkyrockett" >
//    Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Mathematics;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The transform2d struct.
    /// </summary>
    /// <seealso cref="IPrimitive" />
    /// <seealso cref="IEquatable{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Transform2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Transform2D
        : IPrimitive, IMatrix<Transform2D, Vector2D>,
        IEquatable<Transform2D>
    {
        #region Implementations
        /// <summary>
        /// The empty
        /// </summary>
        public static readonly Transform2D Empty = new Transform2D(0d, 0d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// The na n
        /// </summary>
        public static readonly Transform2D NaN = new Transform2D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// The identity.
        /// </summary>
        public static readonly Transform2D Identity = new Transform2D(0d, 0d, 0d, 0d, 1d, 1d);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Transform2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Transform2D((double x, double y, double skewX, double skewY, double scaleX, double scaleY) tuple)
            : this()
        {
            (X, Y, SkewX, SkewY, ScaleX, ScaleY) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform2D" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="skewX">The skewX.</param>
        /// <param name="skewY">The skewY.</param>
        /// <param name="scaleX">The scaleX.</param>
        /// <param name="scaleY">The scaleY.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Transform2D(double x, double y, double skewX, double skewY, double scaleX, double scaleY)
            : this()
        {
            X = x;
            Y = y;
            SkewX = skewX;
            SkewY = skewY;
            ScaleX = scaleX;
            ScaleY = scaleY;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="skewX">The skewX.</param>
        /// <param name="skewY">The skewY.</param>
        /// <param name="scaleX">The scaleX.</param>
        /// <param name="scaleY">The scaleY.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out double x, out double y, out double skewX, out double skewY, out double scaleX, out double scaleY)
        {
            x = X;
            y = Y;
            skewX = SkewX;
            skewY = SkewY;
            scaleX = ScaleX;
            scaleY = ScaleY;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="X" /> coordinate of the location of the <see cref="Transform2D" />.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [XmlAttribute("x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Y" /> coordinate of the location of the <see cref="Transform2D" />.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [XmlAttribute("y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the horizontal skew value of the <see cref="Transform2D" />.
        /// </summary>
        /// <value>
        /// The skew x.
        /// </value>
        [XmlAttribute("skew-x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The x skew of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SkewX { get; set; }

        /// <summary>
        /// Gets or sets the vertical skew value of the <see cref="Transform2D" />.
        /// </summary>
        /// <value>
        /// The skew y.
        /// </value>
        [XmlAttribute("skew-y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y skew of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SkewY { get; set; }

        /// <summary>
        /// Gets or sets the horizontal scale of the <see cref="Transform2D" />.
        /// </summary>
        /// <value>
        /// The scale x.
        /// </value>
        [XmlAttribute("scale-x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The x scale of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double ScaleX { get; set; }

        /// <summary>
        /// Gets or sets the vertical scale of the <see cref="Transform2D" />.
        /// </summary>
        /// <value>
        /// The scale y.
        /// </value>
        [XmlAttribute("scale-y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y scale of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double ScaleY { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Rotation" /> angle of the <see cref="Transform2D" /> in Radians.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The angle of " + nameof(Rotation) + " to rotate the " + nameof(Transform2D) + " in Radians.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double Rotation
        {
            get { return SkewY; }
            set
            {
                var delta = value - SkewY;
                SkewX += delta;
                SkewY += delta;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Rotation" /> angle of the <see cref="Transform2D" /> in Degrees.
        /// </summary>
        /// <value>
        /// The rotation degrees.
        /// </value>
        [XmlAttribute("angle")]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The angle of " + nameof(Rotation) + " to rotate the " + nameof(Transform2D) + " in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double RotationDegrees { get { return Rotation.RadiansToDegrees(); } set { Rotation = value.DegreesToRadians(); } }

        /// <summary>
        /// Gets or sets the <see cref="Location" /> of the <see cref="Transform2D" />
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Location) + " of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location { get { return new Point2D(X, Y); } set { (X, Y) = value; } }

        /// <summary>
        /// Gets or sets the <see cref="SkewY" /> vector of the <see cref="Transform2D" />
        /// </summary>
        /// <value>
        /// The skew.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Skew) + " vector of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Vector2D Skew { get { return new Vector2D(X, Y); } set { (SkewX, SkewY) = value; } }

        /// <summary>
        /// Gets or sets the <see cref="Scale" /> of the <see cref="Transform2D" />
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Location) + " location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Size2D Scale { get { return new Size2D(X, Y); } set { (ScaleX, ScaleY) = value; } }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Add two <see cref="Transform2D" /> structs together.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D operator +(Transform2D augend, Transform2D addend) => Add(augend, addend);

        /// <summary>
        /// Subtract a <see cref="Transform2D" /> struct from another.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D operator -(Transform2D minuend, Transform2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Compares two <see cref="Transform2D" /> instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <param name="transform1">The first <see cref="Transform2D" /> to compare</param>
        /// <param name="transform2">The second <see cref="Transform2D" /> to compare</param>
        /// <returns>
        /// bool - true if the two <see cref="Transform2D" /> instances are exactly equal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Transform2D transform1, Transform2D transform2) => Equals(transform1, transform2);

        /// <summary>
        /// Compares two <see cref="Transform2D" /> instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <param name="transform1">The first <see cref="Transform2D" /> to compare</param>
        /// <param name="transform2">The second <see cref="Transform2D" /> to compare</param>
        /// <returns>
        /// bool - true if the two <see cref="Transform2D" /> instances are exactly unequal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Transform2D transform1, Transform2D transform2) => !Equals(transform1, transform2);

        /// <summary>
        /// Performs an implicit conversion from <see cref="ValueTuple{T, T, T, T, T, T}" /> to <see cref="Transform2D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Transform2D((double X, double Y, double SkewX, double SkewY, double ScaleX, double ScaleY) tuple) => FromValueTuple(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D Add(Transform2D augend, Transform2D addend) => Operations.AddTransformMatrix(augend.X, augend.Y, augend.SkewX, augend.SkewY, augend.ScaleX, augend.ScaleY, addend.X, addend.Y, addend.SkewX, addend.SkewY, addend.ScaleX, addend.ScaleY);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D Subtract(Transform2D minuend, Transform2D subend) => Operations.SubtractTransformMatrix(minuend.X, minuend.Y, minuend.SkewX, minuend.SkewY, minuend.ScaleX, minuend.ScaleY, subend.X, subend.Y, subend.SkewX, subend.SkewY, subend.ScaleX, subend.ScaleY);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Ellipse2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Transform2D other) => X == other.X && Y == other.Y && SkewX == other.SkewX && SkewY == other.SkewY && ScaleX == other.ScaleX && ScaleY == other.ScaleY;

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D FromValueTuple((double X, double Y, double SkewX, double SkewY, double ScaleX, double ScaleY) tuple) => new Transform2D(tuple);
        #endregion

        #region Factories
        /// <summary>
        /// The from matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        /// The <see cref="Transform2D" />.
        /// </returns>
        public static Transform2D FromMatrix(Matrix3x2D matrix)
        {
            const int backupScaleX = 0;//scaleX;
            const int backupScaleY = 0;// scaleY;

            var skewX = Atan(-matrix.M21 / matrix.M22);
            var skewY = Atan(matrix.M12 / matrix.M11);
            if (double.IsNaN(skewX))
            {
                skewX = 0d;
            }

            if (double.IsNaN(skewY))
            {
                skewY = 0d;
            }

            var scaleY = (skewX > -Quart && skewX < Quart) ? matrix.M22 / Cos(skewX) : -matrix.M21 / Sin(skewX);
            var scaleX = (skewY > -Quart && skewY < Quart) ? matrix.M11 / Cos(skewY) : matrix.M12 / Sin(skewY);

            if (backupScaleX >= 0d && scaleX < 0d)
            {
                scaleX = -scaleX;
                skewY -= PI;
            }

            if (backupScaleY >= 0d && scaleY < 0d)
            {
                scaleY = -scaleY;
                skewX -= PI;
            }

            return new Transform2D(matrix.OffsetX, matrix.OffsetY, skewX, skewY, scaleX, scaleY);
        }

        /// <summary>
        /// The to matrix.
        /// </summary>
        /// <returns>
        /// The <see cref="Matrix3x2D" />.
        /// </returns>
        public Matrix3x2D ToMatrix() => new Matrix3x2D(ScaleX * Cos(SkewY), ScaleX * Sin(SkewY), -ScaleY * Sin(SkewX), ScaleY * Cos(SkewX), X, Y);
        #endregion Factories

        #region Standard Methods
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<IEnumerable<double>> GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, SkewX, SkewY, ScaleX, ScaleY);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Transform2D" />.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Transform2D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Transform2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Transform2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Point2D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Transform2D)}({nameof(X)}:{X.ToString(format, provider)}{s} {nameof(Y)}:{Y.ToString(format, provider)}{s} {nameof(SkewX)}:{SkewX.ToString(format, provider)}{s} {nameof(SkewY)}:{SkewY.ToString(format, provider)}{s} {nameof(ScaleX)}:{ScaleX.ToString(format, provider)}{s} {nameof(ScaleY)}:{ScaleY.ToString(format, provider)})";
        }
        #endregion
    }
}
