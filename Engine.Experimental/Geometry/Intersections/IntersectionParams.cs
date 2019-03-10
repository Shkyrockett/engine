// <copyright file="IntersectionParams.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The intersection params struct.
    /// </summary>
    public struct IntersectionParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionParams"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="params">The params.</param>
        public IntersectionParams(string name, string @params)
        {
            this.Name = name;
            this.Params = @params;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the params.
        /// </summary>
        public string Params { get; set; }
    }
}
