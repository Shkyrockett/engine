// <copyright file="EngineReflection.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Engine.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// The engine reflection class.
    /// </summary>
    public static class EngineReflection
    {
        /// <summary>
        /// Add attributes to framework components so they can be listed.
        /// </summary>
        /// <remarks>
        /// <para>https://social.msdn.microsoft.com/Forums/en-US/3c73a473-2e1e-4e2c-8da8-c127f68dabdc/adding-custom-attributes-to-properties-at-run-time-in-c?forum=netfxbcl</para>
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
        /// <para>http://tenera-it.be/blog/2011/06/add-attriutes-to-a-property-at-runtime/</para>
        /// </remarks>
        public static void ReplacePropertyAttribute(Type type, Attribute searchAttribute, params Attribute[] uiEditorAttributes)
        {
            var props = TypeDescriptor.GetProperties(type, new Attribute[] { searchAttribute });
            foreach (PropertyDescriptor prop in props)
            {
                // AttributeArray-property is not accessible
                // => use reflection to get and set it.
                var attributeArrayPropInfo = prop.GetType().GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic);
                var attributeArray = (attributeArrayPropInfo.GetValue(prop, null) as Attribute[]).ToList();
                attributeArray.AddRange(uiEditorAttributes);
                attributeArrayPropInfo.SetValue(prop, attributeArray.ToArray(), null);
            }
        }

        /// <summary>
        /// List all objects derived from the <see cref="Shape2D"/> class.
        /// </summary>
        /// <returns>A list of types that are derived from the <see cref="Shape2D"/> class.</returns>
        public static List<Type> ListShapes()
        {
            var shapeType = typeof(Shape2D);
            var assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyTypes(assembly, shapeType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Tool"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Tool"/> Class.</returns>
        public static List<Type> ListTools()
        {
            var toolType = typeof(Tool);
            var assembly = Assembly.GetAssembly(toolType);
            return GetAssemblyTypes(assembly, toolType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Brush"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Brush"/> Class.</returns>
        public static List<Type> ListBrushes()
        {
            var brushType = typeof(Brush);
            var assembly = Assembly.GetAssembly(brushType);
            return GetAssemblyTypes(assembly, brushType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Pen"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Pen"/> Class.</returns>
        public static List<Type> ListPens()
        {
            var penType = typeof(Pen);
            var assembly = Assembly.GetAssembly(penType);
            return GetAssemblyTypes(assembly, penType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Tool"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Tool"/> Class.</returns>
        public static List<Type> ListFileObjects()
        {
            var objectType = typeof(FileObjectAttribute);
            var assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypeAttributes(assembly, objectType);
        }

        /// <summary>
        /// List all objects derived from the <see cref="Tool"/> Class.
        /// </summary>
        /// <returns>A list of all types that are derived from the <see cref="Tool"/> Class.</returns>
        public static List<Type> ListGraphicsObjects()
        {
            var objectType = typeof(GraphicsObjectAttribute);
            var assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypeAttributes(assembly, objectType);
        }

        /// <summary>
        /// List all types that contain a property tagged with a specified attribute.
        /// </summary>
        /// <returns>The attribute to look for.</returns>
        public static List<Type> ListTypesTaggedWithPropertyAttribute(Attribute attribute)
        {
            var objectType = attribute?.GetType();
            var assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypesTaggedWithPropertyAttribute(assembly, attribute);
        }

        /// <summary>
        /// The list units.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Type> ListUnits(Type units)
        {
            var assembly = Assembly.GetAssembly(units);
            return GetAssemblyInterfaces(assembly, units);
        }

        /// <summary>
        /// The list static factory constructors.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<MethodInfo> ListStaticFactoryConstructors(Type type)
            => new List<MethodInfo>
            (
                from method in type?.GetMethods()
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
                from type in assembly?.GetTypes()
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
                from type in assembly?.GetTypes()
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
        /// <para>http://stackoverflow.com/questions/4852879/get-all-types-in-assembly-with-custom-attribute</para>
        /// </remarks>
        private static List<Type> GetAssemblyTypeAttributes(Assembly assembly, Type attributeType)
            => new List<Type>
            (
                from type in assembly?.GetTypes()
                where Attribute.IsDefined(type, attributeType)
                select type
            ).OrderBy(x => x.Name).ToList();

        /// <summary>
        /// Get the assembly types tagged with property attribute.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Type> GetAssemblyTypesTaggedWithPropertyAttribute(Assembly assembly, Attribute attribute)
            => new List<Type>
            (
                from type in assembly?.GetTypes()
                where TypeDescriptor.GetProperties(type, new Attribute[] { attribute }) is not null
                select type
            );

        /// <summary>
        /// Gets the public key token from assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public static string GetPublicKeyTokenFromAssembly(this AssemblyName assemblyName) => string.Concat(assemblyName?.GetPublicKeyToken()?.Select(b => b.ToString("x2", CultureInfo.InvariantCulture))) ?? "None";
    }
}
