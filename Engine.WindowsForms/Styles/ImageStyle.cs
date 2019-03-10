// <copyright file="ImageStyle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// The image style class.
    /// </summary>
    public class ImageStyle
        : IStyle, IDisposable
    {
        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageStyle"/> class.
        /// </summary>
        public ImageStyle()
            : this(Pens.Transparent, Pens.Transparent, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageStyle"/> class.
        /// </summary>
        /// <param name="foreBrush">The foreBrush.</param>
        /// <param name="backBrush">The backBrush.</param>
        /// <param name="image">The image.</param>
        public ImageStyle(Brush foreBrush, Brush backBrush, Image image)
            : this(new Pen(foreBrush), new Pen(backBrush), image)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageStyle"/> class.
        /// </summary>
        /// <param name="forePen">The forePen.</param>
        /// <param name="backPen">The backPen.</param>
        /// <param name="image">The image.</param>
        public ImageStyle(Pen forePen, Pen backPen, Image image)
        {
            BackPen = backPen;
            ForePen = forePen;
            Image = image;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                ForePen.Dispose();
                BackPen.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ImageStyle"/> class.
        /// </summary>
        ~ImageStyle()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the fore pen.
        /// </summary>
        public Pen ForePen { get; set; }

        /// <summary>
        /// Gets or sets the fore brush.
        /// </summary>
        public Brush ForeBrush
        {
            get { return ForePen.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// Gets or sets the back pen.
        /// </summary>
        public Pen BackPen { get; set; }

        /// <summary>
        /// Gets or sets the back brush.
        /// </summary>
        public Brush BackBrush
        {
            get { return BackPen.Brush; }
            set { BackPen.Brush = value; }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// Gets the fill.
        /// </summary>
        public IFill Fill { get; }

        /// <summary>
        /// Gets the stroke.
        /// </summary>
        public IStroke Stroke { get; }
    }
}
