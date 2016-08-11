using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public class TextStyle
        : IStyle, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public TextStyle()
            : this(Pens.Transparent, Pens.Transparent, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        public TextStyle(Brush foreBrush, Brush backBrush, Font font)
            : this(new Pen(foreBrush), new Pen(backBrush), null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        /// <param name="font"></param>
        public TextStyle(Pen forePen, Pen backPen, Font font)
        {
            BackPen = backPen;
            ForePen = forePen;
            Font = font;
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
                Font.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        ~TextStyle()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public Font Font { get; set; }

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
    }
}
