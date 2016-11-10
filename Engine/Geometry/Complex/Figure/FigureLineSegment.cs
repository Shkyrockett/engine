// <copyright file="FigureLineSegment.cs" >
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
    public class FigureLineSegment
         : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        public FigureLineSegment()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public FigureLineSegment(FigureItem previous, bool relitive, params Double[] args)
            : this(previous, args.Length == 2 ? new Point2D(args[0], args[1]) : null)
        {
            if (relitive)
                End = (Point2D)(End + previous.End);
        }

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
            => Boundings.LineSegment(Start.X, Start.Y, End.X, End.Y);

        /// <summary>
        /// 
        /// </summary>
        public LineSegment ToLineSegment()
            => new LineSegment(Start, End);
    }
}
