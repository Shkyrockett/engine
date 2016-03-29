// <copyright file="EngineEx.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Type> ListShapes()
        {
            Type shapeType = typeof(Shape);
            Assembly assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyTypes(assembly, shapeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Type> ListTools()
        {
            Type toolType = typeof(Tool);
            Assembly assembly = Assembly.GetAssembly(toolType);
            return GetAssemblyTypes(assembly, toolType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static List<Type> GetAssemblyTypes(Assembly assembly, Type classType)
        {
            List<Type> typeList = new List<Type>();
            foreach (Type type in assembly.GetTypes().ToArray())
            {
                if (type.BaseType == classType)
                {
                    typeList.Add(type);
                }
            }

            return typeList;
        }
    }
}
