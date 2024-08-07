﻿// <copyright file="Bone.cs" company="Shkyrockett" >
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
/// The bone class.
/// </summary>
public class Bone
    : Shape2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Bone"/> class.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="iK">The i k.</param>
    /// <param name="length">The length.</param>
    /// <param name="weight">The weight.</param>
    /// <param name="chain">The chain.</param>
    /// <param name="chainIndex">Index of the chain.</param>
    /// <param name="inheritTranslation">if set to <see langword="true"/> [inherit translation].</param>
    /// <param name="inheritRotation">if set to <see langword="true"/> [inherit rotation].</param>
    /// <param name="inheritScale">if set to <see langword="true"/> [inherit scale].</param>
    /// <param name="bendPositive">if set to <see langword="true"/> [bend positive].</param>
    public Bone(Transform2D transform, Bone parent, Bone iK, double length, double weight, uint chain, int chainIndex, bool inheritTranslation, bool inheritRotation, bool inheritScale, bool bendPositive)
    {
        Transform = transform;
        Parent = parent;
        IK = iK;
        Length = length;
        Weight = weight;
        Chain = chain;
        ChainIndex = chainIndex;
        InheritTranslation = inheritTranslation;
        InheritRotation = inheritRotation;
        InheritScale = inheritScale;
        BendPositive = bendPositive;
    }

    /// <summary>
    /// Gets or sets the transform.
    /// </summary>
    public Transform2D Transform { get; set; }

    /// <summary>
    /// Gets or sets the parent.
    /// </summary>
    public Bone Parent { get; set; }

    /// <summary>
    /// Gets or sets the IK.
    /// </summary>
    public Bone IK { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    public double Length { get; set; }

    /// <summary>
    /// Gets or sets the weight.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Gets or sets the chain.
    /// </summary>
    public uint Chain { get; set; }

    /// <summary>
    /// Gets or sets the chain index.
    /// </summary>
    public int ChainIndex { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    public bool InheritTranslation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    public bool InheritRotation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    public bool InheritScale { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    public bool BendPositive { get; set; }
}
