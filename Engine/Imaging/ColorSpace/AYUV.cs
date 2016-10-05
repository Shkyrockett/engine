// <copyright file="AYUV.cs" >
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Drawing;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    ///
    /// </summary>
    public class AYUV
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly AYUV Empty = new AYUV();

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        public AYUV()
            : this(0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        public AYUV(Color value)
        {
            Y = (byte)(0.299 * value.R + 0.587 * value.G + 0.114 * value.B);
            U = (byte)(-0.1687 * value.R - 0.3313 * value.G + 0.5 * value.B + 128);
            V = (byte)(0.5 * value.R - 0.4187 * value.G - 0.0813 * value.B + 128);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        /// <param name="y">Y color component.</param>
        /// <param name="u">U color component.</param>
        /// <param name="v">V color component.</param>
        public AYUV(byte y, byte u, byte v)
            : this(0, y, u, v)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="y">Y color component.</param>
        /// <param name="u">U color component.</param>
        /// <param name="v">V color component.</param>
        public AYUV(byte alpha, byte y, byte u, byte v)
        {
            Y = y;
            U = u;
            V = v;
            Alpha = alpha;
        }

        /// <summary>
        ///
        /// </summary>
        public byte Y { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte V { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte U { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Color ToColor() => Color.FromArgb(Alpha,
    (byte)(Y + 0 * U + 1.13983 * V),
    (byte)(Y + -0.39465 * U + -0.58060 * V),
    (byte)(Y + -0.03211 * U + 0 * V));

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Color ToColorRounded()
        {
            var r = (byte)(Y + 1.140 * V);
            var g = (byte)(Y - 0.395 * U - 0.581 * V);
            var b = (byte)(Y + 2.032 * U);
            return Color.FromArgb(Alpha, r, g, b);
        }
    }
}
