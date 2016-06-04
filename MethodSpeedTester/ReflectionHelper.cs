﻿// <copyright file="ReflectionHelper.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MethodSpeedTester
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodInfo> ListStaticFactoryConstructors(Type type)
        {
            return new List<MethodInfo>
            (
                from method in type.GetMethods()
                where method.IsStatic == true
                where method.ReturnType == type
                select method
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static List<MethodInfo> ListStaticFactoryConstructorsList(Type type, Type type2)
        {
            return new List<MethodInfo>
            (
                from method in type.GetMethods()
                where method.IsStatic == true
                where method.ReturnType == type2
                select method
            );
        }
    }
}