// <copyright file="EngineReflection.cs">
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
    public static class EngineReflection
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
            // ToDo: Figure out why the injected attributes aren't getting picked up in reflection.
            TypeDescriptor.AddAttributes(typeof(Size), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(SizeF), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(Point), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(PointF), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(Rectangle), new GraphicsObjectAttribute());
            TypeDescriptor.AddAttributes(typeof(RectangleF), new GraphicsObjectAttribute());
        }

        /// <summary>
        /// Processes the properties containing specified attribute
        /// to set the UI-editor for property grid.
        /// </summary>
        /// <param name="type">The type to process.</param>
        /// <param name="searchAttribute">The property attribute to look for.</param>
        /// <param name="uiEditorAttributes">The attributes to add to the property.</param>
        /// <remarks>
        /// http://tenera-it.be/blog/2011/06/add-attriutes-to-a-property-at-runtime/
        /// </remarks>
        public static void ReplacePropertyAttribute(Type type, Attribute searchAttribute, params Attribute[] uiEditorAttributes)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(type, new Attribute[] { searchAttribute });
            foreach (PropertyDescriptor prop in props)
            {
                // AttributeArray-property is not accessible 
                // => use reflection to get and set it.
                PropertyInfo attributeArrayPropInfo = prop.GetType().GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic);
                List<Attribute> attributeArray = (attributeArrayPropInfo.GetValue(prop, null) as Attribute[]).ToList();
                attributeArray.AddRange(uiEditorAttributes);
                attributeArrayPropInfo.SetValue(prop, attributeArray.ToArray(), null);
            }
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
            Type objectType = typeof(GraphicsObjectAttribute);
            Assembly assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypeAttributes(assembly, objectType);
        }

        /// <summary>
        /// List all types that contain a property tagged with a specified attribute.
        /// </summary>
        /// <returns>The attribute to look for.</returns>
        public static List<Type> ListTypesTaggedWithPropertyAttribute(Attribute attribute)
        {
            Type objectType = attribute.GetType();
            Assembly assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypesTaggedWithPropertyAttribute(assembly, attribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Type> ListUnits(Type units)
        {
            Assembly assembly = Assembly.GetAssembly(units);
            return GetAssemblyInterfaces(assembly, units);
        }

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
        /// List all of the assembly types of a specific type.
        /// </summary>
        /// <param name="assembly">The assembly to look in.</param>
        /// <param name="classType">The type to look for.</param>
        /// <returns></returns>
        private static List<Type> GetAssemblyTypes(Assembly assembly, Type classType)
            => new List<Type>
            (
                from type in assembly.GetTypes()
                where type.BaseType == classType
                select type
            ).OrderBy(x => x.Name).ToList();

        /// <summary>
        /// List all of the assembly types based on a particular interface. 
        /// </summary>
        /// <param name="assembly">The assembly to look in.</param>
        /// <param name="classType">The interface type to look for.</param>
        /// <returns></returns>
        private static List<Type> GetAssemblyInterfaces(Assembly assembly, Type classType)
            => new List<Type>
            (
                from type in assembly.GetTypes()
                where type.GetInterfaces().Contains(classType)
                select type
            ).OrderBy(x => x.Name).ToList();

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
            => new List<Type>
            (
                from type in assembly.GetTypes()
                where Attribute.IsDefined(type, attributeType)
                select type
            ).OrderBy(x => x.Name).ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static List<Type> GetAssemblyTypesTaggedWithPropertyAttribute(Assembly assembly, Attribute attribute)
            => new List<Type>
            (
                from type in assembly.GetTypes()
                where TypeDescriptor.GetProperties(type, new Attribute[] { attribute }) != null
                select type
            );
    }
}
