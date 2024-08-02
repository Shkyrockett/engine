// <copyright file="GeometryAngleDegreesAttribute.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// Attribute used to mark properties as angles to later add the AngleEditor for WinForms PropertyGrid.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class GeometryAngleDegreesAttribute
    : Attribute
{ }