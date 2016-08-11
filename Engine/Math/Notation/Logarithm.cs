using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Logarithm
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Logarithm"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The base of the <see cref="Logarithm"/>.
        /// </summary>
        public Expression Base { get; set; }

        /// <summary>
        /// The expression the group should contain.
        /// </summary>
        public Expression Expression { get; set; }
    }
}
