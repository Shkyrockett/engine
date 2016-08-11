using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Variable
        : GraphicsObject
    {
        /// <summary>
        /// The location the Variable should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The superscripted exponential expression.
        /// </summary>
        public Expression Exponent { get; set; }

        /// <summary>
        /// The name of the <see cref="Variable"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The subscripted sequence identifier.
        /// </summary>
        public Expression Sequence { get; set; }
    }
}
