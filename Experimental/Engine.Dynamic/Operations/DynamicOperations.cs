using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamicOperations
    {
        /// <summary>
        /// Finds the length of a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion.</param>
        /// <returns>Returns the length of the Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Quaternion4D quaternion) => QuaternionMagnitude(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Finds the square distance of the length of a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion.</param>
        /// <returns>Returns the length of a Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this Quaternion4D quaternion) => QuaternionNormal(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }
}
