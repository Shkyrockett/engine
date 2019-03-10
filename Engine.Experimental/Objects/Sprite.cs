// <copyright file="Sprite.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The sprite class.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// The graphic objects.
        /// </summary>
        public List<Shape> GraphicObjects;

        /// <summary>
        /// The state.
        /// </summary>
        public object State;
    }
}
