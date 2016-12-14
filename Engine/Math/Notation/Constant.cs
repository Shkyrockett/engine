// <copyright file="Constant.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

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
