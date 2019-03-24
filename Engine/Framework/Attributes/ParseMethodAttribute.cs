// <copyright file="Vector2DConverter.cs" company="" >
//     Copyright © 2005 - 2007 Jonathan Mark Porter.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// The parse method attribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ParseMethodAttribute
        : Attribute
    {
        /// <summary>
        /// Get the parse method.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="MethodInfo"/>.</returns>
        public static MethodInfo GetParseMethod(Type t)
        {
            foreach (var method in t.GetMethods())
            {
                if (method.IsStatic &&
                    method.GetCustomAttributes(typeof(ParseMethodAttribute), true).Length > 0 &&
                    method.GetParameters().Length == 1 &&
                    method.GetParameters()[0].ParameterType == typeof(string) &&
                    method.ReturnType == t)
                {
                    return method;
                }
            }

            return null;
        }
    }
}
