﻿// <copyright file="MemberAccessor.cs" >
// Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://github.com/jacobalbano/glide </remarks>

using System.Linq.Expressions;
using System.Reflection;

namespace Engine.Tweening;

/// <summary>
/// The member accessors class.
/// </summary>
/// <seealso cref="Engine.IMemberAccessor" />
public class MemberAccessor
    : IMemberAccessor
{
    #region Constants
    /// <summary>
    /// The flags (const). Value: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static.
    /// </summary>
    private const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
    #endregion Constants

    #region Fields
    /// <summary>
    /// The get method.
    /// </summary>
    protected Func<object, object> getMethod;

    /// <summary>
    /// The set method.
    /// </summary>
    protected Action<object, object> setMethod;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberAccessor" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="name">The property.</param>
    /// <param name="writable">The writeRequired.</param>
    /// <exception cref="Exception">readable</exception>
    public MemberAccessor(object target, string name, bool writable = true)
    {
        if (target is null) return;
        Target = target;
        var targetType = target.GetType();
        MemberInfo memberInfo;

        if ((memberInfo = targetType.GetField(name, flags)) is not null)
        {
            // Capture the field member info.
            var fieldInfo = memberInfo as FieldInfo;
            MemberType = fieldInfo.FieldType;
            MemberName = fieldInfo.Name;

            // Set up for reading
            var self = Expression.Parameter(typeof(object));
            var instance = Expression.Convert(self, fieldInfo.DeclaringType);
            var field = Expression.Field(instance, fieldInfo);
            var convert = Expression.TypeAs(field, typeof(object));
            getMethod = Expression.Lambda<Func<object, object>>(convert, self).Compile();

            // Set up for writing
            if (writable)
            {
                var value = Expression.Parameter(typeof(object));
                var fieldExp = Expression.Field(Expression.Convert(self, fieldInfo.DeclaringType), fieldInfo);
                var assignExp = Expression.Assign(fieldExp, Expression.Convert(value, fieldInfo.FieldType));
                setMethod = Expression.Lambda<Action<object, object>>(assignExp, self, value).Compile();
            }
        }
        else if ((memberInfo = targetType.GetProperty(name, flags)) is not null)
        {
            // Capture the property member info.
            var propertyInfo = memberInfo as PropertyInfo;
            MemberType = propertyInfo.PropertyType;
            MemberName = propertyInfo.Name;

            // Set up for reading
            var param = Expression.Parameter(typeof(object));
            var instance = Expression.Convert(param, propertyInfo.DeclaringType);
            var convert = Expression.TypeAs(Expression.Property(instance, propertyInfo), typeof(object));
            getMethod = Expression.Lambda<Func<object, object>>(convert, param).Compile();

            // Set up for writing
            if (writable)
            {
                var argument = Expression.Parameter(typeof(object));
                var setterCall = Expression.Call(
                    Expression.Convert(param, propertyInfo.DeclaringType),
                    propertyInfo.GetSetMethod(),
                    Expression.Convert(argument, propertyInfo.PropertyType));

                setMethod = Expression.Lambda<Action<object, object>>(setterCall, param, argument).Compile();
            }
        }
        else
        {
            throw new Exception($"Field or {(writable ? "read/write" : "readable")} property '{name}' not found on object of type {targetType.FullName}.");
        }
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets the target.
    /// </summary>
    /// <value>
    /// The target.
    /// </value>
    public object Target { get; private set; }

    /// <summary>
    /// Gets the member name.
    /// </summary>
    /// <value>
    /// The name of the member.
    /// </value>
    public string MemberName { get; private set; }

    /// <summary>
    /// Gets the member type.
    /// </summary>
    /// <value>
    /// The type of the member.
    /// </value>
    public Type MemberType { get; private set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public object Value { get { return getMethod(Target); } set { setMethod(Target, value); } }
    #endregion Properties
}
