// <copyright file="Matrix5x5D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
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
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The Matrix5x5D struct.
    /// </summary>
    /// <seealso cref="IMatrix{M, V}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Matrix5x5DConverter))]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public struct Matrix5x5D
        : IMatrix<Matrix5x5D, Vector5D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix5x5D" />.
        /// </summary>
        public static readonly Matrix5x5D Empty = new Matrix5x5D(
            0d, 0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// An Identity <see cref="Matrix5x5D" />.
        /// </summary>
        public static readonly Matrix5x5D Identity = new Matrix5x5D(
            1d, 0d, 0d, 0d, 0d,
            0d, 1d, 0d, 0d, 0d,
            0d, 0d, 1d, 0d, 0d,
            0d, 0d, 0d, 1d, 0d,
            0d, 0d, 0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix5x5D" /> class.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix5x5D(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4)
            : this()
        {
            (
                M0x0, M0x1, M0x2, M0x3, M0x4,
                M1x0, M1x1, M1x2, M1x3, M1x4,
                M2x0, M2x1, M2x2, M2x3, M2x4,
                M3x0, M3x1, M3x2, M3x3, M3x4,
                M4x0, M4x1, M4x2, M4x3, M4x4
            ) = (
                m0x0, m0x1, m0x2, m0x3, m0x4,
                m1x0, m1x1, m1x2, m1x3, m1x4,
                m2x0, m2x1, m2x2, m2x3, m2x4,
                m3x0, m3x1, m3x2, m3x3, m3x4,
                m4x0, m4x1, m4x2, m4x3, m4x4
            );
        }

        /// <summary>
        /// Create a new Matrix from 2 Vector5D objects.
        /// </summary>
        /// <param name="xAxis">The x axis.</param>
        /// <param name="yAxis">The y axis.</param>
        /// <param name="zAxis">The z axis.</param>
        /// <param name="wAxis">The w axis.</param>
        /// <param name="vAxis">The v axis.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix5x5D(Vector5D xAxis, Vector5D yAxis, Vector5D zAxis, Vector5D wAxis, Vector5D vAxis)
            : this(xAxis.I, xAxis.J, xAxis.K, xAxis.L, xAxis.M,
                   yAxis.I, yAxis.J, yAxis.K, yAxis.L, yAxis.M,
                   zAxis.I, zAxis.J, zAxis.K, zAxis.L, zAxis.M,
                   wAxis.I, wAxis.J, wAxis.K, wAxis.L, wAxis.M,
                   vAxis.I, vAxis.J, vAxis.K, vAxis.L, vAxis.M)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix5x5D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix5x5D((
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4) tuple)
            : this()
        {
            (M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Matrix2x2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}" />.
        /// </summary>
        /// <param name="m0x0">The m0x0.</param>
        /// <param name="m0x1">The m0x1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m1x0">The m1x0.</param>
        /// <param name="m1x1">The m1x1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(
            out double m0x0, out double m0x1, out double m0x2, out double m0x3, out double m0x4,
            out double m1x0, out double m1x1, out double m1x2, out double m1x3, out double m1x4,
            out double m2x0, out double m2x1, out double m2x2, out double m2x3, out double m2x4,
            out double m3x0, out double m3x1, out double m3x2, out double m3x3, out double m3x4,
            out double m4x0, out double m4x1, out double m4x2, out double m4x3, out double m4x4) => (
                m0x0, m0x1, m0x2, m0x3, m0x4,
                m1x0, m1x1, m1x2, m1x3, m1x4,
                m2x0, m2x1, m2x2, m2x3, m2x4,
                m3x0, m3x1, m3x2, m3x3, m3x4,
                m4x0, m4x1, m4x2, m4x3, m4x4
            ) = (
                M0x0, M0x1, M0x2, M0x3, M0x4,
                M1x0, M1x1, M1x2, M1x3, M1x4,
                M2x0, M2x1, M2x2, M2x3, M2x4,
                M3x0, M3x1, M3x2, M3x3, M3x4,
                M4x0, M4x1, M4x2, M4x3, M4x4
            );
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="double"/> with the specified index1.
        /// </summary>
        /// <value>
        /// The <see cref="double"/>.
        /// </value>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        /// <returns></returns>
        public double this[int index1, int index2]
        {
            get
            {
                return index1 switch
                {
                    0 => index2 switch
                    {
                        0 => M0x0,
                        1 => M0x1,
                        2 => M0x2,
                        3 => M0x3,
                        4 => M0x4,
                        _ => double.NaN,
                    },
                    1 => index2 switch
                    {
                        0 => M1x0,
                        1 => M1x1,
                        2 => M1x2,
                        3 => M1x3,
                        4 => M1x4,
                        _ => double.NaN,
                    },
                    2 => index2 switch
                    {
                        0 => M2x0,
                        1 => M2x1,
                        2 => M2x2,
                        3 => M2x3,
                        4 => M2x4,
                        _ => double.NaN,
                    },
                    3 => index2 switch
                    {
                        0 => M3x0,
                        1 => M3x1,
                        2 => M3x2,
                        3 => M3x3,
                        4 => M3x4,
                        _ => double.NaN,
                    },
                    4 => index2 switch
                    {
                        0 => M4x0,
                        1 => M4x1,
                        2 => M4x2,
                        3 => M4x3,
                        4 => M4x4,
                        _ => double.NaN,
                    },
                    _ => double.NaN,
                };
            }
            set
            {
                switch (index1)
                {
                    case 0:
                        switch (index2)
                        {
                            case 0: M0x0 = value; break;
                            case 1: M0x1 = value; break;
                            case 2: M0x2 = value; break;
                            case 3: M0x3 = value; break;
                            case 4: M0x4 = value; break;
                            default: break;
                        }
                        break;
                    case 1:
                        switch (index2)
                        {
                            case 0: M1x0 = value; break;
                            case 1: M1x1 = value; break;
                            case 2: M1x2 = value; break;
                            case 3: M1x3 = value; break;
                            case 4: M1x4 = value; break;
                            default: break;
                        }
                        break;
                    case 2:
                        switch (index2)
                        {
                            case 0: M2x0 = value; break;
                            case 1: M2x1 = value; break;
                            case 2: M2x2 = value; break;
                            case 3: M2x3 = value; break;
                            case 4: M2x4 = value; break;
                            default: break;
                        }
                        break;
                    case 4:
                        switch (index2)
                        {
                            case 0: M4x0 = value; break;
                            case 1: M4x1 = value; break;
                            case 2: M4x2 = value; break;
                            case 3: M4x3 = value; break;
                            case 4: M4x4 = value; break;
                            default: break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the m0x0.
        /// </summary>
        /// <value>
        /// The M0X0.
        /// </value>
        [DataMember(Name = nameof(M0x0)), XmlAttribute(nameof(M0x0)), SoapAttribute(nameof(M0x0))]
        public double M0x0 { get; set; }

        /// <summary>
        /// Gets or sets the m0x1.
        /// </summary>
        /// <value>
        /// The M0X1.
        /// </value>
        [DataMember(Name = nameof(M0x1)), XmlAttribute(nameof(M0x1)), SoapAttribute(nameof(M0x1))]
        public double M0x1 { get; set; }

        /// <summary>
        /// Gets or sets the m0x2.
        /// </summary>
        /// <value>
        /// The M0X2.
        /// </value>
        [DataMember(Name = nameof(M0x2)), XmlAttribute(nameof(M0x2)), SoapAttribute(nameof(M0x2))]
        public double M0x2 { get; set; }

        /// <summary>
        /// Gets or sets the m0x3.
        /// </summary>
        /// <value>
        /// The M0X3.
        /// </value>
        [DataMember(Name = nameof(M0x3)), XmlAttribute(nameof(M0x3)), SoapAttribute(nameof(M0x3))]
        public double M0x3 { get; set; }

        /// <summary>
        /// Gets the M0X4.
        /// </summary>
        /// <value>
        /// The M0X4.
        /// </value>
        [DataMember(Name = nameof(M0x4)), XmlAttribute(nameof(M0x4)), SoapAttribute(nameof(M0x4))]
        public double M0x4 { get; set; }

        /// <summary>
        /// Gets or sets the m1x0.
        /// </summary>
        /// <value>
        /// The M1X0.
        /// </value>
        [DataMember(Name = nameof(M1x0)), XmlAttribute(nameof(M1x0)), SoapAttribute(nameof(M1x0))]
        public double M1x0 { get; set; }

        /// <summary>
        /// Gets or sets the m1x1.
        /// </summary>
        /// <value>
        /// The M1X1.
        /// </value>
        [DataMember(Name = nameof(M1x1)), XmlAttribute(nameof(M1x1)), SoapAttribute(nameof(M1x1))]
        public double M1x1 { get; set; }

        /// <summary>
        /// Gets or sets the m1x2.
        /// </summary>
        /// <value>
        /// The M1X2.
        /// </value>
        [DataMember(Name = nameof(M1x2)), XmlAttribute(nameof(M1x2)), SoapAttribute(nameof(M1x2))]
        public double M1x2 { get; set; }

        /// <summary>
        /// Gets or sets the m1x3.
        /// </summary>
        /// <value>
        /// The M1X3.
        /// </value>
        [DataMember(Name = nameof(M1x3)), XmlAttribute(nameof(M1x3)), SoapAttribute(nameof(M1x3))]
        public double M1x3 { get; set; }

        /// <summary>
        /// Gets or sets the M1X4.
        /// </summary>
        /// <value>
        /// The M1X4.
        /// </value>
        [DataMember(Name = nameof(M1x4)), XmlAttribute(nameof(M1x4)), SoapAttribute(nameof(M1x4))]
        public double M1x4 { get; set; }

        /// <summary>
        /// Gets or sets the m2x0.
        /// </summary>
        /// <value>
        /// The M2X0.
        /// </value>
        [DataMember(Name = nameof(M2x0)), XmlAttribute(nameof(M2x0)), SoapAttribute(nameof(M2x0))]
        public double M2x0 { get; set; }

        /// <summary>
        /// Gets or sets the m2x1.
        /// </summary>
        /// <value>
        /// The M2X1.
        /// </value>
        [DataMember(Name = nameof(M2x1)), XmlAttribute(nameof(M2x1)), SoapAttribute(nameof(M2x1))]
        public double M2x1 { get; set; }

        /// <summary>
        /// Gets or sets the m2x2.
        /// </summary>
        /// <value>
        /// The M2X2.
        /// </value>
        [DataMember(Name = nameof(M2x2)), XmlAttribute(nameof(M2x2)), SoapAttribute(nameof(M2x2))]
        public double M2x2 { get; set; }

        /// <summary>
        /// Gets or sets the m2x3.
        /// </summary>
        /// <value>
        /// The M2X3.
        /// </value>
        [DataMember(Name = nameof(M2x3)), XmlAttribute(nameof(M2x3)), SoapAttribute(nameof(M2x3))]
        public double M2x3 { get; set; }

        /// <summary>
        /// Gets or sets the M2X4.
        /// </summary>
        /// <value>
        /// The M2X4.
        /// </value>
        [DataMember(Name = nameof(M2x4)), XmlAttribute(nameof(M2x4)), SoapAttribute(nameof(M2x4))]
        public double M2x4 { get; set; }

        /// <summary>
        /// Gets or sets the m3x0.
        /// </summary>
        /// <value>
        /// The M3X0.
        /// </value>
        [DataMember(Name = nameof(M3x0)), XmlAttribute(nameof(M3x0)), SoapAttribute(nameof(M3x0))]
        public double M3x0 { get; set; }

        /// <summary>
        /// Gets or sets the m3x1.
        /// </summary>
        /// <value>
        /// The M3X1.
        /// </value>
        [DataMember(Name = nameof(M3x1)), XmlAttribute(nameof(M3x1)), SoapAttribute(nameof(M3x1))]
        public double M3x1 { get; set; }

        /// <summary>
        /// Gets or sets the m3x2.
        /// </summary>
        /// <value>
        /// The M3X2.
        /// </value>
        [DataMember(Name = nameof(M3x2)), XmlAttribute(nameof(M3x2)), SoapAttribute(nameof(M3x2))]
        public double M3x2 { get; set; }

        /// <summary>
        /// Gets or sets the m3x3.
        /// </summary>
        /// <value>
        /// The M3X3.
        /// </value>
        [DataMember(Name = nameof(M3x3)), XmlAttribute(nameof(M3x3)), SoapAttribute(nameof(M3x3))]
        public double M3x3 { get; set; }

        /// <summary>
        /// Gets or sets the M3X4.
        /// </summary>
        /// <value>
        /// The M3X4.
        /// </value>
        [DataMember(Name = nameof(M3x4)), XmlAttribute(nameof(M3x4)), SoapAttribute(nameof(M3x4))]
        public double M3x4 { get; set; }

        /// <summary>
        /// Gets or sets the M4X0.
        /// </summary>
        /// <value>
        /// The M4X0.
        /// </value>
        [DataMember(Name = nameof(M4x0)), XmlAttribute(nameof(M4x0)), SoapAttribute(nameof(M4x0))]
        public double M4x0 { get; set; }

        /// <summary>
        /// Gets or sets the M4X1.
        /// </summary>
        /// <value>
        /// The M4X1.
        /// </value>
        [DataMember(Name = nameof(M4x1)), XmlAttribute(nameof(M4x1)), SoapAttribute(nameof(M4x1))]
        public double M4x1 { get; set; }

        /// <summary>
        /// Gets or sets the M4X2.
        /// </summary>
        /// <value>
        /// The M4X2.
        /// </value>
        [DataMember(Name = nameof(M4x2)), XmlAttribute(nameof(M4x2)), SoapAttribute(nameof(M4x2))]
        public double M4x2 { get; set; }

        /// <summary>
        /// Gets or sets the M4X3.
        /// </summary>
        /// <value>
        /// The M4X3.
        /// </value>
        [DataMember(Name = nameof(M4x3)), XmlAttribute(nameof(M4x3)), SoapAttribute(nameof(M4x3))]
        public double M4x3 { get; set; }

        /// <summary>
        /// Gets or sets the M4X4.
        /// </summary>
        /// <value>
        /// The M4X4.
        /// </value>
        [DataMember(Name = nameof(M4x4)), XmlAttribute(nameof(M4x4)), SoapAttribute(nameof(M4x4))]
        public double M4x4 { get; set; }

        /// <summary>
        /// Gets or sets the cx.
        /// </summary>
        /// <value>
        /// The cx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First column of the " + nameof(Matrix5x5D))]
        public Vector5D Cx { get { return new Vector5D(M0x0, M1x0, M2x0, M3x0, M4x0); } set { (M0x0, M1x0, M2x0, M3x0, M4x0) = value; } }

        /// <summary>
        /// Gets or sets the cy.
        /// </summary>
        /// <value>
        /// The cy.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second column of the " + nameof(Matrix5x5D))]
        public Vector5D Cy { get { return new Vector5D(M0x1, M1x1, M2x1, M3x1, M4x1); } set { (M0x1, M1x1, M2x1, M3x1, M4x1) = value; } }

        /// <summary>
        /// Gets or sets the cz.
        /// </summary>
        /// <value>
        /// The cz.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third column of the " + nameof(Matrix5x5D))]
        public Vector5D Cz { get { return new Vector5D(M0x2, M1x2, M2x2, M3x2, M4x2); } set { (M0x2, M1x2, M2x2, M3x2, M4x2) = value; } }

        /// <summary>
        /// Gets or sets the cw.
        /// </summary>
        /// <value>
        /// The cw.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Fourth column of the " + nameof(Matrix5x5D))]
        public Vector5D Cw { get { return new Vector5D(M0x3, M1x3, M2x3, M3x3, M4x3); } set { (M0x3, M1x3, M2x3, M3x3, M4x3) = value; } }

        /// <summary>
        /// Gets or sets the cv.
        /// </summary>
        /// <value>
        /// The cv.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Fifth column of the " + nameof(Matrix5x5D))]
        public Vector5D CV { get { return new Vector5D(M0x4, M1x4, M2x4, M3x4, M4x4); } set { (M0x4, M1x4, M2x4, M3x4, M4x4) = value; } }

        /// <summary>
        /// Gets or sets the X Row or row one.
        /// </summary>
        /// <value>
        /// The rx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix5x5D))]
        public Vector5D Rx { get { return new Vector5D(M0x0, M0x1, M0x2, M0x3, M0x4); } set { (M0x0, M0x1, M0x2, M0x3, M0x4) = value; } }

        /// <summary>
        /// Gets or sets the Y Row or row two.
        /// </summary>
        /// <value>
        /// The ry.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix5x5D))]
        public Vector5D Ry { get { return new Vector5D(M1x0, M1x1, M1x2, M1x3, M1x4); } set { (M1x0, M1x1, M1x2, M1x3, M1x4) = value; } }

        /// <summary>
        /// Gets or sets the Z Row or row tree.
        /// </summary>
        /// <value>
        /// The rz.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix5x5D))]
        public Vector5D Rz { get { return new Vector5D(M2x0, M2x1, M2x2, M2x3, M2x4); } set { (M2x0, M2x1, M2x2, M2x3, M2x4) = value; } }

        /// <summary>
        /// Gets or sets the W Row or row four.
        /// </summary>
        /// <value>
        /// The rw.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Fourth row of the " + nameof(Matrix5x5D))]
        public Vector5D Rw { get { return new Vector5D(M3x0, M3x1, M3x2, M3x3, M3x4); } set { (M3x0, M3x1, M3x2, M3x3, M3x4) = value; } }

        /// <summary>
        /// Gets or sets the V Row or row five.
        /// </summary>
        /// <value>
        /// The rv.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Fifth row of the " + nameof(Matrix5x5D))]
        public Vector5D Rv { get { return new Vector5D(M4x0, M4x1, M4x2, M4x3, M4x4); } set { (M4x0, M4x1, M4x2, M4x3, M4x4) = value; } }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        /// <value>
        /// The determinant.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant => MatrixDeterminant(
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Gets the transposed matrix where the rows of the matrix are swapped with the columns.
        /// </summary>
        /// <value>
        /// The transposed.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix5x5D Transposed => TransposeMatrix(
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        /// <value>
        /// The adjoint.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix5x5D Adjoint => AdjointMatrix(
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        /// <value>
        /// The cofactor.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix5x5D Cofactor => CofactorMatrix(
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Gets the inverted.
        /// </summary>
        /// <value>
        /// The inverted.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix5x5D Inverted => InverseMatrix(
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Gets a value indicating whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is identity; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity => IsMatrixIdentity(
                M0x0, M0x1, M0x2, M0x3, M0x4,
                M1x0, M1x1, M1x2, M1x3, M1x4,
                M2x0, M2x1, M2x2, M2x3, M2x4,
                M3x0, M3x1, M3x2, M3x3, M3x4,
                M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Rows => 5;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Columns => 5;

        /// <summary>
        /// Gets the number of cells in the Matrix.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count => Rows * Columns;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator +(Matrix5x5D value) => Plus(value);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator +(Matrix5x5D augend, Matrix5x5D addend) => Add(augend, addend);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator -(Matrix5x5D value) => Negate(value);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator -(Matrix5x5D minuend, Matrix5x5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(Matrix5x5D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(double multiplicand, Matrix5x5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator *(Matrix5x5D multiplicand, Vector5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator *(Vector5D multiplicand, Matrix5x5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(Matrix5x5D multiplicand, Matrix5x5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(Matrix5x5D multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(Matrix3x3D multiplicand, Matrix5x5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(Matrix5x5D multiplicand, Matrix2x2D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D operator *(Matrix2x2D multiplicand, Matrix5x5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Compares two Matrix instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <param name="matrix1">The first Matrix to compare</param>
        /// <param name="matrix2">The second Matrix to compare</param>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Matrix5x5D matrix1, Matrix5x5D matrix2) => Equals(matrix1, matrix2);

        /// <summary>
        /// Compares two Matrix instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <param name="matrix1">The first Matrix to compare</param>
        /// <param name="matrix2">The second Matrix to compare</param>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly unequal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Matrix5x5D matrix1, Matrix5x5D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Matrix3x3D" /> to <see cref="Matrix5x5D" />.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Matrix5x5D(Matrix3x3D source)
            => new Matrix5x5D(
                source.M0x0, source.M0x1, source.M0x2, 0d, 0d,
                source.M1x0, source.M1x1, source.M1x2, 0d, 0d,
                source.M2x0, source.M2x1, source.M2x2, 0d, 0d,
                0d, 0d, 0d, 1d, 0d,
                0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Matrix2x2D" /> to <see cref="Matrix5x5D" />.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Matrix5x5D(Matrix2x2D source)
            => new Matrix5x5D(
                source.M0x0, source.M0x1, 0d, 0d, 0d,
                source.M1x0, source.M1x1, 0d, 0d, 0d,
                0d, 0d, 1d, 0d, 0d,
                0d, 0d, 0d, 1d, 0d,
                0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// Tuple to <see cref="Matrix5x5D" />.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
                double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
                double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
                double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
                double M4x0, double M4x1, double M4x2, double M4x3, double M4x4)(Matrix5x5D matrix) => (
            matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M0x3, matrix.M0x4,
            matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M1x3, matrix.M1x4,
            matrix.M2x0, matrix.M2x1, matrix.M2x2, matrix.M2x3, matrix.M2x4,
            matrix.M3x0, matrix.M3x1, matrix.M3x2, matrix.M3x3, matrix.M3x4,
            matrix.M4x0, matrix.M4x1, matrix.M4x2, matrix.M4x3, matrix.M4x4);

        /// <summary>
        /// Tuple to <see cref="Matrix5x5D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Matrix5x5D((double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
                double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
                double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
                double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
                double M4x0, double M4x1, double M4x2, double M4x3, double M4x4) tuple) => new Matrix5x5D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Plus(Matrix5x5D value) => Operations.Plus(
            value.M0x0, value.M0x1, value.M0x2, value.M0x3, value.M0x4,
            value.M1x0, value.M1x1, value.M1x2, value.M1x3, value.M1x4,
            value.M2x0, value.M2x1, value.M2x2, value.M2x3, value.M2x4,
            value.M3x0, value.M3x1, value.M3x2, value.M3x3, value.M3x4,
            value.M4x0, value.M4x1, value.M4x2, value.M4x3, value.M4x4);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Add(Matrix5x5D augend, Matrix5x5D addend) => AddMatrix(
            augend.M0x0, augend.M0x1, augend.M0x2, augend.M0x3, augend.M0x4,
            augend.M1x0, augend.M1x1, augend.M1x2, augend.M1x3, augend.M1x4,
            augend.M2x0, augend.M2x1, augend.M2x2, augend.M2x3, augend.M2x4,
            augend.M3x0, augend.M3x1, augend.M3x2, augend.M3x3, augend.M3x4,
            augend.M4x0, augend.M4x1, augend.M4x2, augend.M4x3, augend.M4x4,
            addend.M0x0, addend.M0x1, addend.M0x2, addend.M0x3, addend.M0x4,
            addend.M1x0, addend.M1x1, addend.M1x2, addend.M1x3, addend.M1x4,
            addend.M2x0, addend.M2x1, addend.M2x2, addend.M2x3, addend.M2x4,
            addend.M3x0, addend.M3x1, addend.M3x2, addend.M3x3, addend.M3x4,
            addend.M4x0, addend.M4x1, addend.M4x2, addend.M4x3, addend.M4x4);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Negate(Matrix5x5D value) => Operations.Negate(
            value.M0x0, value.M0x1, value.M0x2, value.M0x3, value.M0x4,
            value.M1x0, value.M1x1, value.M1x2, value.M1x3, value.M1x4,
            value.M2x0, value.M2x1, value.M2x2, value.M2x3, value.M2x4,
            value.M3x0, value.M3x1, value.M3x2, value.M3x3, value.M3x4,
            value.M4x0, value.M4x1, value.M4x2, value.M4x3, value.M4x4);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Subtract(Matrix5x5D minuend, Matrix5x5D subend) => Subtract5x5x5x5(
            minuend.M0x0, minuend.M0x1, minuend.M0x2, minuend.M0x3, minuend.M0x4,
            minuend.M1x0, minuend.M1x1, minuend.M1x2, minuend.M1x3, minuend.M1x4,
            minuend.M2x0, minuend.M2x1, minuend.M2x2, minuend.M2x3, minuend.M2x4,
            minuend.M3x0, minuend.M3x1, minuend.M3x2, minuend.M3x3, minuend.M3x4,
            minuend.M4x0, minuend.M4x1, minuend.M4x2, minuend.M4x3, minuend.M4x4,
            subend.M0x0, subend.M0x1, subend.M0x2, subend.M0x3, subend.M0x4,
            subend.M1x0, subend.M1x1, subend.M1x2, subend.M1x3, subend.M1x4,
            subend.M2x0, subend.M2x1, subend.M2x2, subend.M2x3, subend.M2x4,
            subend.M3x0, subend.M3x1, subend.M3x2, subend.M3x3, subend.M3x4,
            subend.M4x0, subend.M4x1, subend.M4x2, subend.M4x3, subend.M4x4);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix5x5D multiplicand, double multiplier) => Scale5x5(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4,
            multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(double multiplicand, Matrix5x5D multiplier) => Scale5x5(
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4,
            multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Matrix5x5D multiplicand, Vector5D multiplier) => MultiplyMatrix5x5ByVerticalVector5D(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4,
            multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4,
            multiplier.I, multiplier.J, multiplier.K, multiplier.L, multiplier.M);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Vector5D multiplicand, Matrix5x5D multiplier) => MultiplyHorizontalVector5DByMatrix5x5(
            multiplicand.I, multiplicand.J, multiplicand.K, multiplicand.L, multiplicand.M,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4,
            multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix5x5D multiplicand, Matrix5x5D multiplier) => Multiply5x5x5x5(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4,
            multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4,
            multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix5x5D multiplicand, Matrix4x4D multiplier) => Multiply5x5x4x4(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4,
            multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix4x4D multiplicand, Matrix5x5D multiplier) => Multiply4x4x5x5(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4,
            multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix5x5D multiplicand, Matrix3x3D multiplier) => Multiply5x5x3x3(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4,
            multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix3x3D multiplicand, Matrix5x5D multiplier) => Multiply3x3x5x5(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4,
            multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix5x5D multiplicand, Matrix2x2D multiplier) => Multiply5x5x2x2(
            multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4,
            multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4,
            multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4,
            multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4,
            multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4,
            multiplier.M0x0, multiplier.M0x1,
            multiplier.M1x0, multiplier.M1x1);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Multiply(Matrix2x2D multiplicand, Matrix5x5D multiplier) => Multiply2x2x5x5(
            multiplicand.M0x0, multiplicand.M0x1,
            multiplicand.M1x0, multiplicand.M1x1,
            multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4,
            multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4,
            multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4,
            multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4,
            multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Matrix5x5D d && Equals(d);

        /// <summary>
        /// Compares two <see cref="Matrix5x5D" /> instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <param name="matrix2">The second Matrix to compare</param>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix5x5D matrix2) =>
            M0x0 == matrix2.M0x0 && M0x1 == matrix2.M0x1 && M0x2 == matrix2.M0x2 && M0x3 == matrix2.M0x3 && M0x4 == matrix2.M0x4 &&
            M1x0 == matrix2.M1x0 && M1x1 == matrix2.M1x1 && M1x2 == matrix2.M1x2 && M1x3 == matrix2.M1x3 && M1x4 == matrix2.M1x4 &&
            M2x0 == matrix2.M2x0 && M2x1 == matrix2.M2x1 && M2x2 == matrix2.M2x2 && M2x3 == matrix2.M2x3 && M2x4 == matrix2.M2x4 &&
            M3x0 == matrix2.M3x0 && M3x1 == matrix2.M3x1 && M3x2 == matrix2.M3x2 && M3x3 == matrix2.M3x3 && M3x4 == matrix2.M3x4 &&
            M4x0 == matrix2.M4x0 && M4x1 == matrix2.M4x1 && M4x2 == matrix2.M4x2 && M4x3 == matrix2.M4x3 && M4x4 == matrix2.M4x4;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
                double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
                double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
                double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
                double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) ToValueTuple() => (
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromValueTuple((double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
                double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
                double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
                double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
                double M4x0, double M4x1, double M4x2, double M4x3, double M4x4) tuple) => new Matrix5x5D(tuple);

        /// <summary>
        /// Converts to matrix5x5d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix5x5D ToMatrix5x5D() => new Matrix5x5D(
            M0x0, M0x1, M0x2, M0x3, M0x4,
            M1x0, M1x1, M1x2, M1x3, M1x4,
            M2x0, M2x1, M2x2, M2x3, M2x4,
            M3x0, M3x1, M3x2, M3x3, M3x4,
            M4x0, M4x1, M4x2, M4x3, M4x4);
        #endregion

        #region Factories
        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(Vector2D scale)
            => new Matrix5x5D(
                scale.I, 0d, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d, 0d,
                0d, 0d, 1, 0d, 0d,
                0d, 0d, 0d, 1, 0d,
                0d, 0d, 0d, 0d, 1);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(Vector3D scale)
            => new Matrix5x5D(
                scale.I, 0d, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d, 0d,
                0d, 0d, scale.K, 0d, 0d,
                0d, 0d, 0d, 1d, 0d,
                0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(Vector4D scale)
            => new Matrix5x5D(
                scale.I, 0d, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d, 0d,
                0d, 0d, scale.K, 0d, 0d,
                0d, 0d, 0d, scale.L, 0d,
                0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(Vector5D scale)
            => new Matrix5x5D(
                scale.I, 0d, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d, 0d,
                0d, 0d, scale.K, 0d, 0d,
                0d, 0d, 0d, scale.L, 0d,
                0d, 0d, 0d, 0d, scale.M);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(double scaleX, double scaleY)
            => new Matrix5x5D(
                scaleX, 0d, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d, 0d,
                0d, 0d, 1, 0d, 0d,
                0d, 0d, 0d, 1, 0d,
                0d, 0d, 0d, 0d, 1);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <param name="scaleZ">The scale factor in the z dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(double scaleX, double scaleY, double scaleZ)
            => new Matrix5x5D(
                scaleX, 0d, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d, 0d,
                0d, 0d, scaleZ, 0d, 0d,
                0d, 0d, 0d, 1d, 0d,
                0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <param name="scaleZ">The scale factor in the z dimension</param>
        /// <param name="scaleW">The scale factor in the w dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(double scaleX, double scaleY, double scaleZ, double scaleW)
            => new Matrix5x5D(
                scaleX, 0d, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d, 0d,
                0d, 0d, scaleZ, 0d, 0d,
                0d, 0d, 0d, scaleW, 0d,
                0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <param name="scaleZ">The scale factor in the z dimension</param>
        /// <param name="scaleW">The scale factor in the w dimension</param>
        /// <param name="scaleV">The scale v.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D FromScale(double scaleX, double scaleY, double scaleZ, double scaleW, double scaleV)
            => new Matrix5x5D(
                scaleX, 0d, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d, 0d,
                0d, 0d, scaleZ, 0d, 0d,
                0d, 0d, 0d, scaleW, 0d,
                0d, 0d, 0d, 0d, scaleV);

        /// <summary>
        /// Parse a string for a <see cref="Matrix5x5D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix5x5D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix5x5D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix5x5D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix5x5D" /> data</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix5x5D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix5x5D Parse(string source, IFormatProvider formatProvider)
        {
            var tokenizer = new Tokenizer(source, formatProvider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Identity) ? Identity : new Matrix5x5D(
                Convert.ToDouble(firstToken, formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Standard Methods
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { M0x0, M0x1, M0x2, M0x3, M0x4 },
                new List<double> { M1x0, M1x1, M1x2, M1x3, M1x4 },
                new List<double> { M2x0, M2x1, M2x2, M2x3, M2x4 },
                new List<double> { M3x0, M3x1, M3x2, M3x3, M3x4 },
                new List<double> { M4x0, M4x1, M4x2, M4x3, M4x4 },
            }.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the HashCode for this Matrix
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Matrix
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(M0x0);
            hash.Add(M0x1);
            hash.Add(M0x2);
            hash.Add(M0x3);
            hash.Add(M0x4);
            hash.Add(M1x0);
            hash.Add(M1x1);
            hash.Add(M1x2);
            hash.Add(M1x3);
            hash.Add(M1x4);
            hash.Add(M2x0);
            hash.Add(M2x1);
            hash.Add(M2x2);
            hash.Add(M2x3);
            hash.Add(M2x4);
            hash.Add(M3x0);
            hash.Add(M3x1);
            hash.Add(M3x2);
            hash.Add(M3x3);
            hash.Add(M3x4);
            hash.Add(M4x0);
            hash.Add(M4x1);
            hash.Add(M4x2);
            hash.Add(M4x3);
            hash.Add(M4x4);
            return hash.ToHashCode();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix5x5D" /> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Matrix5x5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix5x5D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Matrix5x5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix5x5D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Matrix5x5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Matrix5x5D);
            if (IsIdentity) return nameof(Identity);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Matrix5x5D)}({nameof(M0x0)}:{M0x0.ToString(format, formatProvider)}{s} {nameof(M0x1)}:{M0x1.ToString(format, formatProvider)}{s} {nameof(M0x2)}:{M0x2.ToString(format, formatProvider)}{s} {nameof(M0x3)}:{M0x3.ToString(format, formatProvider)}{s} {nameof(M0x4)}:{M0x4.ToString(format, formatProvider)}{s} {nameof(M1x0)}:{M1x0.ToString(format, formatProvider)}{s} {nameof(M1x1)}:{M1x1.ToString(format, formatProvider)}{s} {nameof(M1x2)}:{M1x2.ToString(format, formatProvider)}{s} {nameof(M1x3)}:{M1x3.ToString(format, formatProvider)}{s} {nameof(M1x4)}:{M1x4.ToString(format, formatProvider)}{s} {nameof(M2x0)}:{M2x0.ToString(format, formatProvider)}{s} {nameof(M2x1)}:{M2x1.ToString(format, formatProvider)}{s} {nameof(M2x2)}:{M2x2.ToString(format, formatProvider)}{s} {nameof(M2x3)}:{M2x3.ToString(format, formatProvider)}{s} {nameof(M2x4)}:{M2x4.ToString(format, formatProvider)}{s} {nameof(M3x0)}:{M3x0.ToString(format, formatProvider)}{s} {nameof(M3x1)}:{M3x1.ToString(format, formatProvider)}{s} {nameof(M3x2)}:{M3x2.ToString(format, formatProvider)}{s} {nameof(M3x3)}:{M3x3.ToString(format, formatProvider)}{s} {nameof(M3x4)}:{M3x4.ToString(format, formatProvider)}{s} {nameof(M4x0)}:{M4x0.ToString(format, formatProvider)}{s} {nameof(M4x1)}:{M4x1.ToString(format, formatProvider)}{s} {nameof(M4x2)}:{M4x2.ToString(format, formatProvider)}{s} {nameof(M4x3)}:{M4x3.ToString(format, formatProvider)}{s} {nameof(M4x4)}:{M4x4.ToString(format, formatProvider)})";
        }

        /// <summary>
        /// Gets the debugger display.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string GetDebuggerDisplay() => ToString();
        #endregion
    }
}
