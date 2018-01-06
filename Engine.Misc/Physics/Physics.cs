// <copyright file="Physics.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public class Physics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double AddVelocities(double v1, double v2, double c = 299790000d)
            => v1 + v2 / (1 + v1 * v2 / c * c);
    }
}
