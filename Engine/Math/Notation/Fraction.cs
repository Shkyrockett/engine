using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Fraction
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Logarithm"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The top number of the <see cref="Fraction"/>.
        /// </summary>
        public Expression Numerator { get; set; }

        /// <summary>
        /// The bottom number of the <see cref="Fraction"/>.
        /// </summary>
        public Expression Denominator { get; set; }
    }
}
