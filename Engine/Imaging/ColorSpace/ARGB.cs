// <copyright file="ARGB.cs" >
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
    /// Alpha Red Green Blue color class.
    /// </summary>
    public class ARGB
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly ARGB Empty = new ARGB();

        private const int AlphaShift = 24;
        private const int RedShift = 16;
        private const int GreenShift = 8;
        private const int BlueShift = 0;

        private string name;
        private int value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        public ARGB()
            : this(0, 0, 0, 0, "")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        public ARGB(Color value)
            : this(value.A, value.R, value.G, value.B, value.Name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB(int value, string name = "")
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB(byte red, byte green, byte blue, string name = "")
            : this(0, red, green, blue, name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB(byte alpha, byte red, byte green, byte blue, string name = "")
        {
            this.name = name;
            value =
                (red << RedShift
                | green << GreenShift
                | blue << BlueShift
                | alpha << AlphaShift);// & 0xffffffff;
        }

        /// <summary>
        /// Gets or sets the red color value.
        /// </summary>
        public byte Red
        {
            get { return (byte)((Value >> RedShift) & 0xFF); }
            set { this.value |= value << RedShift; }
        }

        /// <summary>
        /// Gets or sets the green color value.
        /// </summary>
        public byte Green
        {
            get { return (byte)((Value >> GreenShift) & 0xFF); }
            set { this.value |= value << GreenShift; }
        }

        /// <summary>
        /// Gets or sets the blue color value.
        /// </summary>
        public byte Blue
        {
            get { return (byte)((Value >> BlueShift) & 0xFF); }
            set { this.value |= value << BlueShift; }
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha
        {
            get { return (byte)((Value >> AlphaShift) & 0xFF); }
            set { this.value |= value << AlphaShift; }
        }

        /// <summary>
        ///
        /// </summary>
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Convert between a <see cref="Color"/> and a <see cref="ARGB"/> structures.
        /// </summary>
        /// <param name="argb">A fully qualified <see cref="ARGB"/> structure.</param>
        /// <returns>Returns a fully qualified <see cref="Color"/> structure.</returns>
        public static implicit operator Color(ARGB argb) => Color.FromArgb(argb.Alpha, argb.Red, argb.Green, argb.Blue);

        /// <summary>
        /// Convert between a <see cref="ARGB"/> and a <see cref="Color"/> structures.
        /// </summary>
        /// <param name="color">A fully qualified <see cref="Color"/> structure.</param>
        /// <returns>Returns a fully qualified <see cref="ARGB"/> structure.</returns>
        public static explicit operator ARGB(Color color) => new ARGB(color);
    }
}
