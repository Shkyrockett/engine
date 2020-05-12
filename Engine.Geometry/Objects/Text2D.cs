// <copyright file="Text2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
    /// The text2d class.
    /// </summary>
    public class Text2D
        : GraphicsObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Text2D"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="location">The location.</param>
        /// <param name="metrics">The metrics.</param>
        public Text2D(string text, RenderFont font, Point2D location, IPlatformTextMetrics metrics)
            : this(text, font, location, new Size2D(int.MaxValue, int.MaxValue), metrics)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text2D"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="location">The location.</param>
        /// <param name="limits">The limits.</param>
        /// <param name="metrics">The metrics.</param>
        public Text2D(string text, RenderFont font, Point2D location, Size2D limits, IPlatformTextMetrics metrics)
        {
            Text = text;
            Font = font;
            Location = location;
            Limits = limits;
            Metrics = metrics;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public RenderFont Font { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// Gets or sets the limits.
        /// </summary>
        public Size2D Limits { get; set; }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        public override Rectangle2D Bounds
            => new Rectangle2D(Location, Metrics.MeasureString(Text, Font, (int)Limits.Width));

        /// <summary>
        /// Gets or sets the metrics.
        /// </summary>
        public IPlatformTextMetrics Metrics { get; set; }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Contains(Point2D point)
            => Intersections.Contains(Bounds, point) != Inclusions.Outside;

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
        /// <param name="formatProvider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ConvertToString(string format, IFormatProvider formatProvider)
        {
            if (this is null)
            {
                return nameof(Text2D);
            }
            //var sep = ',';// Tokenizer.GetNumericListSeparator(formatProvider);
            IFormattable formatable = $"{nameof(Text2D)}{{{nameof(Text)}=\"{Text}\",{nameof(Font)}={Font},{nameof(Location)}={Location},{nameof(Limits)}={Limits}}}";
            return formatable.ToString(format, formatProvider);
        }
    }
}
