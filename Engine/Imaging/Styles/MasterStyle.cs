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
        private Image image;

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
            forePen = new Pen(foreBrush);
            backPen = new Pen(backBrush);
            this.font = font;
            this.icon = icon;
            this.image = image;
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
            this.forePen = forePen;
            this.backPen = backPen;
            this.font = font;
            this.icon = icon;
            this.image = image;
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
        ~MasterStyle()
        {
            Dispose(false);
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

        /// <summary>
        /// 
        /// </summary>
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
