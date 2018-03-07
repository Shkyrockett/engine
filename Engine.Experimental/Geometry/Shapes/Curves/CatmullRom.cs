// <copyright file="CatmullRom.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("CatmullRom Spline")]
    public class CatmullRom
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Point2D tangentA;

        /// <summary>
        /// 
        /// </summary>
        public Point2D positionA;

        /// <summary>
        /// 
        /// </summary>
        public Point2D positionB;

        /// <summary>
        /// 
        /// </summary>
        public Point2D tangentB;

        /// <summary>
        /// 
        /// </summary>
        public CatmullRom()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tangentA"></param>
        /// <param name="positionA"></param>
        /// <param name="positionB"></param>
        /// <param name="tangentB"></param>
        public CatmullRom(Point2D tangentA, Point2D positionA, Point2D positionB, Point2D tangentB)
        {
            this.tangentA = tangentA;
            this.positionA = positionA;
            this.positionB = positionB;
            this.tangentB = tangentB;
            Precision = 0.1;
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolators.CatmullRom(tangentA, positionA, positionB, TangentB, t);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(CatmullRom);
            return $"{nameof(CatmullRom)}{{{nameof(TangentA)}={tangentA},{nameof(PositionA)}={positionA},{nameof(TangentB)}={tangentB},{nameof(PositionB)}={positionB},{nameof(Precision)}={Precision}}}";
        }
    }
}
