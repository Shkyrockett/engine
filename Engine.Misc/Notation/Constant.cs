// <copyright file="Constant.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public class Constant
        :Term
    {
        /// <summary>
        /// 
        /// </summary>
        public Constant()
        {
            Variables = new List<Variable>();
        }

        /// <summary>
        /// 
        /// </summary>
        private new List<Variable> Variables { get; set; }
    }
}
