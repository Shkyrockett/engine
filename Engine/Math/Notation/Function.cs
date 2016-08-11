using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Function
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Function"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The name of the <see cref="Function"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The input expression of the <see cref="Function"/>.
        /// </summary>
        public Expression Input { get; set; }

        /// <summary>
        /// The output expression of the <see cref="Function"/>.
        /// </summary>
        public Expression Output { get; set; }

        /// <summary>
        /// The range expression of the <see cref="Function"/>.
        /// </summary>
        /// <remarks>
        /// http://www.mathsisfun.com/sets/domain-range-codomain.html
        /// </remarks>
        public Range Range { get; set; }

        /// <summary>
        /// The domain expression of the <see cref="Function"/>.
        /// </summary>
        /// <remarks>
        /// http://www.mathsisfun.com/sets/domain-range-codomain.html
        /// </remarks>
        public Domain Domain { get; set; }

        /// <summary>
        /// The codomain expression of the <see cref="Function"/>.
        /// </summary>
        /// <remarks>
        /// http://www.mathsisfun.com/sets/domain-range-codomain.html
        /// </remarks>
        public Codomain Codomain { get; set; }
    }
}
