// <copyright file="IconStyle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// The icon style class.
    /// </summary>
    public class IconStyle
        : IStyle, IDisposable
    {
        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="IconStyle"/> class.
        /// </summary>
        public IconStyle()
            : this(Pens.Transparent, Pens.Transparent, null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IconStyle"/> class.
        /// </summary>
        /// <param name="foreBrush">The foreBrush.</param>
        /// <param name="backBrush">The backBrush.</param>
        /// <param name="font">The font.</param>
        /// <param name="icon">The icon.</param>
        public IconStyle(Brush foreBrush, Brush backBrush, Font font, Icon icon)
            : this(new Pen(foreBrush), new Pen(backBrush), null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IconStyle"/> class.
        /// </summary>
        /// <param name="forePen">The forePen.</param>
        /// <param name="backPen">The backPen.</param>
        /// <param name="font">The font.</param>
        /// <param name="icon">The icon.</param>
        public IconStyle(Pen forePen, Pen backPen, Font font, Icon icon)
        {
            BackPen = backPen;
            ForePen = forePen;
            Font = font;
            Icon = icon;
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
        /// Finalizes an instance of the <see cref="IconStyle"/> class.
        /// </summary>
        ~IconStyle()
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
        /// Gets or sets the font.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public Icon Icon { get; set; }

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
