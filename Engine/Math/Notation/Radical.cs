using Engine.Geometry;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Radical
        :Variable
    {
        /// <summary>
        /// The index of the Root.
        /// </summary>
        /// <remarks>
        /// http://tutorial.math.lamar.edu/Classes/Alg/Radicals.aspx
        /// </remarks>
        public Expression Index { get; set; }

        /// <summary>
        /// The contents of the Root.
        /// </summary>
        /// <remarks>
        /// http://tutorial.math.lamar.edu/Classes/Alg/Radicals.aspx
        /// </remarks>
        public Expression Radicand { get; set; }
    }
}
