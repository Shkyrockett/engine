// <copyright file="ILocatable.cs" company="Shkyrockett" >
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

namespace Engine.Geometry
{
    /// <summary>
    /// The ILocatable interface.
    /// </summary>
    public interface ILocatable
    {
        /// <summary>
        /// Gets or sets the location of the locatable object.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(Point2DConverter))]
        Point2D Location { get; set; }
    }
}
