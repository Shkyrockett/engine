// <copyright file="ISpeed.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public interface ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        double Value { get; /*set;*/ }

        ///// <summary>
        ///// 
        ///// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Abreviation { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
