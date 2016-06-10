using System.Drawing;
using System.Drawing.Imaging;
using static System.Math;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageCompare
    {
        private const int bytes = 4;
        private const byte max = 255;

        /// <summary>
        /// Differences the pixels of two images.
        /// </summary>
        /// <param name="a">The first image.</param>
        /// <param name="b">The second image</param>
        /// <returns>Returns a bitmap that expresses the difference of the two images.</returns>
        /// <remarks>Based off of http://stackoverflow.com/questions/9367138/calculate-image-differences-in-c-sharp </remarks>
        public unsafe Bitmap PixelDiff(Bitmap a, Bitmap b)
        {
            if (a == null || b == null) return null;
            else if (a == null) return b;
            else if (b == null) return a;

            var aRect = new Rectangle(Point.Empty, a.Size);
            var bRect = new Rectangle(Point.Empty, b.Size);
            var cRect = new Rectangle(Point.Empty, new Size(a.Width > b.Width ? a.Width : b.Width, a.Height > b.Height ? a.Height : b.Height));

            var c = new Bitmap(cRect.Width, cRect.Height, PixelFormat.Format32bppArgb);

            using (BitmapExtensions.DisposableImageData aData = a.LockBitsDisposable(aRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData bData = b.LockBitsDisposable(bRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData cData = c.LockBitsDisposable(cRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb))
            {
                byte* aPtr = (byte*)aData.Scan0;
                byte* bPtr = (byte*)bData.Scan0;
                byte* cPtr = (byte*)cData.Scan0;

                int cLen = cData.Stride * cData.Height;
                int aEnd = cData.Stride * aData.Height;
                int bEnd = cData.Stride * bData.Height;

                int aCol = 0;
                int bCol = 0;
                int cCol = 0;

                int colorComp = 0;

                for (int cCur = 0; cCur < cLen; cCur++)
                {
                    cCol = cCur % cData.Stride + 1;
                    aCol = cCol > aData.Stride ? aCol : cCol;
                    bCol = cCol > bData.Stride ? bCol : cCol;

                    colorComp = (cCur + 1) % bytes;

                    if (cCur > aEnd || cCol > aCol)
                    {
                        Compare(max, *bPtr, cPtr, colorComp);
                        bPtr++;
                    }
                    else if (cCur > bEnd || cCol > bCol)
                    {
                        Compare(*aPtr, max, cPtr, colorComp);
                        aPtr++;
                    }
                    else if (cCol == aCol && cCol == bCol)
                    {
                        Compare(*aPtr, *bPtr, cPtr, colorComp);
                        aPtr++;
                        bPtr++;
                    }

                    cPtr++;
                }
            }

            return c;
        }

        /// <summary>
        /// Differences the pixels of two images.
        /// </summary>
        /// <param name="a">The first image.</param>
        /// <param name="b">The second image</param>
        /// <returns>Returns a bitmap that expresses the difference of the two images.</returns>
        /// <remarks>Based off of http://stackoverflow.com/questions/9367138/calculate-image-differences-in-c-sharp </remarks>
        public unsafe Bitmap PixelDiff1(Bitmap a, Bitmap b)
        {
            if (a == null || b == null) return null;
            else if (a == null) return b;
            else if (b == null) return a;

            var aRect = new Rectangle(Point.Empty, a.Size);
            var bRect = new Rectangle(Point.Empty, b.Size);
            var cRect = new Rectangle(Point.Empty, new Size(a.Width > b.Width ? a.Width : b.Width, a.Height > b.Height ? a.Height : b.Height));

            var c = new Bitmap(cRect.Width, cRect.Height, PixelFormat.Format32bppArgb);

            using (BitmapExtensions.DisposableImageData aData = a.LockBitsDisposable(aRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData bData = b.LockBitsDisposable(bRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData cData = c.LockBitsDisposable(cRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb))
            {
                byte* aPtr = (byte*)aData.Scan0;
                byte* bPtr = (byte*)bData.Scan0;
                byte* cPtr = (byte*)cData.Scan0;

                int cLen = cData.Stride * cData.Height;
                int aEnd = cData.Stride * aData.Height;
                int bEnd = cData.Stride * bData.Height;

                int aCol = 0;
                int bCol = 0;
                int cCol = 0;

                int colorComp = 0;

                for (int cCur = 0; cCur < cLen; cCur++)
                {
                    cCol = cCur % cData.Stride + 1;
                    aCol = cCol > aData.Stride ? aCol : cCol;
                    bCol = cCol > bData.Stride ? bCol : cCol;

                    colorComp = (cCur + 1) % bytes;

                    if (cCur > aEnd || cCol > aCol)
                    {
                        Compare1(max, *bPtr, cPtr, colorComp);
                        bPtr++;
                    }
                    else if (cCur > bEnd || cCol > bCol)
                    {
                        Compare1(*aPtr, max, cPtr, colorComp);
                        aPtr++;
                    }
                    else if (cCol == aCol && cCol == bCol)
                    {
                        Compare1(*aPtr, *bPtr, cPtr, colorComp);
                        aPtr++;
                        bPtr++;
                    }

                    cPtr++;
                }
            }

            return c;
        }

        /// <summary>
        /// </summary>
        /// <param name="aPtr"></param>
        /// <param name="bPtr"></param>
        /// <param name="cPtr"></param>
        /// <param name="colorComp"></param>
        /// <remarks>
        /// From http://stackoverflow.com/questions/9367138/calculate-image-differences-in-c-sharp 
        /// </remarks>
        private unsafe void Compare(byte aPtr, byte bPtr, byte* cPtr, int colorComp)
        {
            // For alpha, use the average of both images (otherwise pixels with the same alpha won't be visible)
            *cPtr = colorComp == 0 ? (byte)((aPtr + bPtr) / 2) : (byte)~(aPtr ^ bPtr);
        }

        /// <summary>
        /// </summary>
        /// <param name="aPtr"></param>
        /// <param name="bPtr"></param>
        /// <param name="cPtr"></param>
        /// <param name="colorComp"></param>
        /// <remarks>
        /// Based on http://jeffkreeftmeijer.com/2011/comparing-images-and-creating-image-diffs/
        /// </remarks>
        private unsafe void Compare1(byte aPtr, byte bPtr, byte* cPtr, int colorComp)
        {
            *cPtr = (byte)(aPtr + bPtr - 2 * Min(aPtr, bPtr));
        }
    }
}
