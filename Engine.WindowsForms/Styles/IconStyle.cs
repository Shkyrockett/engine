// <copyright file="IconStyle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public class IconStyle
        : IStyle, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public IconStyle()
            : this(Pens.Transparent, Pens.Transparent, null, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        public IconStyle(Brush foreBrush, Brush backBrush, Font font, Icon icon)
            : this(new Pen(foreBrush), new Pen(backBrush), null, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        /// <param name="font"></param>
        /// <param name="icon"></param>
        public IconStyle(Pen forePen, Pen backPen, Font font, Icon icon)
        {
            BackPen = backPen;
            ForePen = forePen;
            Font = font;
            Icon = icon;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

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
        /// 
        /// </summary>
        ~IconStyle()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public Pen ForePen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Brush ForeBrush
        {
            get { return ForePen.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Pen BackPen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Brush BackBrush
        {
            get { return BackPen.Brush; }
            set { BackPen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Icon Icon { get; set; }
        public IFill Fill { get; }
        public IStroke Stroke { get; }
    }
}
