using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageStyle
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
        private Image image;

        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public ImageStyle()
            : this(Pens.Transparent, Pens.Transparent, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        public ImageStyle(Brush foreBrush, Brush backBrush, Image image)
            : this(new Pen(foreBrush), new Pen(backBrush), image)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        /// <param name="image"></param>
        public ImageStyle(Pen forePen, Pen backPen, Image image)
        {
            this.backPen = backPen;
            this.forePen = forePen;
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
        ~ImageStyle()
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
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
