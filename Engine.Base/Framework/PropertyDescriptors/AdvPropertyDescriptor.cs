﻿// <copyright file="AdvPropertyDescriptor.cs" company="" >
// Copyright © 2005 - 2007 Jonathan Mark Porter.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Engine;

/// <summary>
/// The adv property descriptor class.
/// </summary>
public class AdvPropertyDescriptor
    : PropertyDescriptor, IEquatable<AdvPropertyDescriptor>
{
    #region Fields
    /// <summary>
    /// The info.
    /// </summary>
    private readonly MemberInfo info;

    /// <summary>
    /// The field.
    /// </summary>
    private readonly FieldInfo field;

    /// <summary>
    /// The property.
    /// </summary>
    private readonly PropertyInfo property;

    /// <summary>
    /// The description.
    /// </summary>
    private string description;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvPropertyDescriptor"/> class.
    /// </summary>
    /// <param name="field">The field.</param>
    public AdvPropertyDescriptor(FieldInfo field)
        : this(field?.Name, field)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvPropertyDescriptor"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="field">The field.</param>
    public AdvPropertyDescriptor(string name, FieldInfo field)
        : base(name, (Attribute[])field?.GetCustomAttributes(typeof(Attribute), true))
    {
        info = field;
        this.field = field;
        description = base.Description;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvPropertyDescriptor"/> class.
    /// </summary>
    /// <param name="property">The property.</param>
    public AdvPropertyDescriptor(PropertyInfo property)
        : this(property?.Name, property)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvPropertyDescriptor"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="property">The property.</param>
    public AdvPropertyDescriptor(string name, PropertyInfo property)
        : base(name, (Attribute[])property?.GetCustomAttributes(typeof(Attribute), true))
    {
        info = property;
        this.property = property;
        this.property = property;
        description = base.Description;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets the description.
    /// </summary>
    public override string Description => description;

    /// <summary>
    /// Gets a value indicating whether
    /// </summary>
    public override bool IsReadOnly => property is not null && property.CanWrite;

    /// <summary>
    /// Gets the component type.
    /// </summary>
    public override Type ComponentType => info.DeclaringType;

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public override Type PropertyType
    {
        get
        {
            return field is null ? property.PropertyType : field.FieldType;
        }
    }
    #endregion Properties

    #region Methods
    /// <summary>
    /// The can reset value.
    /// </summary>
    /// <param name="component">The component.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public override bool CanResetValue(object component) => false;

    /// <summary>
    /// Reset the value.
    /// </summary>
    /// <param name="component">The component.</param>
    public override void ResetValue(object component) => throw new NotSupportedException();

    /// <summary>
    /// Get the value.
    /// </summary>
    /// <param name="component">The component.</param>
    /// <returns>The <see cref="object"/>.</returns>
    public override object? GetValue(object? component) => field is null ? property.GetValue(component, null) : field.GetValue(component);

    /// <summary>
    /// Set the value.
    /// </summary>
    /// <param name="component">The component.</param>
    /// <param name="value">The value.</param>
    public override void SetValue(object? component, object? value)
    {
        if (field is null)
        {
            property.SetValue(component, value, null);
        }
        else
        {
            field.SetValue(component, value);
        }
        OnValueChanged(component, EventArgs.Empty);
    }

    /// <summary>
    /// The should serialize value.
    /// </summary>
    /// <param name="component">The component.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public override bool ShouldSerializeValue(object component) => true;

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    public override int GetHashCode() => info.GetHashCode();

    /// <summary>
    /// Set the description.
    /// </summary>
    /// <param name="value">The value.</param>
    public void SetDescription(string value) => description = value;

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public override bool Equals([AllowNull] object obj) => obj is AdvPropertyDescriptor descriptor && Equals(descriptor);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Equals(AdvPropertyDescriptor? other) => info.Equals(other?.info);
    #endregion Methods
}
