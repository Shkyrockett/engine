// <copyright file="PathCubicBezier.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
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
    public class PathCubicBezier
         : PathItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathCubicBezier()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public PathCubicBezier(PathItem item, bool relitive, Double[] args)
            : this(item, relitive, args.Length == 6 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]), new Point2D(args[4], args[5]) }
                : args.Length == 4 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]) } : null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public PathCubicBezier(PathItem item, bool relitive, Point2D[] args)
            : this(item, args.Length == 3 ? args[0] : null, args.Length == 3 ? args[1] : args[0], args.Length == 3 ? args[1] : args[2])
        {
            if (relitive)
            {
                Handle1 = (Point2D)(Handle1 + item.End);
                Handle2 = (Point2D)(Handle2 + item.End);
                End = (Point2D)(End + item.End);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        public PathCubicBezier(PathItem previous, Point2D handle1, Point2D handle2, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle1 = handle1 ?? (Point2D)(2 * previous.End - previous.NextToEnd) ;
            Handle2 = handle2;
            End = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Point2D Handle1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Point2D Handle2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D NextToEnd { get { return Handle2; } set { Handle2 = value; } }

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
            => Boundings.CubicBezier(Start.X,Start.Y, Handle1.X,Handle1.Y, Handle2.X,Handle2.Y, End.X, End.Y);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CubicBezier ToCubicBezier()
            => new CubicBezier(Start, Handle1, Handle2, End);

        #endregion
    }
}
