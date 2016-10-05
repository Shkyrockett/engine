// <copyright file="Variable.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class Variable
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
    }
}
