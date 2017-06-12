// <copyright file="ReflectionHelper.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MethodSpeedTester
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodInfo> ListStaticFactoryConstructors(Type type)
            => new List<MethodInfo>
            (
                from method in type.GetMethods()
                where method.IsStatic
                where method.ReturnType == type
                select method
            ).OrderBy(x => x.Name).ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static List<MethodInfo> ListStaticFactoryConstructorsList(Type type, Type type2)
            => new List<MethodInfo>
            (
                from method in type.GetMethods()
                where method.IsStatic
                where method.ReturnType == type2
                select method
            ).OrderBy(x => x.Name).ToList();
    }
}
