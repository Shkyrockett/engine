// <copyright file="Star.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>http://csharphelper.com/blog/2015/05/draw-stars-inside-polygons-in-c/</summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The n gon star class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(NGonStar))]
    public class NGonStar
        : PolygonContour
    {
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return nameof(NGonStar);
            }

            return nameof(NGonStar);
        }
    }
}
