// <copyright file="ACYMK.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.Colorspace
{
    /// <summary>
    /// ACYMK color class.
    /// </summary>
    public struct ACYMK
        : IColor<ACYMK>
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly ACYMK Empty = new ACYMK();

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ACYMK"/> class.
        ///// </summary>
        //public ACYMK()
        //    : this(0, 0, 0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACYMK"/> class.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        /// <remarks>
        /// RGB --> CMYK
        /// Black   = minimum(1-Red,1-Green,1-Blue)
        /// Cyan    = (1-Red-Black)/(1-Black)
        /// Magenta = (1-Green-Black)/(1-Black)
        /// Yellow  = (1-Blue-Black)/(1-Black)
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </remarks>
        public ACYMK(ARGB color)
        {
            var red = 1d - (color.Red / 255d);
            var green = 1d - (color.Green / 255d);
            var blue = 1d - (color.Blue / 255d);

            var K = red < green ? red : green;
            K = blue < K ? blue : K;

            var C = (red - K) / (1.0 - K);
            var M = (green - K) / (1.0 - K);
            var Y = (blue - K) / (1.0 - K);

            Alpha = color.Alpha;
            Cyan = (byte)((C * 100) + 0.5);
            Yellow = (byte)((Y * 100) + 0.5);
            Magenta = (byte)((M * 100) + 0.5);
            Black = (byte)((K * 100) + 0.5);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACYMK"/> class.
        /// </summary>
        /// <param name="cyan">Cyan color component.</param>
        /// <param name="yellow">Yellow color component.</param>
        /// <param name="magenta">Magenta color component.</param>
        /// <param name="black">Black color component.</param>
        public ACYMK(byte cyan, byte yellow, byte magenta, byte black)
            : this(0, cyan, yellow, magenta, black)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACYMK"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="cyan">Cyan color component.</param>
        /// <param name="yellow">Yellow color component.</param>
        /// <param name="magenta">Magenta color component.</param>
        /// <param name="black">Black color component.</param>
        public ACYMK(byte alpha, byte cyan, byte yellow, byte magenta, byte black)
        {
            Cyan = cyan;
            Yellow = yellow;
            Magenta = magenta;
            Black = black;
            Alpha = alpha;
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        /// Gets or sets the cyan color value.
        /// </summary>
        public byte Cyan { get; set; }

        /// <summary>
        /// Gets or sets the green color value.
        /// </summary>
        public byte Yellow { get; set; }

        /// <summary>
        /// Gets or sets the blue color value.
        /// </summary>
        public byte Magenta { get; set; }

        /// <summary>
        /// Gets or sets the black color value.
        /// </summary>
        public byte Black { get; set; }

        /// <summary>
        /// Converts the <see cref="ACYMK"/> class to a <see cref="ARGB"/> class.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// CMYK --> RGB
        /// Red   = 1-minimum(1,Cyan*(1-Black)+Black)
        /// Green = 1-minimum(1,Magenta*(1-Black)+Black)
        /// Blue  = 1-minimum(1,Yellow*(1-Black)+Black)
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </remarks>
        public ARGB ToARGB()
        {
            var C = Cyan / 255d;
            var M = Magenta / 255d;
            var Y = Yellow / 255d;
            var K = Black / 255d;

            var R = C * (1d - K) + K;
            var G = M * (1d - K) + K;
            var B = Y * (1d - K) + K;

            R = (1d - R) * 255d + 0.5d;
            G = (1d - G) * 255d + 0.5d;
            B = (1d - B) * 255d + 0.5d;

            return new ARGB((byte)R, (byte)G, (byte)B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ACYMK other)
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => throw new NotImplementedException();
    }
}
