﻿// <copyright file="ReflectionHelper.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Reflection;

namespace MethodSpeedTester;

/// <summary>
/// The reflection helper class.
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// The list static factory constructors.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    public static List<MethodInfo> ListStaticFactoryConstructors(Type type)
        => [.. new List<MethodInfo>
        (
            from method in type?.GetMethods()
            where method.IsStatic
            where method.ReturnType == type
            select method
        ).OrderBy(x => x.Name)];

    /// <summary>
    /// The list static factory constructors list.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="type2">The type2.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    public static List<MethodInfo> ListStaticFactoryConstructorsList(Type type, Type type2)
        => [.. new List<MethodInfo>
        (
            from method in type?.GetMethods()
            where method.IsStatic
            where method.ReturnType == type2
            select method
        ).OrderBy(x => x.Name)];
}
