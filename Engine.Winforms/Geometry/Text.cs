// <copyright file="Text2D.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    public class Text2D
        : GraphicsObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="location"></param>
        public Text2D(string text, Point2D location)
            : this(text, Control.DefaultFont, location, new Size2D(int.MaxValue, int.MaxValue))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="location"></param>
        public Text2D(string text, Font font, Point2D location)
            : this(text, font, location, new Size2D(int.MaxValue, int.MaxValue))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="location"></param>
        /// <param name="limits"></param>
        public Text2D(string text, Font font, Point2D location, Size2D limits)
        {
            Text = text;
            Font = font;
            Location = location;
            Limits = limits;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Limits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
            => new Rectangle2D(Location, MeasureString(Text, Font, (int)Limits.Width));

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
        public static Size2D MeasureString(string text, Font font, int width = int.MaxValue)
        {
            Size2D result;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    result = g.MeasureString(text, font, width, StringFormat.GenericDefault).ToSize2D();
                }
            }

            return result;
        }

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
        public static Size2D MeasureStringClose(string text, Font font, int width = int.MaxValue)
        {
            Size2D result;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    result = g.MeasureString(text, font, width, StringFormat.GenericTypographic).ToSize2D();
                }
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Intersections.Contains(Bounds, point) != Inclusion.Outside;

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D"/> .
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool Contains(Rectangle2D rect)
            => Intersections.Contains(Bounds, rect);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IntersectsWith(Rectangle2D rect)
            => Intersections.Intersects(Bounds, rect);

        /// <summary>
        /// Creates a string representation of this <see cref="Rectangle2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null)
                return nameof(Text2D);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Text2D)}{{{nameof(Text)}=\"{Text}\",{nameof(Font)}={Font},{nameof(Location)}={Location},{nameof(Limits)}={Limits}}}";
            return formatable.ToString(format, provider);
        }
    }
}
