﻿// <copyright file="GraphicsCataloge.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphicsCataloge
    {
        /// <summary>
        /// 
        /// </summary>
        List<GraphicsObject> items;

        /// <summary>
        /// 
        /// </summary>
        public List<GraphicsObject> Items { get { return items; } set { items = value; } }
    }
}