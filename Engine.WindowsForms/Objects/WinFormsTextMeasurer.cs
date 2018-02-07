using Engine.Imaging;
using System;
using System.Drawing;
using System.Drawing.Text;

namespace Engine
{
    /// <summary>
    /// https://stackoverflow.com/a/6708492/7004229
    /// </summary>
    public class WinFormsTextMeasurer
        : IPlatformTextMetrics, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private Image bitmap;

        /// <summary>
        /// 
        /// </summary>
        private Graphics graphics;

        /// <summary>
        /// 
        /// </summary>
        private bool disposedValue = false; // To detect redundant dispose calls

        /// <summary>
        /// 
        /// </summary>
        public WinFormsTextMeasurer()
        {
            bitmap = new Bitmap(1, 1);
            graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="format"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Size2D MeasureString(string text, RenderFont font, TextFormat format, int width = int.MaxValue)
            => graphics.MeasureString(text, font.ToFont(), width, format.ToStringFormat()).ToSize2D();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/1003370/measure-a-string-without-using-a-graphics-object
        /// </remarks>
        public Size2D MeasureString(string text, RenderFont font, int width = int.MaxValue)
            => graphics.MeasureString(text, font.ToFont(), width, StringFormat.GenericDefault).ToSize2D();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/1003370/measure-a-string-without-using-a-graphics-object
        /// </remarks>
        public Size2D MeasureStringClose(string text, RenderFont font, int width = int.MaxValue)
            => graphics.MeasureString(text, font.ToFont(), width, StringFormat.GenericTypographic).ToSize2D();

        #region IDisposable Support
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                bitmap.Dispose();
                graphics.Dispose();
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        /// <summary>
        /// 
        /// </summary>
        ~WinFormsTextMeasurer()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Support

    }
}
