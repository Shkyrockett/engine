using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// A Parenthesized group containing an expression.
    /// </summary>
    public class Group
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Group"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The Parenthesis type to use for the group.
        /// </summary>
        public GroupTypes Type { get; set; }

        /// <summary>
        /// The superscripted exponential expression.
        /// </summary>
        public Expression Exponent { get; set; }

        /// <summary>
        /// The expression the group should contain.
        /// </summary>
        public Expression Expression { get; set; }

        /// <summary>
        /// The subscripted sequence identifier.
        /// </summary>
        public Expression Sequence { get; set; }
    }
}
