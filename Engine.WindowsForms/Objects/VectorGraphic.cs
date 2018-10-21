// <copyright file="VectorGraphic.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Engine
{
    /// <summary>
    /// The vector graphic class.
    /// </summary>
    public class VectorGraphic
        : IBitmap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VectorGraphic"/> class.
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
        /// Gets or sets the bitmap.
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// Gets or sets the handles.
        /// </summary>
        public List<PointF> Handles { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public List<IRenderable> Items { get; set; }

        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        public Stream Stream { get; set; }

        /// <returns></returns>
        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>The <see cref="object"/>.</returns>
        public static object Clone() => new object();
    }
}
