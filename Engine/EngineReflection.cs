// <copyright file="EngineEx.cs">
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineReflection
    {
        /// <summary>
        /// Add attributes to framework components so they can be listed.
        /// </summary>
        /// <remarks>
        /// https://social.msdn.microsoft.com/Forums/en-US/3c73a473-2e1e-4e2c-8da8-c127f68dabdc/adding-custom-attributes-to-properties-at-run-time-in-c?forum=netfxbcl
        /// </remarks>
        static EngineReflection()
        {
            // Attempt to inject custom attributes to try to pick up native graphics types.
            TypeDescriptor.AddAttributes(typeof(Size), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(SizeF), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(Point), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(PointF), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(Rectangle), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(RectangleF), new GraphicsObjectAttribute());
        }

        /// <summary>
        /// List all objects derived from the <see cref="Shape"/> class.
        /// </summary>
        /// <returns>A list of types that are derived from the <see cref="Shape"/> class.</returns>
        public static List<Type> ListShapes()
        {
            Type shapeType = typeof(Shape);
            Assembly assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyTypes(assembly, shapeType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Tool"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Tool"/> Class.</returns>
        public static List<Type> ListTools()
        {
            Type toolType = typeof(Tool);
            Assembly assembly = Assembly.GetAssembly(toolType);
            return GetAssemblyTypes(assembly, toolType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Brush"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Brush"/> Class.</returns>
        public static List<Type> ListBrushes()
        {
            Type brushType = typeof(Brush);
            Assembly assembly = Assembly.GetAssembly(brushType);
            return GetAssemblyTypes(assembly, brushType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Pen"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Pen"/> Class.</returns>
        public static List<Type> ListPens()
        {
            Type penType = typeof(Pen);
            Assembly assembly = Assembly.GetAssembly(penType);
            return GetAssemblyTypes(assembly, penType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Tool"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Tool"/> Class.</returns>
        public static List<Type> ListFileObjects()
        {
            Type objectType = typeof(FileObjectAttribute);
            Assembly assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypeAttributes(assembly, objectType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Tool"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Tool"/> Class.</returns>
        public static List<Type> ListGraphicsObjects()
        {
            //// ToDo: Figure out why the injected attributes aren't getting picked up in reflection.
            //TypeDescriptor.AddAttributes(typeof(Rectangle), new GraphicsObjectAttribute());
            //TypeDescriptor.AddAttributes(typeof(RectangleF), new GraphicsObjectAttribute());
            //var test = TypeDescriptor.GetAttributes(typeof(Rectangle));

            Type objectType = typeof(GraphicsObjectAttribute);
            Assembly assembly = Assembly.GetAssembly(objectType);
            List<Type> types = GetAssemblyTypeAttributes(assembly, objectType);
            //assembly = Assembly.GetAssembly(typeof(Rectangle));
            //types.AddRange(GetAssemblyAttributes(assembly, objectType));
            return types;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodInfo> ListStaticFactoryConstructors(Type type)
        {
            return new List<MethodInfo>( from method in type.GetMethods()
                where method.IsStatic == true
                where method.ReturnType == type
                select method);
        }

        /// <summary>
        /// List all of the assembly types of a specific type.
        /// </summary>
        /// <param name="assembly">The assembly to look in.</param>
        /// <param name="classType">The type to look for.</param>
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

        /// <summary>
        /// List all of the assembly types based on a particular interface. 
        /// </summary>
        /// <param name="assembly">The assembly to look in.</param>
        /// <param name="classType">The interface type to look for.</param>
        /// <returns></returns>
        private static List<Type> GetAssemblyInterfaces(Assembly assembly, Type classType)
        {
            List<Type> typeList = new List<Type>();
            foreach (Type type in assembly.GetTypes().Where(t => t.GetInterfaces().Contains(classType)).ToArray())
            {
                typeList.Add(type);
            }

            return typeList;
        }

        /// <summary>
        /// List all of the assembly types based on a particular attribute. 
        /// </summary>
        /// <param name="assembly">The assembly to look in.</param>
        /// <param name="attributeType">The attribute type to look for.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4852879/get-all-types-in-assembly-with-custom-attribute
        /// </remarks>
        private static List<Type> GetAssemblyTypeAttributes(Assembly assembly, Type attributeType)
        {
            List<Type> typeList = new List<Type>();

            var types = from type in assembly.GetTypes()
                        where Attribute.IsDefined(type, attributeType)
                        select type;

            foreach (Type type in types.ToArray())
            {
                typeList.Add(type);
            }

            return typeList;
        }
    }
}
