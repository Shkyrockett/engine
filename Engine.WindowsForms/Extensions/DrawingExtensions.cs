using Engine.Colorspace;
using System.Drawing;

namespace Engine.WindowsForms
{
    /// <summary>
    /// 
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(this IColor color)
        {
            (byte A, byte R, byte G, byte B) = color.ToRGBATuple();
            return Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static RGBA ToARGB(this Color color)
            => new RGBA(color.R, color.G, color.B, color.A);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public static Brush ToBrush(this IFill brush)
            => new SolidBrush(brush.Color.ToColor());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <returns></returns>
        public static Pen ToPen(this IStroke pen)
            => new Pen(pen.Fill.Color.ToColor());//new Pen(pen.Brush.ToBrush());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image ToBitmap(this IBitmap image)
            => Image.FromStream(image.Stream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public static WinformsRenderer ToWinformsRenderer(this Graphics graphics)
            => new WinformsRenderer(graphics);
    }
}
