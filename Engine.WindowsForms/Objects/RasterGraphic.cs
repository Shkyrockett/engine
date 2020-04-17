// <copyright file="RasterGraphic.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Engine
{
    /// <summary>
    /// The raster graphic class.
    /// </summary>
    public class RasterGraphic
        : IBitmap, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RasterGraphic"/> class.
        /// </summary>
        public RasterGraphic()
        {
            Bitmap = new Bitmap(1, 1);
            Handles = new List<PointF>();
            Filename = "RasterGraphics.png";
            Name = "RasterGraphics";
            DisplayName = "RasterGraphics";
        }

        /// <summary>
        /// The bitmap.
        /// </summary>
        public Bitmap Bitmap;

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
        /// Gets or sets the stream.
        /// </summary>
        public Stream Stream { get; set; }

        /// <returns></returns>
        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>The <see cref="object"/>.</returns>
        public static object Clone() => new object();

        /// <summary>
        /// Finalizes an instance of the <see cref="RasterGraphic"/> class.
        /// </summary>
        ~RasterGraphic()
        {
            Dispose(false);
        }

        /// <summary>
        /// Unlocks this System.Drawing.Bitmap from system memory.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Unlocks this System.Drawing.Bitmap from system memory.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Bitmap != null)
            {
                // free managed resources
                Bitmap.Dispose();
            }
            // free native resources if there are any.
        }
    }
}
