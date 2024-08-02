// <copyright file="Pose.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The pose class.
/// </summary>
public class Pose
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pose"/> class.
    /// </summary>
    /// <param name="bones">The bones.</param>
    public Pose(Bone[] bones)
    {
        Bones = bones ?? [];
    }

    /// <summary>
    /// Gets or sets the bones.
    /// </summary>
    /// <value>
    /// The bones.
    /// </value>
    internal Bone[] Bones { get; set; }
}
