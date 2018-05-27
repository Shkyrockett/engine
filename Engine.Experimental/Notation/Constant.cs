// <copyright file="Constant.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine.MathNotation
{
    /// <summary>
    /// The constant class.
    /// </summary>
    public class Constant
        :Term
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Constant"/> class.
        /// </summary>
        public Constant()
        {
            Variables = new List<Variable>();
        }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        private new List<Variable> Variables { get; set; }
    }
}
