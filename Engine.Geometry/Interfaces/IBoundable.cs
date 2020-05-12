// <copyright file="IBoundable.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
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
    /// The IBoundable interface.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IBoundable
    {
        /// <summary>
        /// Gets the axis oriented bounding box rectangle of the boundable object.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        Rectangle2D Bounds { get; set; }

        /// <summary>
        /// Determines whether this instance contains a Point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        ///   <see langword="true"/> if [contains] [the specified point]; otherwise, <see langword="false"/>.
        /// </returns>
        bool Contains(Point2D point);
    }
}
