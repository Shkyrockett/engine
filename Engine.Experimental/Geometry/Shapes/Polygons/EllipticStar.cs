// <copyright file="Star.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>http://csharphelper.com/blog/2014/08/draw-a-star-in-c/</summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The elliptic star class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("ElipticStar")]
    public class EllipticStar
        : PolygonContour2D
    {
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return "ElipticStar";
            }

            return "ElipticStar";
        }
    }
}
