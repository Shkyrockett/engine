﻿// <copyright file="Pose.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The pose class.
    /// </summary>
    public class Pose
    {
        public Pose(Bone[] bones)
        {
            Bones = bones ?? new Bone[] { };
        }

        /// <summary>
        /// Gets or sets the bones.
        /// </summary>
        internal Bone[] Bones { get; set; }
    }
}
