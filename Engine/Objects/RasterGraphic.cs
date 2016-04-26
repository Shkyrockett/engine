// <copyright file="Image.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class RasterGraphic
        : IImage, IDisposable
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
        /// <returns></returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }


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
            if (disposing)
            {
                // free managed resources
                if (Bitmap != null)
                {
                    Bitmap.Dispose();
                }
            }
            // free native resources if there are any.
        }
    }
}
