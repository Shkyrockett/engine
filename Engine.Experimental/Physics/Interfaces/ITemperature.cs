﻿// <copyright file="ITemperature.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// The ITemperature interface.
    /// </summary>
    public interface ITemperature
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The <see cref="double"/>.</value>
        double Value { get; /*set;*/ }

        ///// <summary>
        ///// 
        ///// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //string Name { get; }

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        /// <value>The <see cref="string"/>.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Abreviation { get; }

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string ToString();
    }
}
