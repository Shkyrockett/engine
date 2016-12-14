// <copyright file="MasterStyle.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public class MasterStyle
        : IStyle, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public MasterStyle()
            : this(Brushes.Transparent, Brushes.Transparent, null, null, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreBrush"></param>
        /// <param name="backBrush"></param>
        /// <param name="font"></param>
        /// <param name="icon"></param>
        /// <param name="image"></param>
        public MasterStyle(Brush foreBrush, Brush backBrush, Font font, Icon icon, Image image)
        {
            ForePen = new Pen(foreBrush);
            BackPen = new Pen(backBrush);
            Font = font;
            Icon = icon;
            Image = image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        /// <param name="font"></param>
        /// <param name="icon"></param>
        /// <param name="image"></param>
        public MasterStyle(Pen forePen, Pen backPen, Font font, Icon icon, Image image)
        {
            ForePen = forePen;
            BackPen = backPen;
            Font = font;
            Icon = icon;
            Image = image;
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
        ~MasterStyle()
        {
            Dispose(false);
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
            get { return BackPen?.Brush; }
            set { BackPen.Brush = value; }
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
            get { return ForePen?.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Image Image { get; set; }
    }
}
