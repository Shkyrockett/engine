// <copyright file="Text2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.WindowsForms
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
        /// <param name="font"></param>
        /// <param name="location"></param>
        /// <param name="metrics"></param>
        public Text2D(string text, RenderFont font, Point2D location, IPlatformTextMetrics metrics)
            : this(text, font, location, new Size2D(int.MaxValue, int.MaxValue), metrics)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="location"></param>
        /// <param name="limits"></param>
        /// <param name="metrics"></param>
        public Text2D(string text, RenderFont font, Point2D location, Size2D limits, IPlatformTextMetrics metrics)
        {
            Text = text;
            Font = font;
            Location = location;
            Limits = limits;
            Metrics = metrics;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RenderFont Font { get; set; }

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
            => new Rectangle2D(Location, Metrics.MeasureString(Text, Font, (int)Limits.Width));

        /// <summary>
        /// 
        /// </summary>
        public IPlatformTextMetrics Metrics { get; set; }

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
            //var sep = ',';// Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Text2D)}{{{nameof(Text)}=\"{Text}\",{nameof(Font)}={Font},{nameof(Location)}={Location},{nameof(Limits)}={Limits}}}";
            return formatable.ToString(format, provider);
        }
    }
}
