// <copyright file="IShape.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IShape
    {
        /// <summary>
        /// 
        /// </summary>
        Rectangle2D Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool Contains(Point2D point);
    }
}
