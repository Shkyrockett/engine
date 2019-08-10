﻿// <copyright file="Transform.cs" company="Shkyrockett" >
//    Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// The transform2d struct.
    /// </summary>
    public struct Transform2D
        : IMatrix<Transform2D, Vector2D>
    {
        #region Implementations
        /// <summary>
        /// The identity.
        /// </summary>
        public static Transform2D Identity = new Transform2D(0d, 0d, 0d, 0d, 1d, 1d);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Transform2D"/> class.
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
        /// Initializes a new instance of the <see cref="Transform2D"/> class.
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
        /// Gets or sets the <see cref="X"/> coordinate of the location of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute("x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Y"/> coordinate of the location of the <see cref="Transform2D"/>.
        /// </summary>
        [XmlAttribute("y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y { get; set; }

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
        public double SkewX { get; set; }

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
        public double SkewY { get; set; }

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
        public double ScaleX { get; set; }

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
        public double ScaleY { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Rotation"/> angle of the <see cref="Transform2D"/> in Radians.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        //[GeometryAngleRadians]
        [Category("Elements")]
        [Description("The angle of " + nameof(Rotation) + " to rotate the " + nameof(Transform2D) + " in Radians.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        //[TypeConverter(typeof(AngleConverter))]
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
        /// Gets or sets the <see cref="Rotation"/> angle of the <see cref="Transform2D"/> in Degrees.
        /// </summary>
        [XmlAttribute("angle")]
        [Browsable(false)]
        //[GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The angle of " + nameof(Rotation) + " to rotate the " + nameof(Transform2D) + " in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double RotationDegrees { get { return Rotation.ToDegrees(); } set { Rotation = value.ToRadians(); } }

        /// <summary>
        /// Gets or sets the <see cref="Location"/> of the <see cref="Transform2D"/>
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Location) + " of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //[TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location { get { return new Point2D(X, Y); } set { (X, Y) = value; } }

        /// <summary>
        /// Gets or sets the <see cref="SkewY"/> vector of the <see cref="Transform2D"/>
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Skew) + " vector of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //[TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Vector2D Skew { get { return new Vector2D(X, Y); } set { (SkewX, SkewY) = value; } }

        /// <summary>
        /// Gets or sets the <see cref="Scale"/> of the <see cref="Transform2D"/>
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Location) + " location of the " + nameof(Transform2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //[TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Size2D Scale { get { return new Size2D(X, Y); } set { (ScaleX, ScaleY) = value; } }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Add two <see cref="Transform2D"/> structs together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D operator +(Transform2D value, Transform2D addend) => value.Add(addend);

        /// <summary>
        /// Subtract a <see cref="Transform2D"/> struct from another.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D operator -(Transform2D value, Transform2D subend) => value.Subtract(subend);

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Transform2D transform1, Transform2D transform2) => Equals(transform1, transform2);

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Transform2D transform1, Transform2D transform2) => !Equals(transform1, transform2);
        #endregion Operators

        #region Factories
        /// <summary>
        /// The from matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The <see cref="Transform2D"/>.</returns>
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
        /// <returns>The <see cref="Matrix3x2D"/>.</returns>
        public Matrix3x2D ToMatrix()
            => new Matrix3x2D(ScaleX * Cos(SkewY), ScaleX * Sin(SkewY), -ScaleY * Sin(SkewX), ScaleY * Cos(SkewX), X, Y);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
        public IEnumerator<IEnumerable<double>> GetEnumerator() => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
        public bool Equals(Transform2D other) => throw new NotImplementedException();
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
        #endregion Factories

    }
}