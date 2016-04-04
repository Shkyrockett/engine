// <copyright file="AYUV.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2015 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Alma Jenks</author>
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
        /// y color component
        /// </summary>
        private byte y;

        /// <summary>
        /// u color component.
        /// </summary>
        private byte u;

        /// <summary>
        /// v color component.
        /// </summary>
        private byte v;

        /// <summary>
        /// Alpha color component.
        /// </summary>
        private byte alpha;

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        public AYUV()
            : this(0, 0, 0, 0)
        {
        }

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
        public byte Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte V
        {
            get { return v; }
            set { v = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte U
        {
            get { return u; }
            set { u = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            return Color.FromArgb(alpha,
                (byte)(Y + 0 * U + 1.13983 * V),
                (byte)(Y + -0.39465 * U + -0.58060 * V),
                (byte)(Y + -0.03211 * U + 0 * V));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color ToColorRounded()
        {
            byte r = (byte)(y + 1.140 * v);
            byte g = (byte)(y - 0.395 * u - 0.581 * v);
            byte b = (byte)(y + 2.032 * u);
            return Color.FromArgb(alpha, r, g, b);
        }
    }
}
