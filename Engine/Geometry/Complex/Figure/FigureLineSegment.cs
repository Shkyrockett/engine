// <copyright file="FigureLineSegment.cs" >
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
    public class FigureLineSegment
         : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="end"></param>
        public FigureLineSegment(FigureItem previous, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
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
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.LineSegment(Start.X, Start.Y, End.X, End.Y);

        /// <summary>
        /// 
        /// </summary>
        public LineSegment ToLineSegment
            => new LineSegment(Start, End);
    }
}
