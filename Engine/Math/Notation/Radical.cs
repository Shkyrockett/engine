// <copyright file="Radical.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

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
