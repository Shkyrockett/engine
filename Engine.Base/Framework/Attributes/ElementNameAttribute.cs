﻿// <copyright file="ElementNameAttribute.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// https://msdn.microsoft.com/en-us/library/84c42s56(v=vs.110).aspx
// </references>

namespace Engine;

/// <summary>
/// The element name attribute class.
/// </summary>
/// <seealso cref="Attribute" />
/// <acknowledgment>
/// https://msdn.microsoft.com/en-us/library/84c42s56(v=vs.110).aspx
/// </acknowledgment>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ElementNameAttribute
    : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementNameAttribute" /> class.
    /// </summary>
    /// <param name="v">The v.</param>
    public ElementNameAttribute(string v)
    {
        V = v;
    }

    /// <summary>
    /// Gets the v.
    /// </summary>
    /// <value>
    /// The v.
    /// </value>
    public string V { get; }
}
