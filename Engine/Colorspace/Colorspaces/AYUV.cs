// <copyright file="AYUV.cs" company="Shkyrockett" >
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
    ///
    /// </summary>
    public struct AYUV
        : IColor<AYUV>
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly AYUV Empty = new AYUV();

        ///// <summary>
        ///// Initializes a new instance of the <see cref="AYUV"/> class.
        ///// </summary>
        //public AYUV()
        //    : this(0, 0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        public AYUV(ARGB value)
        {
            Alpha = value.Alpha;
            Y = (byte)(0.299 * value.Red + 0.587 * value.Green + 0.114 * value.Blue);
            U = (byte)(-0.1687 * value.Red - 0.3313 * value.Green + 0.5 * value.Blue + 128);
            V = (byte)(0.5 * value.Red - 0.4187 * value.Green - 0.0813 * value.Blue + 128);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        /// <param name="y">Y color component.</param>
        /// <param name="u">U color component.</param>
        /// <param name="v">V color component.</param>
        public AYUV(byte y, byte u, byte v)
            : this(0, y, u, v)
        { }

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
        public ARGB ToColor() => new ARGB(Alpha,
            (byte)(Y + 0 * U + 1.13983 * V),
            (byte)(Y + -0.39465 * U + -0.58060 * V),
            (byte)(Y + -0.03211 * U + 0 * V));

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ARGB ToColorRounded()
        {
            var r = (byte)(Y + 1.140 * V);
            var g = (byte)(Y - 0.395 * U - 0.581 * V);
            var b = (byte)(Y + 2.032 * U);
            return new ARGB(Alpha, r, g, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AYUV other)
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
