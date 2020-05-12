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
        /// The bitmap.
        /// </summary>
        private readonly Image bitmap;

        /// <summary>
        /// The graphics.
        /// </summary>
        private readonly Graphics graphics;

        /// <summary>
        /// The disposed value.
        /// </summary>
        private bool disposedValue = false; // To detect redundant dispose calls

        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsTextMeasurer"/> class.
        /// </summary>
        public WinFormsTextMeasurer()
        {
            bitmap = new Bitmap(1, 1);
            graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="format">The format.</param>
        /// <param name="width">The width.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public Size2D MeasureString(string text, RenderFont font, TextFormat format, int width = int.MaxValue)
        {
            using var font1 = font.ToFont();
            using var format1 = format.ToStringFormat();
            return graphics.MeasureString(text, font1, width, format1).ToSize2D();
        }

        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="width">The width.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        /// <remarks>
        /// <para>http://stackoverflow.com/questions/1003370/measure-a-string-without-using-a-graphics-object</para>
        /// </remarks>
        public Size2D MeasureString(string text, RenderFont font, int width = int.MaxValue)
        {
            using var font1 = font.ToFont();
            return graphics.MeasureString(text, font1, width, StringFormat.GenericDefault).ToSize2D();
        }

        /// <summary>
        /// The measure string close.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="width">The width.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        /// <remarks>
        /// <para>http://stackoverflow.com/questions/1003370/measure-a-string-without-using-a-graphics-object</para>
        /// </remarks>
        public Size2D MeasureStringClose(string text, RenderFont font, int width = int.MaxValue)
        {
            using var font1 = font.ToFont();
            return graphics.MeasureString(text, font1, width, StringFormat.GenericTypographic).ToSize2D();
        }

        #region IDisposable Support
        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // ToDo: dispose managed state (managed objects).
                }

                // ToDo: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // ToDo: set large fields to null.
                bitmap.Dispose();
                graphics.Dispose();
                disposedValue = true;
            }
        }

        // ToDo: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        /// <summary>
        /// Finalizes an instance of the <see cref="WinFormsTextMeasurer"/> class.
        /// </summary>
        ~WinFormsTextMeasurer()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // ToDo: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Support
    }
}
