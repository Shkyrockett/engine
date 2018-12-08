// <copyright file="ImageCompare.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Drawing;
using System.Drawing.Imaging;
using static System.Math;

namespace Engine.Imaging
{
    /// <summary>
    /// The image compare class.
    /// </summary>
    public class ImageCompare
    {
        /// <summary>
        /// The bytes (const). Value: 4.
        /// </summary>
        private const int bytes = 4;

        /// <summary>
        /// The max (const). Value: 255.
        /// </summary>
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
            if (a is null || b is null)
            {
                return null;
            }
            else if (a is null)
            {
                return b;
            }
            else if (b is null)
            {
                return a;
            }

            var aRect = new Rectangle(Point.Empty, a.Size);
            var bRect = new Rectangle(Point.Empty, b.Size);
            var cRect = new Rectangle(Point.Empty, new Size(a.Width > b.Width ? a.Width : b.Width, a.Height > b.Height ? a.Height : b.Height));

            var c = new Bitmap(cRect.Width, cRect.Height, PixelFormat.Format32bppArgb);

            using (BitmapExtensions.DisposableImageData aData = a.LockBitsDisposable(aRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData bData = b.LockBitsDisposable(bRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData cData = c.LockBitsDisposable(cRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb))
            {
                var aPtr = (byte*)aData.Scan0;
                var bPtr = (byte*)bData.Scan0;
                var cPtr = (byte*)cData.Scan0;

                var cLen = cData.Stride * cData.Height;
                var aEnd = cData.Stride * aData.Height;
                var bEnd = cData.Stride * bData.Height;

                var aCol = 0;
                var bCol = 0;
                var cCol = 0;

                var colorComp = 0;

                for (var cCur = 0; cCur < cLen; cCur++)
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
            if (a is null || b is null)
            {
                return null;
            }
            else if (a is null)
            {
                return b;
            }
            else if (b is null)
            {
                return a;
            }

            var aRect = new Rectangle(Point.Empty, a.Size);
            var bRect = new Rectangle(Point.Empty, b.Size);
            var cRect = new Rectangle(Point.Empty, new Size(a.Width > b.Width ? a.Width : b.Width, a.Height > b.Height ? a.Height : b.Height));

            var c = new Bitmap(cRect.Width, cRect.Height, PixelFormat.Format32bppArgb);

            using (BitmapExtensions.DisposableImageData aData = a.LockBitsDisposable(aRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData bData = b.LockBitsDisposable(bRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb))
            using (BitmapExtensions.DisposableImageData cData = c.LockBitsDisposable(cRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb))
            {
                var aPtr = (byte*)aData.Scan0;
                var bPtr = (byte*)bData.Scan0;
                var cPtr = (byte*)cData.Scan0;

                var cLen = cData.Stride * cData.Height;
                var aEnd = cData.Stride * aData.Height;
                var bEnd = cData.Stride * bData.Height;

                var aCol = 0;
                var bCol = 0;
                var cCol = 0;

                var colorComp = 0;

                for (var cCur = 0; cCur < cLen; cCur++)
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
        /// The compare.
        /// </summary>
        /// <param name="aPtr">The aPtr.</param>
        /// <param name="bPtr">The bPtr.</param>
        /// <param name="cPtr">The cPtr.</param>
        /// <param name="colorComp">The colorComp.</param>
        /// <remarks>
        /// From http://stackoverflow.com/questions/9367138/calculate-image-differences-in-c-sharp 
        /// </remarks>
        private unsafe static void Compare(byte aPtr, byte bPtr, byte* cPtr, int colorComp)
            // For alpha, use the average of both images (otherwise pixels with the same alpha won't be visible)
            => *cPtr = colorComp == 0 ? (byte)((aPtr + bPtr) / 2) : (byte)~(aPtr ^ bPtr);

        /// <summary>
        /// The compare1.
        /// </summary>
        /// <param name="aPtr">The aPtr.</param>
        /// <param name="bPtr">The bPtr.</param>
        /// <param name="cPtr">The cPtr.</param>
        /// <param name="colorComp">The colorComp.</param>
        /// <remarks>
        /// Based on http://jeffkreeftmeijer.com/2011/comparing-images-and-creating-image-diffs/
        /// </remarks>
        private unsafe static void Compare1(byte aPtr, byte bPtr, byte* cPtr, int colorComp)
            => *cPtr = (byte)(aPtr + bPtr - 2 * Min(aPtr, bPtr));
    }
}
