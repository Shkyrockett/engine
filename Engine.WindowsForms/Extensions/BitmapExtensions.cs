﻿// <copyright file="BitmapExtensions.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Drawing.Imaging;

namespace Engine.Imaging;

/// <summary>
/// The bitmap extensions class.
/// </summary>
public static class BitmapExtensions
{
    /// <summary>
    /// The lock bits disposable.
    /// </summary>
    /// <param name="bitmap">The bitmap.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="flags">The flags.</param>
    /// <param name="format">The format.</param>
    /// <returns>The <see cref="DisposableImageData"/>.</returns>
    public static DisposableImageData LockBitsDisposable(this Bitmap bitmap, Rectangle rect, ImageLockMode flags, PixelFormat format)
    {
        ArgumentNullException.ThrowIfNull(bitmap);

        return new DisposableImageData(bitmap, rect, flags, format);
    }
}

/// <summary>
/// The disposable image data class.
/// </summary>
public class DisposableImageData
    : IDisposable
{
    /// <summary>
    /// The bitmap (readonly).
    /// </summary>
    private readonly Bitmap bitmap;

    /// <summary>
    /// The data (readonly).
    /// </summary>
    private readonly BitmapData data;

    /// <summary>
    /// Initializes a new instance of the <see cref="DisposableImageData"/> class.
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="rect"></param>
    /// <param name="flags"></param>
    /// <param name="format"></param>
    internal DisposableImageData(Bitmap bitmap, Rectangle rect, ImageLockMode flags, PixelFormat format)
    {
        this.bitmap = bitmap;
        data = bitmap.LockBits(rect, flags, format);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="DisposableImageData"/> class.
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
            if (bitmap is not null)
            {
                bitmap.UnlockBits(data);
                bitmap.Dispose();
            }
        }
        // free native resources if there are any.
    }

    /// <summary>
    /// Gets or sets the address of the first pixel data in the bitmap. This can also
    /// be thought of as the first scan line in the bitmap.
    /// </summary>
    public IntPtr Scan0 => data.Scan0;

    /// <summary>
    /// Gets or sets the stride width (also called scan width) of the System.Drawing.Bitmap
    /// object.
    /// </summary>
    public int Stride => data.Stride;

    /// <summary>
    /// Gets or sets the pixel width of the System.Drawing.Bitmap object. This can also
    /// be thought of as the number of pixels in one scan line.
    /// </summary>
    public int Width => data.Width;

    /// <summary>
    /// Gets or sets the pixel height of the System.Drawing.Bitmap object. Also sometimes
    /// referred to as the number of scan lines.
    /// </summary>
    public int Height => data.Height;

    /// <summary>
    /// Gets or sets the format of the pixel information in the System.Drawing.Bitmap
    /// object that returned this System.Drawing.Imaging.BitmapData object.
    /// </summary>
    public PixelFormat PixelFormat => data.PixelFormat;

    /// <summary>
    /// Reserved. Do not use.
    /// </summary>
    public int Reserved => data.Reserved;
}
