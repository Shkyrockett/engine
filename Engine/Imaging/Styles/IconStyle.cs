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
        private Pen forePen;

        /// <summary>
        /// 
        /// </summary>
        private Pen backPen;

        /// <summary>
        /// 
        /// </summary>
        private Font font;

        /// <summary>
        /// 
        /// </summary>
        private Icon icon;

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
            this.backPen = backPen;
            this.forePen = forePen;
            this.font = font;
            this.icon = icon;
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
                forePen.Dispose();
                backPen.Dispose();
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
        public Pen ForePen
        {
            get { return forePen; }
            set { forePen = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush ForeBrush
        {
            get { return forePen.Brush; }
            set { forePen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Pen BackPen
        {
            get { return backPen; }
            set { backPen = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush BackBrush
        {
            get { return backPen.Brush; }
            set { backPen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Icon Icon
        {
            get { return icon; }
            set { icon = value; }
        }
    }
}
