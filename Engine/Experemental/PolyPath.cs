// <copyright file="Path.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;

namespace Engine.Geometry
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
        private List<IOpenShape> shapes;

        /// <summary>
        /// 
        /// </summary>
        public List<IOpenShape> Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Path";
        }
    }
}
