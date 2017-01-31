// <copyright file="ParallelLocus.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine
{
    /// <summary>
    /// <see cref="ParallelLocus"/> is a special case of the <see cref="EmptyLocus"/> where there are no intersections because lines are exactly parallel.
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class ParallelLocus
        : EmptyLocus
    {
        /// <summary>
        /// Initializes a default instance of the <see cref="ParallelLocus"/> class.
        /// </summary>
        public ParallelLocus()
        { }
    }
}
