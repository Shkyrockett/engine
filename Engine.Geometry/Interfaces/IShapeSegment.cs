// <copyright file="IShapeSegment.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The IShapeSegment interface.
    /// </summary>
    public interface IShapeSegment
        : IShape
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IShapeSegment"/> position should be calculated relative to the last item, or from Origin.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if relative; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool Relative { get; set; }

        /// <summary>
        /// Gets or sets a reference to the segment after this segment.
        /// </summary>
        /// <value>
        /// The before.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        IShapeSegment? Before { get; set; }

        /// <summary>
        /// Gets or sets a reference to the segment before this segment.
        /// </summary>
        /// <value>
        /// The after.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        IShapeSegment? After { get; set; }

        /// <summary>
        /// Gets or sets the head point.
        /// </summary>
        /// <value>
        /// The head.
        /// </value>
        [TypeConverter(typeof(Point2DConverter))]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Point2D Head { get; set; }

        /// <summary>
        /// Gets or sets the next to first point from the head point.
        /// </summary>
        /// <value>
        /// The next to head point.
        /// </value>
        [TypeConverter(typeof(Point2DConverter))]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Point2D NextToHead { get; set; }

        /// <summary>
        /// Gets or sets the next to last point to the tail point.
        /// </summary>
        /// <value>
        /// The next to tail point.
        /// </value>
        [TypeConverter(typeof(Point2DConverter))]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Point2D NextToTail { get; set; }

        /// <summary>
        /// Gets or sets the tail point.
        /// </summary>
        /// <value>
        /// The tail.
        /// </value>
        [TypeConverter(typeof(Point2DConverter))]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Point2D Tail { get; set; }
    }
}
