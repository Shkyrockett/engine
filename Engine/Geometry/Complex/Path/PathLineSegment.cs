// <copyright file="PathLineSegment.cs" company="Shkyrockett" >
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
    public class PathLineSegment
         : PathItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathLineSegment()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public PathLineSegment(PathItem previous, bool relitive, params Double[] args)
            : this(previous, args.Length == 2 ? (Point2D?)new Point2D(args[0], args[1]) : null)
        {
            if (relitive)
                End = (Point2D)(End + previous.End);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="end"></param>
        public PathLineSegment(PathItem previous, Point2D? end)
        {
            Previous = previous;
            previous.Next = this;
            End = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? Start { get => Previous.End; set => Previous.End = value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get => Start; set => Start = value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D? End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.LineSegment(Start.Value.X, Start.Value.Y, End.Value.X, End.Value.Y);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override double Length => ToLineSegment().Length;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public LineSegment ToLineSegment()
            => new LineSegment(Start.Value, End.Value);

        #endregion
    }
}
