using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public class ShapeStyle
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
        private bool disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public ShapeStyle()
            : this(Pens.Transparent, Pens.Transparent)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        public ShapeStyle(Brush forePen, Brush backPen)
            : this(new Pen(backPen), new Pen(forePen))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        public ShapeStyle(Pen forePen, Pen backPen)
        {
            this.backPen = backPen;
            this.forePen = forePen;
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
        ~ShapeStyle()
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
    }
}
