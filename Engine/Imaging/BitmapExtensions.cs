using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="rect"></param>
        /// <param name="flags"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DisposableImageData LockBitsDisposable(this Bitmap bitmap, Rectangle rect, ImageLockMode flags, PixelFormat format) => new DisposableImageData(bitmap, rect, flags, format);

        /// <summary>
        /// 
        /// </summary>
        public class DisposableImageData
            : IDisposable
        {
            private readonly Bitmap _bitmap;
            private readonly BitmapData _data;

            /// <summary>
            /// Initializes a new instance of the <see cref="DisposableImageData"/> class.
            /// </summary>
            /// <param name="bitmap"></param>
            /// <param name="rect"></param>
            /// <param name="flags"></param>
            /// <param name="format"></param>
            internal DisposableImageData(Bitmap bitmap, Rectangle rect, ImageLockMode flags, PixelFormat format)
            {
                _bitmap = bitmap;
                _data = bitmap.LockBits(rect, flags, format);
            }

            /// <summary>
            /// 
            /// </summary>
            ~DisposableImageData()
            {
                Dispose(false);
            }

            /// <summary>
            /// Unlocks this System.Drawing.Bitmap from system memory.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Unlocks this System.Drawing.Bitmap from system memory.
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    // free managed resources
                    if (_bitmap != null)
                    {
                        _bitmap.UnlockBits(_data);
                        _bitmap.Dispose();
                    }
                }
                // free native resources if there are any.
            }

            /// <summary>
            /// Gets or sets the address of the first pixel data in the bitmap. This can also
            /// be thought of as the first scan line in the bitmap.
            /// </summary>
            public IntPtr Scan0 => _data.Scan0;

            /// <summary>
            /// Gets or sets the stride width (also called scan width) of the System.Drawing.Bitmap
            /// object.
            /// </summary>
            public int Stride => _data.Stride;

            /// <summary>
            /// Gets or sets the pixel width of the System.Drawing.Bitmap object. This can also
            /// be thought of as the number of pixels in one scan line.
            /// </summary>
            public int Width => _data.Width;

            /// <summary>
            /// Gets or sets the pixel height of the System.Drawing.Bitmap object. Also sometimes
            /// referred to as the number of scan lines.
            /// </summary>
            public int Height => _data.Height;

            /// <summary>
            /// Gets or sets the format of the pixel information in the System.Drawing.Bitmap
            /// object that returned this System.Drawing.Imaging.BitmapData object.
            /// </summary>
            public PixelFormat PixelFormat => _data.PixelFormat;

            /// <summary>
            /// Reserved. Do not use.
            /// </summary>
            public int Reserved => _data.Reserved;
        }
    }
}
