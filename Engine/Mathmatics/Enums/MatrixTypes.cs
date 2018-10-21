// <copyright file="MatrixTypes.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// MatrixTypes
    /// </summary>
    [Flags]
    public enum MatrixTypes
        : byte
    {
        /// <summary>
        /// The Identity = 0.
        /// </summary>
        Identity = 0,

        /// <summary>
        /// The Translation = 1.
        /// </summary>
        Translation = 1,

        /// <summary>
        /// The Scaling = 2.
        /// </summary>
        Scaling = 2,

        /// <summary>
        /// The Unknown = 4.
        /// </summary>
        Unknown = 4
    }
}
