// <copyright file="FigureArc.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FigureArc
        : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        public FigureArc()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public FigureArc(FigureItem item, bool relitive, Double[] args)
            : this(item, args[0], args[1], args[2], args[3] != 0, args[4] != 0, args.Length == 7 ? new Point2D(args[5], args[6]) : null)
        {
            if (relitive)
                End = (Point2D)(End + item.End);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        public FigureArc(FigureItem previous, double rx, double ry, double angle, bool largeArc, bool sweep, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            RX = rx;
            RY = ry;
            Angle = angle;
            LargeArc = largeArc;
            Sweep = sweep;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public double RX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public double RY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public double Angle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public bool LargeArc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public bool Sweep { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D NextToEnd { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => ToEllipticalArc().Bounds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Inclusion Contains(Point2D point)
            => Containings.Contains(ToEllipticalArc(), point);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EllipticalArc ToEllipticalArc()
            => new EllipticalArc(Start.X, Start.Y, RX, RY, Angle, LargeArc, Sweep, End.X, End.Y);
    }
}
