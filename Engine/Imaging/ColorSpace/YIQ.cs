using System.Drawing;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// 
    /// </summary>
    public class YIQ
    {
        /// <summary>
        /// 
        /// </summary>
        private byte alpha;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double i;

        /// <summary>
        /// 
        /// </summary>
        private double q;

        /// <summary>
        /// 
        /// </summary>
        public YIQ()
            :this(0,0,0,0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <remarks>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </remarks>
        public YIQ(Color color)
        {
            double r = color.R;
            double g = color.G;
            double b = color.B;

            alpha = color.A;
            y = 0.299900 * r + 0.587000 * g + 0.114000 * b;
            i = 0.595716 * r - 0.274453 * g - 0.321264 * b;
            q = 0.211456 * r - 0.522591 * g + 0.311350 * b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="q"></param>
        public YIQ(double y, double i, double q)
            :this(0,0,0,0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="q"></param>
        public YIQ(byte alpha,double y, double i, double q)
        {
            this.alpha = alpha;
            this.y = y;
            this.i = i;
            this.q = q;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double I
        {
            get { return i; }
            set { i = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Q
        {
            get { return q; }
            set { q = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </remarks>
        public Color ToColor(byte a, double y, double i, double q)
        {
            byte r = (byte)(y + 0.9563 * i + 0.6210 * q);
            byte g = (byte)(y - 0.2721 * i - 0.6474 * q);
            byte b = (byte)(y - 1.1070 * i + 1.7046 * q);

            return Color.FromArgb(a, r, g, b);
        }
    }
}
