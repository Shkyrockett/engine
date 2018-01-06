// <copyright file="IBitmap.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.IO;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBitmap
    {
        /// <summary>
        /// 
        /// </summary>
        Stream Stream { get; set; }
    }
}
