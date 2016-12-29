// <copyright file="Vector2DConverter.cs" company="" >
//     Copyright (c) 2005-2007 Jonathan Mark Porter.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ParseMethodAttribute
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static MethodInfo GetParseMethod(Type t)
        {
            foreach (MethodInfo method in t.GetMethods())
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
