// <copyright file="CatmullRom.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// CatmullRom2D
    /// </summary>
    /// <structure>Engine.Geometry.CatmullRom2D</structure>
    /// <remarks>
    /// <para>http://pomax.github.io/bezierinfo/</para>
    /// </remarks>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("CatmullRom Spline")]
    public class CatmullRom
        : Shape
    {
        /// <summary>
        /// The tangent a.
        /// </summary>
        public Point2D tangentA;

        /// <summary>
        /// The position a.
        /// </summary>
        public Point2D positionA;

        /// <summary>
        /// The position b.
        /// </summary>
        public Point2D positionB;

        /// <summary>
        /// The tangent b.
        /// </summary>
        public Point2D tangentB;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatmullRom"/> class.
        /// </summary>
        public CatmullRom()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatmullRom"/> class.
        /// </summary>
        /// <param name="tangentA">The tangentA.</param>
        /// <param name="positionA">The positionA.</param>
        /// <param name="positionB">The positionB.</param>
        /// <param name="tangentB">The tangentB.</param>
        public CatmullRom(Point2D tangentA, Point2D positionA, Point2D positionB, Point2D tangentB)
        {
            this.tangentA = tangentA;
            this.positionA = positionA;
            this.positionB = positionB;
            this.tangentB = tangentB;
            Precision = 0.1;
        }

        /// <summary>
        /// Gets or sets the tangent a.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [DataMember, XmlAttribute, SoapAttribute]
        public Point2D TangentA
        {
            get { return tangentA; }
            set { tangentA = value; }
        }

        /// <summary>
        /// Gets or sets the position a.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [DataMember, XmlAttribute, SoapAttribute]
        public Point2D PositionA
        {
            get { return positionA; }
            set { positionA = value; }
        }

        /// <summary>
        /// Gets or sets the position b.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [DataMember, XmlAttribute, SoapAttribute]
        public Point2D PositionB
        {
            get { return positionB; }
            set { positionB = value; }
        }

        /// <summary>
        /// Gets or sets the tangent b.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [DataMember, XmlAttribute, SoapAttribute]
        public Point2D TangentB
        {
            get { return tangentB; }
            set { tangentB = value; }
        }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// Gets or sets the handles.
        /// </summary>
        public List<Point2D> Handles
        {
            get { return new List<Point2D> { tangentA, positionA, positionB, tangentB }; }
            set
            {
                tangentA = value[0];
                positionA = value[1];
                positionB = value[2];
                tangentB = value[3];
            }
        }

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => Interpolators.CatmullRom(t, tangentA, positionA, positionB, TangentB);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return nameof(CatmullRom);
            }

            return $"{nameof(CatmullRom)}{{{nameof(TangentA)}={tangentA},{nameof(PositionA)}={positionA},{nameof(TangentB)}={tangentB},{nameof(PositionB)}={positionB},{nameof(Precision)}={Precision}}}";
        }
    }
}
