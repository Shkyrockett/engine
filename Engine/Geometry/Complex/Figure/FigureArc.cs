// <copyright file="FigureArc.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class FigureArc
        : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        public FigureArc(FigureItem previous, double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            R1 = r1;
            R2 = r2;
            Angle = angle;
            LargeArc = largeArc;
            Sweep = sweep;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double R1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double R2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LargeArc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Sweep { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => ToEllipticalArc.Bounds;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public EllipticalArc ToEllipticalArc
            => new EllipticalArc(Start.X, Start.Y, R1, R2, Angle, LargeArc, Sweep, End.X, End.Y);
    }
}
