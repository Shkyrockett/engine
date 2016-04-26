namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// Lightness and Channels A and B color spaces.
    /// </summary>
    public class CIELAB
    {
        /// <summary>
        /// Lightness component.
        /// </summary>
        private double lightness;

        /// <summary>
        /// Channel A.
        /// </summary>
        private double channelA;

        /// <summary>
        /// Channel B.
        /// </summary>
        private double channelB;

        /// <summary>
        /// Initializes a new instance of the Lightness and Channels A and B color space structure.
        /// </summary>
        public CIELAB()
            : this(0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Lightness and Channels A and B color space structure.
        /// </summary>
        /// <param name="lightness">Lightness component.</param>
        /// <param name="a">Channel A.</param>
        /// <param name="b">Channel B.</param>
        public CIELAB(byte lightness, byte a, byte b)
        {
            this.lightness = lightness;
            channelA = a;
            channelB = b;
        }

        /// <summary>
        /// Lightness component.
        /// </summary>
        public double Lightness
        {
            get { return lightness; }
            set { lightness = value; }
        }

        /// <summary>
        /// Channel A.
        /// </summary>
        public double ChannelA
        {
            get { return channelA; }
            set { channelA = value; }
        }

        /// <summary>
        /// Channel B.
        /// </summary>
        public double ChannelB
        {
            get { return channelB; }
            set { channelB = value; }
        }
    }
}
