// <copyright file="PathQuadraticBezier.cs" >
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
    public class PathQuadraticBezier
         : PathItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathQuadraticBezier()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public PathQuadraticBezier(PathItem previous, bool relitive, Double[] args)
            : this(previous, relitive, args.Length == 4 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]) }
                       : args.Length == 2 ? new Point2D[] { new Point2D(args[0], args[1]) } : null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public PathQuadraticBezier(PathItem previous, bool relitive, Point2D[] args)
            : this(previous, args.Length == 2 ? args[0] : null, args.Length == 2 ? args[0] : args[1])
        {
            if (relitive)
            {
                Handle = (Point2D)(Handle + previous.End);
                End = (Point2D)(End + previous.End);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        public PathQuadraticBezier(PathItem previous, Point2D handle, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle = handle == null ? (Point2D)(2 * previous.End - previous.NextToEnd) : handle;
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
        public Point2D Handle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D NextToEnd { get { return Handle; } set { Handle = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QuadraticBezier ToQuadtraticBezier()
            => new QuadraticBezier(Start, Handle, End);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.QuadraticBezier(Start.X, Start.Y, Handle.X, Handle.Y, End.X, End.Y);

        #endregion
    }
}
