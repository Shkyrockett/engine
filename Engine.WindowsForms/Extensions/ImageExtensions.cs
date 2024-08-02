// <copyright file="ImageExtensions.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;

namespace Engine.Imaging;

/// <summary>
/// Extended imaging tools class.
/// </summary>
public static class ImageExtensions
{
    /// <summary>
    /// Retrieves the bounds of an image.
    /// </summary>
    /// <param name="image">The image to get the bounds from.</param>
    /// <returns>A rectangle the size of the image.</returns>
    public static RectangleF Bounds(this Image image) => new(PointF.Empty, (image?.Size).Value);

    /// <summary>
    /// Convert an image to a different bit format.
    /// </summary>
    /// <param name="canvas">Original image to convert.</param>
    /// <param name="format">The format to convert to.</param>
    /// <returns>A copy of the original image in the requested Pixel format.</returns>
    public static Image ConvertToFormat(this Image canvas, PixelFormat format)
    {
        if (canvas is null)
        {
            return canvas;
        }

        var NewRect = new Rectangle(Point.Empty, canvas.Size);
        var New_Bmp = new Bitmap(canvas.Width, canvas.Height, format);
        var gr = Graphics.FromImage(New_Bmp);
        gr.DrawImage(canvas, NewRect, NewRect, GraphicsUnit.Pixel);
        return New_Bmp;
    }

    /// <summary>
    /// The crop image.
    /// </summary>
    /// <param name="canvas">The canvas.</param>
    /// <param name="cropArea">The cropArea.</param>
    /// <returns>The <see cref="Image"/>.</returns>
    /// <remarks><para>http://tech.pro/tutorial/620/csharp-tutorial-image-editing-saving-cropping-and-resizing</para></remarks>
    public static Image CropImage(this Image canvas, Rectangle cropArea)
    {
        using var bmpImage = new Bitmap(canvas);
        var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        return bmpCrop;
    }

    /// <summary>
    /// The resize image.
    /// </summary>
    /// <param name="canvas">The canvas.</param>
    /// <param name="size">The size.</param>
    /// <returns>The <see cref="Image"/>.</returns>
    /// <remarks><para>http://tech.pro/tutorial/620/csharp-tutorial-image-editing-saving-cropping-and-resizing</para></remarks>
    public static Image ResizeImage(this Image canvas, Size size)
    {
        var sourceWidth = canvas?.Width;
        var sourceHeight = canvas.Height;
        var nPercentW = size.Width / (float)sourceWidth;
        var nPercentH = size.Height / (float)sourceHeight;

        var nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

        var destWidth = (int)(sourceWidth * nPercent);
        var destHeight = (int)(sourceHeight * nPercent);

        var b = new Bitmap(destWidth, destHeight);
        var g = Graphics.FromImage(b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(canvas, 0, 0, destWidth, destHeight);
        g.Dispose();

        return b;
    }

    ///// <summary>
    /////
    ///// </summary>
    ///// <param name="canvas"></param>
    ///// <param name="radienAngle"></param>
    ///// <returns></returns>
    //public static Image RotateImage(this Image canvas, double radienAngle)
    //{
    //    // Image bounds.
    //    RectangleF imageBounds = canvas.Bounds();

    //    // Corners of the image.
    //    PointF[] rotationPoints = imageBounds.ToPoints();

    //    // Rotate the corners.
    //    PrimitivesExtensions.RotatePoints(rotationPoints, imageBounds.Center(), radienAngle);

    //    // Get the new bounds given from the rotation of the corners to avoid clipping of the image.
    //    Rectangle newBounds = RectangleFExtensions.GetBounds(rotationPoints);

    //    // An empty bitmap to draw the rotated image.
    //    Bitmap rotatedBitmap = new Bitmap(newBounds.Width, newBounds.Height);

    //    using (Graphics graphics = Graphics.FromImage(rotatedBitmap))
    //    {
    //        graphics.SmoothingMode = SmoothingMode.HighQuality;
    //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    //        // Transformation matrix
    //        Matrix matrix = new PointF(canvas.Width / 2.0f, canvas.Height / 2.0f).RotateAroundPoint(radienAngle);
    //        matrix.Translate(-newBounds.Left, -newBounds.Top, MatrixOrder.Append); //shift to compensate for the rotation

    //        graphics.Transform = matrix;
    //        graphics.DrawImage(canvas, 0, 0);
    //        graphics.ResetTransform();
    //        graphics.TranslateTransform(-newBounds.Left, -newBounds.Top);
    //        graphics.DrawRectangle(Pens.CornflowerBlue, newBounds);
    //    }

    //    return rotatedBitmap;
    //}

    ///// <summary>
    /////  Rotate an image to an angle.
    ///// </summary>
    ///// <param name="canvas">The image to rotate.</param>
    ///// <returns>The rotated image.</returns>
    ///// <param name="angle">The angle in radians, to rotate the image.</param>
    //public static Image RotateByAngle(this Image canvas, double angle)
    //{
    //    // If there is nothing to rotate, return what was given.
    //    if (canvas is null) return canvas;

    //    // New canvas bounding size.
    //    RectangleF bounds = new RectangleF(PointF.Empty, canvas.Size).RotatedOffsetBounds(angle);

    //    // Center point of the new canvas.
    //    PointF center = bounds.Center();

    //    // The bounding rectangle for the image to draw.
    //    RectangleF imageBounds = RectangleFExtensions.RectangleFFromCenter(center, canvas.Size);

    //    // Create the new Bitmap to draw on with it's new boundaries.
    //    Bitmap picture = new Bitmap((int)bounds.Size.Width, (int)bounds.Size.Height, PixelFormat.Format32bppArgb);
    //    Graphics graphics = Graphics.FromImage(picture);
    //    graphics.SmoothingMode = SmoothingMode.HighQuality;
    //    graphics.CompositingQuality = CompositingQuality.HighQuality;
    //    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    //    // Draw the background with a transparent colored brush.
    //    graphics.FillRectangle(Brushes.Transparent, bounds);

    //    // Setup the transform for drawing the picture back on to the canvas.
    //    graphics.Transform = center.RotateAroundPoint(angle);

    //    // Draw the picture back onto the new canvas.
    //    graphics.DrawImage(canvas, imageBounds);

    //    // Return the result.
    //    return picture;
    //}

    /// <summary>
    /// Retrieve Cursor Resource from Executable
    /// </summary>
    /// <param name="ResourceName"></param>
    /// <returns></returns>
    /// <remarks><para>BE SURE (embedded).cur HAS BUILD ACTION IN PROPERTIES SET TO EMBEDDED RESOURCE!!</para></remarks>
    public static Cursor RetriveCursorResource(string ResourceName)
    {
        //  Get the namespace
        var strNameSpace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString(CultureInfo.InvariantCulture);
        //  Get the resource into a stream
        var ResourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream($"{strNameSpace}.{ResourceName}");
        if (ResourceStream is null)
        {
            // ToDo: #If Then ... Warning! not translated
            MessageBox.Show($"Unable to find: {ResourceName}\r\nBe Sure {ResourceName} Property Build Action is set to Embedded Resource\r\nAnother reason can be that the Project Root Namespace is not the same as the Assembly Name");
            // ToDo: # ... Warning! not translated

            //  ToDo: Report the Error message in a nicer fashion since this in game.
            //  Perhaps on Exit provide a message errors were encountered and
            //  ignored would you like to send an error report?
            // ToDo: #End If ... Warning! not translated
            return Cursors.Default;
        }
        else
        {
            //  Return the Resource as a cursor
            if (ResourceStream.CanRead)
            {
                return new Cursor(ResourceStream);
            }
            else
            {
                return Cursors.Default;
            }
        }
    }
}
