// <copyright file="Armature.cs" company="Shkyrockett" >
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
/// The armature class.
/// </summary>
public class Armature
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bones"></param>
    public Armature(List<Bone> bones)
    {
        Bones = bones ?? [];
    }

    /// <summary>
    /// Gets or sets the bones.
    /// </summary>
    public List<Bone> Bones { get; set; }
}
