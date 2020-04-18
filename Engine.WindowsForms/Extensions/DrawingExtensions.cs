using Engine.Colorspace;
using System.Drawing;

namespace Engine.WindowsForms
{
    /// <summary>
    /// The drawing extensions class.
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// Converts an <see cref="IColor"/> to a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="Color"/>.</returns>
        public static Color ToColor(this IColor color)
        {
            (var red, var green, var blue, var alpha) = (color?.ToRGBATuple()).Value;
            return Color.FromArgb(alpha, red, green, blue);
        }

        /// <summary>
        /// Converts a <see cref="Color"/> to an <see cref="RGBA"/> color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="RGBA"/>.</returns>
        public static RGBA ToRGBA(this Color color) => new RGBA(color.R, color.G, color.B, color.A, color.Name);

        /// <summary>
        /// Converts an <see cref="IFill"/> to a <see cref="Brush"/>.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <returns>The <see cref="Brush"/>.</returns>
        public static Brush ToBrush(this IFill brush) => new SolidBrush((brush?.Color).ToColor());

        /// <summary>
        /// Converts an <see cref="IStroke"/> to a <see cref="Pen"/>.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <returns>The <see cref="Pen"/>.</returns>
        public static Pen ToPen(this IStroke pen) => new Pen((pen?.Fill).Color.ToColor());//new Pen(pen.Brush.ToBrush());

        /// <summary>
        /// Converts an <see cref="IBitmap"/> to an <see cref="Image"/>.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The <see cref="Image"/>.</returns>
        public static Image ToBitmap(this IBitmap image) => Image.FromStream(image?.Stream);

        /// <summary>
        /// Converts a Winforms <see cref="Graphics"/> to and <see cref="WinformsRenderer"/>.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <returns>The <see cref="WinformsRenderer"/>.</returns>
        public static WinformsRenderer ToWinformsRenderer(this Graphics graphics) => new WinformsRenderer(graphics);
    }
}
