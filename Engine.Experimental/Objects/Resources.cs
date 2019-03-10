// <copyright file="Resources.cs" company="Shkyrockett" >
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
    /// The resources class.
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// The fonts.
        /// </summary>
        public List<Sprite> Fonts;

        /// <summary>
        /// The sprites.
        /// </summary>
        public List<Sprite> Sprites;

        /// <summary>
        /// The audio.
        /// </summary>
        public List<IAudio> Audio;
    }
}
