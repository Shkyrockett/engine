// <copyright file="IStyle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    public interface IStyle
    {
        /// <summary>
        /// 
        /// </summary>
        IFill Fill { get; }

        /// <summary>
        /// 
        /// </summary>
        IStroke Stroke { get; }
    }
}
