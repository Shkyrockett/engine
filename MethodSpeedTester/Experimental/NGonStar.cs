// <copyright file="Star.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>http://csharphelper.com/blog/2015/05/draw-stars-inside-polygons-in-c/</summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("NGonStar")]
    public class NGonStar
        : Contour
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "NGonStar";
            return "NGonStar";
        }
    }
}
