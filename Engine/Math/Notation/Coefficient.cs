using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Coefficient
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Term"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The value of the <see cref="Coefficient"/>.
        /// </summary>
        public decimal Value { get; set; }
    }
}
