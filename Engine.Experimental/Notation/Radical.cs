// <copyright file="Radical.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.MathNotation
{
    /// <summary>
    /// The radical class.
    /// </summary>
    public class Radical
        : Variable
    {
        /// <summary>
        /// The index of the Root.
        /// </summary>
        /// <remarks>
        /// <para>http://tutorial.math.lamar.edu/Classes/Alg/Radicals.aspx</para>
        /// </remarks>
        public Expression Index { get; set; }

        /// <summary>
        /// The contents of the Root.
        /// </summary>
        /// <remarks>
        /// <para>http://tutorial.math.lamar.edu/Classes/Alg/Radicals.aspx</para>
        /// </remarks>
        public Expression Radicand { get; set; }
    }
}
