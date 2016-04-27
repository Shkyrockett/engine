// <copyright file="CatmullRom.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks>
// </remarks>

using Engine.Imaging;
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
    [Serializable]
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
        private double precision;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        public CatmullRom()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty)
        {
        }

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
            precision = 0.1;
            points = InterpolatePoints(precision);
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D TangentA
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
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D PositionA
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
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D PositionB
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
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D TangentB
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
        public  List<Point2D> Handles
        {
            get
            {
                return new List<Point2D> { tangentA, positionA, positionB, tangentB };
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
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D Interpolate(double index)
        {
            double t2 = index * index;
            double t3 = t2 * index;
            return new Point2D(
                0.5f * ((2.0f * positionA.X) + (-tangentA.X + positionB.X) * index + (2.0f * tangentA.X - 5.0f * positionA.X + 4 * positionB.X - tangentB.X) * t2 + (-tangentA.X + 3.0f * positionA.X - 3.0f * positionB.X + tangentB.X) * t3),
                0.5f * ((2.0f * positionA.Y) + (-tangentA.Y + positionB.Y) * index + (2.0f * tangentA.Y - 5.0f * positionA.Y + 4 * positionB.Y - tangentB.Y) * t2 + (-tangentA.Y + 3.0f * positionA.Y - 3.0f * positionB.Y + tangentB.Y) * t3)
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints(double precision)
        {
            List<Point2D> points = new List<Point2D>();
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
        static public Point2D PointOnCurve(Point2D p0, Point2D p1, Point2D p2, Point2D p3, double t)
        {
            Point2D ret = new Point2D();

            double t2 = t * t;
            double t3 = t2 * t;

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
            if (this == null) return nameof(CatmullRom);
            return string.Format("{0}{{{1}={2},{3}={4},{5}={6},{7}={8},{9}={10}}}", nameof(CatmullRom), nameof(TangentA), tangentA, nameof(PositionA), positionA, nameof(TangentB), tangentB, nameof(PositionB), positionB, nameof(Precision), precision);
        }
    }
}
