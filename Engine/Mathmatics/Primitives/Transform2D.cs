// <copyright file="Transform.cs" company="Shkyrockett" >
//    Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System;
using System.Globalization;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct Transform2D
        : IFormattable,
        IEquatable<Transform2D>
    {
        #region Implementations

        /// <summary>
        ///
        /// </summary>
        public static Transform2D Identity = new Transform2D(0, 0, 0, 0, 1, 1);

        #endregion

        #region Fields

        /// <summary>
        ///
        /// </summary>
        private double x;

        /// <summary>
        ///
        /// </summary>
        private double y;

        /// <summary>
        ///
        /// </summary>
        private double skewX;

        /// <summary>
        ///
        /// </summary>
        private double skewY;

        /// <summary>
        ///
        /// </summary>
        private double scaleX;

        /// <summary>
        ///
        /// </summary>
        private double scaleY;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="tuple"></param>
        public Transform2D((double x, double y, double skewX, double skewY, double scaleX, double scaleY) tuple)
        {
            (x, y, skewX, skewY, scaleX, scaleY) = tuple;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="skewX"></param>
        /// <param name="skewY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        public Transform2D(double x, double y, double skewX, double skewY, double scaleX, double scaleY)
        {
            this.x = x;
            this.y = y;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="skewX"></param>
        /// <param name="skewY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        public void Deconstruct(out double x, out double y, out double skewX, out double skewY, out double scaleX, out double scaleY)
        {
            x = this.x;
            y = this.y;
            skewX = this.skewX;
            skewY = this.skewY;
            scaleX = this.scaleX;
            scaleY = this.scaleY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="X"/> coordinate of the location of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute(nameof(x))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X { get { return x; } set { x = value; } }

        /// <summary>
        /// Gets or sets the <see cref="Y"/> coordinate of the location of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute(nameof(y))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y { get { return y; } set { y = value; } }

        /// <summary>
        /// Gets or sets the horizontal skew value of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute("skew-x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The x skew of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SkewX { get { return skewX; } set { skewX = value; } }

        /// <summary>
        /// Gets or sets the vertical skew value of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute("skew-y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y skew of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SkewY { get { return skewY; } set { skewY = value; } }

        /// <summary>
        /// Gets or sets the horizontal scale of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute("scale-x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The x scale of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double ScaleX { get { return scaleX; } set { scaleX = value; } }

        /// <summary>
        /// Gets or sets the vertical scale of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute("scale-y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y scale of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double ScaleY { get { return scaleY; } set { scaleY = value; } }

        /// <summary>
        /// Gets or sets the <see cref="Rotation"/> angle of the <see cref="Transform2D"/> in Radians.
        /// </summary>
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
            get { return skewY; }
            set
            {
                var delta = value - skewY;
                skewX += delta;
                skewY += delta;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Rotation"/> angle of the <see cref="Transform2D"/> in Degrees.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("angle")]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The angle of " + nameof(Rotation) + " to rotate the " + nameof(Transform2D) + " in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double RotationDegrees
        {
            get { return Rotation.ToDegrees(); }
            set { Rotation = value.ToRadians(); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Location"/> of the <see cref="Transform2D"/>
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Location) + " of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="skewY"/> vector of the <see cref="Transform2D"/>
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Skew) + " vector of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Vector2D Skew
        {
            get { return new Vector2D(x, y); }
            set
            {
                skewX = value.I;
                skewY = value.J;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Scale"/> of the <see cref="Transform2D"/>
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Location) + " location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Size2D Scale
        {
            get { return new Size2D(x, y); }
            set
            {
                scaleX = value.Width;
                scaleY = value.Height;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Add two <see cref="Transform2D"/> structs together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D operator +(Transform2D value, Transform2D addend)
            => value.Add(addend);

        /// <summary>
        /// Subtract a <see cref="Transform2D"/> struct from another.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D operator -(Transform2D value, Transform2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Compares two <see cref="Transform2D"/> instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two <see cref="Transform2D"/> instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='transform1'>The first <see cref="Transform2D"/> to compare</param>
        /// <param name='transform2'>The second <see cref="Transform2D"/> to compare</param>
        public static bool operator ==(Transform2D transform1, Transform2D transform2)
            => Equals(transform1, transform2);

        /// <summary>
        /// Compares two <see cref="Transform2D"/> instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two <see cref="Transform2D"/> instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='transform1'>The first <see cref="Transform2D"/> to compare</param>
        /// <param name='transform2'>The second <see cref="Transform2D"/> to compare</param>
        public static bool operator !=(Transform2D transform1, Transform2D transform2)
            => !Equals(transform1, transform2);

        #endregion

        #region Factories

        /// <summary>
        ///
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Transform2D FromMatrix(Matrix3x2D matrix)
        {
            const int backupScaleX = 0;//scaleX;
            const int backupScaleY = 0;// scaleY;

            var skewX = Atan(-matrix.M21 / matrix.M22);
            var skewY = Atan(matrix.M12 / matrix.M11);
            if (double.IsNaN(skewX)) skewX = 0d;
            if (double.IsNaN(skewY)) skewY = 0d;

            var scaleY = (skewX > -Quart && skewX < Quart) ? matrix.M22 / Cos(skewX) : -matrix.M21 / Sin(skewX);
            var scaleX = (skewY > -Quart && skewY < Quart) ? matrix.M11 / Cos(skewY) : matrix.M12 / Sin(skewY);

            if (backupScaleX >= 0d && scaleX < 0d)
            {
                scaleX = -scaleX;
                skewY = skewY - PI;
            }

            if (backupScaleY >= 0d && scaleY < 0d)
            {
                scaleY = -scaleY;
                skewX = skewX - PI;
            }

            return new Transform2D(matrix.OffsetX, matrix.OffsetY, skewX, skewY, scaleX, scaleY);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Matrix3x2D ToMatrix()
            => new Matrix3x2D(ScaleX * Cos(SkewY), ScaleX * Sin(SkewY), -ScaleY * Sin(SkewX), ScaleY * Cos(SkewX), X, Y);

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => X.GetHashCode()
            ^ Y.GetHashCode()
            ^ skewX.GetHashCode()
            ^ SkewY.GetHashCode()
            ^ scaleX.GetHashCode()
            ^ ScaleY.GetHashCode();

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Transform2D a, Transform2D b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Transform2D a, Transform2D b)
            => a.X == b.X & a.Y == b.Y & a.skewX == b.skewX & a.SkewY == b.SkewY & a.scaleX == b.scaleX & a.scaleY == b.scaleY;

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Transform2D && Equals(this, (Transform2D)obj);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Transform2D value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector4D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            // Capture the culture's list separator character.
            var sep = Tokenizer.GetNumericListSeparator(provider);

            // Create the string representation of the struct.
            return $"{nameof(Transform2D)}{{{nameof(X)}={X.ToString(format, provider)}{sep}{nameof(Y)}={Y.ToString(format, provider)}{sep}{nameof(SkewX)}={SkewX.ToString(format, provider)}{sep}{nameof(SkewY)}={SkewY.ToString(format, provider)}{sep}{nameof(ScaleX)}={ScaleX.ToString(format, provider)}{sep}{nameof(ScaleY)}={ScaleY.ToString(format, provider)}}}";
        }

        #endregion
    }
}
