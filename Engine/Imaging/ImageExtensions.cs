// <copyright file="ImageExtensions.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Engine.Imaging
{
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
        public static RectangleF Bounds(this Image image) => new RectangleF(PointF.Empty, image.Size);

        /// <summary>
        /// Convert an image to a different bit format.
        /// </summary>
        /// <param name="canvas">Original image to convert.</param>
        /// <param name="format">The format to convert to.</param>
        /// <returns>A copy of the original image in the requested Pixel format.</returns>
        public static Image ConvertToFormat(this Image canvas, PixelFormat format)
        {
            if (canvas == null) return canvas;

            var NewRect = new Rectangle(Point.Empty, canvas.Size);
            var New_Bmp = new Bitmap(canvas.Width, canvas.Height, format);
            Graphics gr = Graphics.FromImage(New_Bmp);
            gr.DrawImage(canvas, NewRect, NewRect, GraphicsUnit.Pixel);
            return New_Bmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        /// <remarks>http://tech.pro/tutorial/620/csharp-tutorial-image-editing-saving-cropping-and-resizing</remarks>
        public static Image CropImage(this Image canvas, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(canvas);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return bmpCrop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <remarks>http://tech.pro/tutorial/620/csharp-tutorial-image-editing-saving-cropping-and-resizing</remarks>
        public static Image resizeImage(this Image canvas, Size size)
        {
            int sourceWidth = canvas.Width;
            int sourceHeight = canvas.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = size.Width / (float)sourceWidth;
            nPercentH = size.Height / (float)sourceHeight;

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
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
        //    if (canvas == null) return canvas;

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
        /// <remarks>BE SURE (embedded).cur HAS BUILD ACTION IN PROPERTIES SET TO EMBEDDED RESOURCE!!</remarks>
        public static Cursor RetriveCursorResource(string ResourceName)
        {
            //  Get the namespace 
            string strNameSpace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            //  Get the resource into a stream 
            Stream ResourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream((strNameSpace + ("." + ResourceName)));
            if (ResourceStream == null)
            {
                // TODO: #If Then ... Warning!!! not translated
                MessageBox.Show(("Unable to find: "
                                + (ResourceName + ("\r\n" + ("Be Sure "
                                + (ResourceName + (" Property Build Action is set to Embedded Resource" + ("\r\n" + "Another reason can be that the Project Root Namespace is not the same as the Assembly Name"))))))));
                // TODO: # ... Warning!!! not translated
            }
            else
            {
                //  ToDo: Report the Error message in a nicer fashion since this in game. 
                //  Perhaps on Exit provide a message errors were encountered and 
                //  ignored would you like to send an error report?
                // TODO: #End If ... Warning!!! not translated
                return Cursors.Default;
            }
            //  Return the Resource as a cursor
            if (ResourceStream.CanRead)
                return new Cursor(ResourceStream);
            else
                return Cursors.Default;
        }
    }
}
