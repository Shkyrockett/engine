// <copyright file="RasterGraphic.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public class RasterGraphic
        : IBitmap, IDisposable
    {
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public Bitmap Bitmap;

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
        public Stream Stream { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static object Clone() => new object();

        /// <summary>
        /// 
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
