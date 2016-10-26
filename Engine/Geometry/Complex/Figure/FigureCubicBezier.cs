// <copyright file="FigureCubicBezier.cs" >
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
    public class FigureCubicBezier
         : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        public FigureCubicBezier(FigureItem previous, Point2D handle1, Point2D handle2, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle1 = handle1;
            Handle2 = handle2;
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
        public Point2D Handle1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Handle2 { get; set; }

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
            => Boundings.CubicBezier(Start, Handle1, Handle2, End);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public CubicBezier ToCubicBezier
            => new CubicBezier(Start, Handle1, Handle2, End);
    }
}
