// <copyright file="Path.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(PolyPath))]
    public class PolyPath
            : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public List<IOpenShape> Shapes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => "Path";
    }
}
