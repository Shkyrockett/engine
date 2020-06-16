// <copyright file="PointSegment2D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The point segment class.
    /// </summary>
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class PointSegment2D
        : CurveSegment2D
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PointSegment"/> class.
        /// </summary>
        public PointSegment2D()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relative">The relative.</param>
        /// <param name="args">The args.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointSegment2D(CurveSegment2D previous, bool relative, double[] args)
            : this(args?.Length == 2 ? new Point2D(args[0], args[1]) : null)
        {
            if (relative)
            {
                Head = (Point2D)(Head + previous?.Tail);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relative">The relative.</param>
        /// <param name="startPoint">The startPoint.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointSegment2D(CurveSegment2D previous, bool relative, Point2D startPoint)
            : this(startPoint)
        {
            if (relative)
            {
                Head = (Point2D)(Head + previous?.Tail);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSegment"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointSegment2D(Point2D? start)
        {
            Head = start;
            Previous = this;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void Deconstruct(out double x, out double y)
        {
            x = Head.X;
            y = Head.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D Head { get; set; }

        /// <summary>
        /// Gets or sets the next to end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D NextToEnd { get { return Head; } set { Head = value; } }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D Tail { get { return Head; } set { Head = value; } }

        /// <summary>
        /// Gets the grips.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override List<Point2D> Grips => new List<Point2D> { Head };

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(Head, Tail));

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length => 0;
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t) => Head;

        #region Methods
        /// <summary>
        /// The to point2d.
        /// </summary>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D ToPoint2D() => Head;
        #endregion Methods
    }
}
