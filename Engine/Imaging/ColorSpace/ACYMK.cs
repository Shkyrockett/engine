using System.Drawing;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// ACYMK color class.
    /// </summary>
    public class ACYMK
    {
        /// <summary>
        /// Alpha color component.
        /// </summary>
        private byte alpha;

        /// <summary>
        /// Cyan color component
        /// </summary>
        private byte cyan;

        /// <summary>
        /// Yellow color component.
        /// </summary>
        private byte yellow;

        /// <summary>
        /// Magenta color component.
        /// </summary>
        private byte magenta;

        /// <summary>
        /// Black color component.
        /// </summary>
        private byte black;

        /// <summary>
        /// Initializes a new instance of the <see cref="ACYMK"/> class.
        /// </summary>
        public ACYMK()
            : this(0, 0, 0, 0, 0)
        {
        }

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
        public ACYMK(Color color)
        {
            double red = 1.0 - (color.R / 255.0);
            double green = 1.0 - (color.G / 255.0);
            double blue = 1.0 - (color.B / 255.0);

            double K = red < green ? red : green;
            K = blue < K ? blue : K;

            double C = (red - K) / (1.0 - K);
            double M = (green - K) / (1.0 - K);
            double Y = (blue - K) / (1.0 - K);

            alpha = color.A;
            cyan = (byte)((C * 100) + 0.5);
            yellow = (byte)((Y * 100) + 0.5);
            magenta = (byte)((M * 100) + 0.5);
            black = (byte)((K * 100) + 0.5);
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
        {
        }

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
            this.cyan = cyan;
            this.yellow = yellow;
            this.magenta = magenta;
            this.black = black;
            this.alpha = alpha;
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
        /// Gets or sets the cyan color value.
        /// </summary>
        public byte Cyan
        {
            get { return cyan; }
            set { cyan = value; }
        }

        /// <summary>
        /// Gets or sets the green color value.
        /// </summary>
        public byte Yellow
        {
            get { return yellow; }
            set { yellow = value; }
        }

        /// <summary>
        /// Gets or sets the blue color value.
        /// </summary>
        public byte Magenta
        {
            get { return magenta; }
            set { magenta = value; }
        }

        /// <summary>
        /// Gets or sets the black color value.
        /// </summary>
        public byte Black
        {
            get { return black; }
            set { black = value; }
        }

        /// <summary>
        /// Converts the <see cref="ACYMK"/> class to a <see cref="Color"/> class.
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
        public Color ToARGB()
        {
            double C = cyan / 255.0;
            double M = magenta / 255.0;
            double Y = yellow / 255.0;
            double K = black / 255.0;

            double R = C * (1.0 - K) + K;
            double G = M * (1.0 - K) + K;
            double B = Y * (1.0 - K) + K;

            R = (1.0 - R) * 255.0 + 0.5;
            G = (1.0 - G) * 255.0 + 0.5;
            B = (1.0 - B) * 255.0 + 0.5;

            return Color.FromArgb((byte)R, (byte)G, (byte)B);
        }
    }
}
