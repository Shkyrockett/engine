// <copyright file="TextStyle.cs" company="Shkyrockett" >
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
    /// The text style class.
    /// </summary>
    public class TextStyle
        : IStyle, IDisposable
    {
        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextStyle"/> class.
        /// </summary>
        public TextStyle()
            : this(Pens.Transparent, Pens.Transparent, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextStyle"/> class.
        /// </summary>
        /// <param name="foreBrush">The foreBrush.</param>
        /// <param name="backBrush">The backBrush.</param>
        /// <param name="font">The font.</param>
        public TextStyle(Brush foreBrush, Brush backBrush, Font font)
            : this(new Pen(foreBrush), new Pen(backBrush), null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextStyle"/> class.
        /// </summary>
        /// <param name="forePen">The forePen.</param>
        /// <param name="backPen">The backPen.</param>
        /// <param name="font">The font.</param>
        public TextStyle(Pen forePen, Pen backPen, Font font)
        {
            BackPen = backPen;
            ForePen = forePen;
            Font = font;
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
            if (disposed) return;

            if (disposing)
            {
                // Free any other managed objects here.
                ForePen.Dispose();
                BackPen.Dispose();
                Font.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TextStyle"/> class.
        /// </summary>
        ~TextStyle()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public Font Font { get; set; }

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
        /// Gets the fill.
        /// </summary>
        public IFill Fill { get; }

        /// <summary>
        /// Gets the stroke.
        /// </summary>
        public IStroke Stroke { get; }
    }
}
