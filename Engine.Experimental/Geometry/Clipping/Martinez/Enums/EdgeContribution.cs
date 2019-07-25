// <copyright file="EdgeContributions.cs" >
//     Copyright © 2012 Francisco Martínez del Río. All rights reserved.
// </copyright>
// <author id="fmartin@ujaen.es">Francisco Martínez del Río</author>
// <license>
//     This code is public domain.
// </license>
// <summary></summary>
// <remarks> http://www4.ujaen.es/~fmartin/bool_op.html </remarks>

namespace Engine
{
    /// <summary>
    /// An enumeration of the contribution of overlapping edge types.
    /// </summary>
    public enum EdgeContribution
        : byte
    {
        /// <summary>
        /// The edge is normal.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// The edge does not contribute to the clipping.
        /// </summary>
        NonContributing = 1,

        /// <summary>
        /// The edge has the same transition.
        /// </summary>
        SameTransition = 2,

        /// <summary>
        /// The edge has a different transition.
        /// </summary>
        DifferentTransition = 3
    }
}
