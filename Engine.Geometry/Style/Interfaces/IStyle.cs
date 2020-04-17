// <copyright file="IStyle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
    /// The IStyle interface.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IStyle
    {
        /// <summary>
        /// Gets the fill.
        /// </summary>
        /// <value>The <see cref="IFill"/>.</value>
        IFill Fill { get; }

        /// <summary>
        /// Gets the stroke.
        /// </summary>
        /// <value>The <see cref="IStroke"/>.</value>
        IStroke Stroke { get; }
    }
}
