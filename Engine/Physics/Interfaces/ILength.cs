// <copyright file="ILength.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public interface ILength
    {
        /// <summary>
        ///
        /// </summary>
        double Value { get; /*set;*/ }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Name { get; }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Abreviation { get; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
