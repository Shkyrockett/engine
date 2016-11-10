// <copyright file="VectorGraphic.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.Drawing;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class VectorGraphic
        : IImage
    {
        /// <summary>
        /// 
        /// </summary>
        public VectorGraphic()
        {
            Bitmap = new Bitmap(1, 1);
            Handles = new List<PointF>();
            Filename = "VectorGraphics.vector";
            Name = "VectorGraphics";
            DisplayName = "VectorGraphics";
            Items = new List<IRenderable>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PointF> Handles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IRenderable> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone() => new object();
    }
}
