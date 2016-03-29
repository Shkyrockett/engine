﻿// <copyright file="CatmullRom.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <date></date>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>
// <remarks>
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// CatmullRom2D
    /// </summary>
    /// <structure>Engine.Geometry.CatmullRom2D</structure>
    /// <remarks>
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    /// <history>
    /// Shkyrockett[Alma Jenks] 9/January/2005 Created
    /// </history>
    [Serializable()]
    public class CatmullRom
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public PointF tangentA;

        /// <summary>
        /// 
        /// </summary>
        public PointF positionA;

        /// <summary>
        /// 
        /// </summary>
        public PointF positionB;

        /// <summary>
        /// 
        /// </summary>
        public PointF tangentB;

        /// <summary>
        /// 
        /// </summary>
        private double precision;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        public CatmullRom()
            : this(PointF.Empty, PointF.Empty, PointF.Empty, PointF.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tangentA"></param>
        /// <param name="positionA"></param>
        /// <param name="positionB"></param>
        /// <param name="tangentB"></param>
        public CatmullRom(PointF tangentA, PointF positionA, PointF positionB, PointF tangentB)
        {
            this.tangentA = tangentA;
            this.positionA = positionA;
            this.positionB = positionB;
            this.tangentB = tangentB;
            precision = 0.1;
            points = InterpolatePoints(precision);
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [XmlAttribute()]
        public PointF TangentA
        {
            get
            {
                return tangentA;
            }
            set
            {
                tangentA = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [XmlAttribute()]
        public PointF PositionA
        {
            get
            {
                return positionA;
            }
            set
            {
                positionA = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [XmlAttribute()]
        public PointF PositionB
        {
            get
            {
                return positionB;
            }
            set
            {
                positionB = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [XmlAttribute()]
        public PointF TangentB
        {
            get
            {
                return tangentB;
            }
            set
            {
                tangentB = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public  List<PointF> Handles
        {
            get
            {
                return new List<PointF> { tangentA, positionA, positionB, tangentB };
            }
            set
            {
                tangentA = value[0];
                positionA = value[1];
                positionB = value[2];
                tangentB = value[3];
                points = InterpolatePoints(precision);
            }
        }

        /// <summary>
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(double index)
        {
            double t2 = index * index;
            double t3 = t2 * index;
            return new PointF(
                (float)(0.5f * ((2.0f * positionA.X) + (-tangentA.X + positionB.X) * index + (2.0f * tangentA.X - 5.0f * positionA.X + 4 * positionB.X - tangentB.X) * t2 + (-tangentA.X + 3.0f * positionA.X - 3.0f * positionB.X + tangentB.X) * t3)),
                (float)(0.5f * ((2.0f * positionA.Y) + (-tangentA.Y + positionB.Y) * index + (2.0f * tangentA.Y - 5.0f * positionA.Y + 4 * positionB.Y - tangentB.Y) * t2 + (-tangentA.Y + 3.0f * positionA.Y - 3.0f * positionB.Y + tangentB.Y) * t3))
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<PointF> InterpolatePoints(double precision)
        {
            List<PointF> points = new List<PointF>();
            for (double Index = 0; (Index == 1); Index += precision)
            {
                points.Add(Interpolate(Index));
            }

            return points;
        }

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <remarks>
        /// Points calculated exist on the spline between points two and three.
        /// </remarks>
        /// <param name="p0">First Point</param>
        /// <param name="p1">Second Point</param>
        /// <param name="p2">Third Point</param>
        /// <param name="p3">Fourth Point</param>
        /// <param name="t">
        /// Normalized distance between second and third point 
        /// where the spline point will be calculated
        /// </param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        static public PointF PointOnCurve(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            PointF ret = new PointF();

            float t2 = t * t;
            float t3 = t2 * t;

            ret.X = 0.5f * ((2.0f * p1.X) +
            (-p0.X + p2.X) * t +
            (2.0f * p0.X - 5.0f * p1.X + 4 * p2.X - p3.X) * t2 +
            (-p0.X + 3.0f * p1.X - 3.0f * p2.X + p3.X) * t3);

            ret.Y = 0.5f * ((2.0f * p1.Y) +
            (-p0.Y + p2.Y) * t +
            (2.0f * p0.Y - 5.0f * p1.Y + 4 * p2.Y - p3.Y) * t2 +
            (-p0.Y + 3.0f * p1.Y - 3.0f * p2.Y + p3.Y) * t3);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Catmullrom";
            return string.Format("{0}{{T1={1},P1={2},T2={2},P2={3},C={4}}}", "Catmullrom", tangentA.ToString(), positionA.ToString(), tangentB.ToString(), positionB.ToString(), precision.ToString());
        }
    }
}
