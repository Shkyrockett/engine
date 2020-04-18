// <copyright file="Gear.cs" company="Shkyrockett" >
//     Copyright © 2015 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// http://csharphelper.com/blog/2015/08/animate-gears-with-unequal-sizes-in-c/
    /// http://csharphelper.com/blog/2015/08/draw-gears-in-c/
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    public class Gear
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
                return nameof(Gear);
            }

            return $"{nameof(Gear)}";
        }
    }
}
