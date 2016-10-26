// <copyright file="FigureQuadraticBezier.cs" >
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
    public class FigureQuadraticBezier
         : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        public FigureQuadraticBezier(FigureItem previous, Point2D handle, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle = handle;
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
        public Point2D Handle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public QuadraticBezier ToQuadtraticBezier
            => new QuadraticBezier(Start, Handle, End);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.QuadraticBezier(Start, Handle, End);
    }
}
