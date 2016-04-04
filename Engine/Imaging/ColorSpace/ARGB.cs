using System.Drawing;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// Alpha Red Green Blue color class.
    /// </summary>
    public class ARGB
    {
        /// <summary>
        /// Red color component
        /// </summary>
        private byte red;

        /// <summary>
        /// Green color component.
        /// </summary>
        private byte green;

        /// <summary>
        /// Blue color component.
        /// </summary>
        private byte blue;

        /// <summary>
        /// Alpha color component.
        /// </summary>
        private byte alpha;

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        public ARGB()
            : this(0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        public ARGB(Color value)
            : this(value.A, value.R, value.G, value.B)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        public ARGB(byte red, byte green, byte blue)
            : this(0, red, green, blue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        public ARGB(byte alpha, byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        /// <summary>
        /// Gets or sets the red color value.
        /// </summary>
        public byte Red
        {
            get { return red; }
            set { red = value; }
        }

        /// <summary>
        /// Gets or sets the green color value.
        /// </summary>
        public byte Green
        {
            get { return green; }
            set { green = value; }
        }

        /// <summary>
        /// Gets or sets the blue color value.
        /// </summary>
        public byte Blue
        {
            get { return blue; }
            set { blue = value; }
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /// <summary>
        /// Convert between a <see cref="Color"/> and a <see cref="ARGB"/> structures.
        /// </summary>
        /// <param name="argb">A fully qualified <see cref="ARGB"/> structure.</param>
        /// <returns>Returns a fully qualified <see cref="Color"/> structure.</returns>
        public static implicit operator Color(ARGB argb)
        {
            return Color.FromArgb(argb.alpha, argb.red, argb.green, argb.blue);
        }

        /// <summary>
        /// Convert between a <see cref="ARGB"/> and a <see cref="Color"/> structures.
        /// </summary>
        /// <param name="color">A fully qualified <see cref="Color"/> structure.</param>
        /// <returns>Returns a fully qualified <see cref="ARGB"/> structure.</returns>
        public static explicit operator ARGB(Color color)
        {
            return new ARGB(color);
        }
    }
}
