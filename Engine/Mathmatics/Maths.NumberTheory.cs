using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The maths class.
    /// </summary>
    public static partial class Maths
    {
        /// <summary>
        /// The greatest common denominator. 
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/Articles/76878/Spirograph-Shapes-WPF-Bezier-Shapes-from-Math-Form
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GreatestCommonDenomonator(int a, int b)
        {
            int temp;
            while (b != 0)
            {
                temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// The least common denominator. 
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/Articles/76878/Spirograph-Shapes-WPF-Bezier-Shapes-from-Math-Form
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeastCommonDenomonator(int a, int b)
            => a * b / GreatestCommonDenomonator(a, b);
    }
}
