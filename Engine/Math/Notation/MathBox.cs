using Engine.Geometry;
using Engine.Objects;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class MathBox
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Expression"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The equation to render.
        /// </summary>
        public Equation Equation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void BuildTable()
        {

        }
    }
}
