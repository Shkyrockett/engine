// <copyright file="PathCubicBezier.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PathCubicBezier(PathItem item, bool relitive, Point2D[] args)
            : this(item, args.Length == 3 ? (Point2D?)args[0] : null, args.Length == 3 ? args[1] : args[0], args.Length == 3 ? args[1] : args[2])
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PathCubicBezier(PathItem previous, Point2D? handle1, Point2D handle2, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle1 = handle1 ?? (Point2D)(2 * previous.End - previous.NextToEnd);
            Handle2 = handle2;
            End = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Point2D Handle1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Point2D? Handle2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Handle2; } set { Handle2 = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D? End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value, Handle1, Handle2.Value, End.Value };

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Measurements.CubicBezierBounds(Start.Value.X, Start.Value.Y, Handle1.X, Handle1.Y, Handle2.Value.X, Handle2.Value.Y, End.Value.X, End.Value.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => Measurements.CubicBezierArcLength(Start.Value.X, Start.Value.Y, Handle1.X, Handle1.Y, Handle2.Value.X, Handle2.Value.Y, End.Value.X, End.Value.Y));

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => ToCubicBezier().Interpolate(t);

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CubicBezier ToCubicBezier()
            => new CubicBezier(Start.Value, Handle1, Handle2.Value, End.Value);

        #endregion
    }
}
