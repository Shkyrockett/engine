﻿// <copyright file="InstanceConstructorAttribute.cs" company="" >
// Copyright © 2005 - 2007 Jonathan Mark Porter.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System.Reflection;

namespace Engine;

/// <summary>
/// The instance constructor attribute class.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
public sealed class InstanceConstructorAttribute
    : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceConstructorAttribute" /> class.
    /// </summary>
    /// <param name="parameterNames">The parameterNames.</param>
    public InstanceConstructorAttribute(string parameterNames)
    {
        ParameterNames = parameterNames?.Split(',');
    }

    /// <summary>
    /// Gets the parameter names.
    /// </summary>
    /// <value>
    /// The parameter names.
    /// </value>
    public string[] ParameterNames { get; }

    /// <summary>
    /// Get the constructor.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <param name="paramNames">The paramNames.</param>
    /// <returns>
    /// The <see cref="ConstructorInfo" />.
    /// </returns>
    public static ConstructorInfo GetConstructor(Type t, out string[] paramNames)
    {
        foreach (var method in t?.GetConstructors())
        {
            var atts = method?.GetCustomAttributes(typeof(InstanceConstructorAttribute), true);

            if (atts.Length > 0)
            {
                var att = (InstanceConstructorAttribute)atts[0];
                if (method?.GetParameters().Length == att.ParameterNames.Length)
                {
                    paramNames = att.ParameterNames;
                    return method;
                }
            }
        }

        paramNames = null;
        return null;
    }
}
