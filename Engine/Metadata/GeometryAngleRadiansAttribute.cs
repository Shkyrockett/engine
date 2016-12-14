﻿// <copyright file="GeometryAngleRadiansAttribute.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;

namespace Engine
{
    /// <summary>
    /// Attribute used to mark properties as angles to later add the AngleEditor for WinForms PropertyGrid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class GeometryAngleRadians
        : Attribute
    { }
}