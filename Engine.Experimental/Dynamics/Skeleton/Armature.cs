// <copyright file="Armature.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// The armature class.
    /// </summary>
    public class Armature
    {
        public Armature(List<Bone> bones)
        {
            Bones = bones ?? new List<Bone>();
        }

        /// <summary>
        /// Gets or sets the bones.
        /// </summary>
        public List<Bone> Bones { get; set; }
    }
}
