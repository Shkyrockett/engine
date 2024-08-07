﻿// <copyright file="AdvBrowsableOrderAttribute.cs" company="" >
// Copyright © 2005 - 2007 Jonathan Mark Porter.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The adv browsable order attribute class.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class AdvBrowsableOrderAttribute
    : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvBrowsableOrderAttribute" /> class.
    /// </summary>
    /// <param name="order">The order.</param>
    public AdvBrowsableOrderAttribute(string order)
        : this(order?.Split(','))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvBrowsableOrderAttribute" /> class.
    /// </summary>
    /// <param name="order">The order.</param>
    public AdvBrowsableOrderAttribute(params string[] order)
    {
        Order = order;
    }

    /// <summary>
    /// Gets the order.
    /// </summary>
    /// <value>
    /// The order.
    /// </value>
    public string[] Order { get; }

    /// <summary>
    /// Get the order.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    public static string[] GetOrder(Type t)
    {
        var arr = t?.GetCustomAttributes(typeof(AdvBrowsableOrderAttribute), false);
        return arr?.Length > 0 ? ((AdvBrowsableOrderAttribute)arr[0]).Order : null;
    }
}
