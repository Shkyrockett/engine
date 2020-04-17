// <copyright file="WinformsReflection.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// The engine winforms reflection class.
    /// </summary>
    public static class EngineWinformsReflection
    {
        /// <summary>
        /// Call this method to touch the <see cref="EngineWinformsReflection"/> class so the static constructor initializes.
        /// </summary>
        public static void Tickle()
        { }

        /// <summary>
        /// The .NET Standard does not support PropertyGrid modifier attributes. But we can add them ourselves.
        /// </summary>
        static EngineWinformsReflection()
        {
            Attribute expandableAttribute = new TypeConverterAttribute(typeof(ExpandableObjectConverter));
            Attribute expandableListAttribute = new TypeConverterAttribute(typeof(ExpandableCollectionConverter));

            Attribute lookupExpandableAttribute = new ExpandableAttribute();
            Attribute lookupExpandableListAttribute = new ExpandableListAttribute();

            // Replace all expandable types.
            foreach (var item in ListTypesTaggedWithPropertyAttribute(lookupExpandableAttribute))
            {
                ReplacePropertyAttribute(item, lookupExpandableAttribute, expandableAttribute);
            }

            // Replace all expandable list types.
            foreach (var item in ListTypesTaggedWithPropertyAttribute(lookupExpandableListAttribute))
            {
                ReplacePropertyAttribute(item, lookupExpandableListAttribute, expandableListAttribute);
            }
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
        /// List all types that contain a property tagged with a specified attribute.
        /// </summary>
        /// <returns>The attribute to look for.</returns>
        public static List<Type> ListTypesTaggedWithPropertyAttribute(Attribute attribute)
        {
            var objectType = attribute.GetType();
            var assembly = Assembly.GetAssembly(objectType);
            return GetAssemblyTypesTaggedWithPropertyAttribute(assembly, attribute);
        }

        /// <summary>
        /// The list static factory constructors.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
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
        public static List<Type> GetAssemblyTypes(Assembly assembly, Type classType)
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
        public static List<Type> GetAssemblyInterfaces(Assembly assembly, Type classType)
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
        /// <para>http://stackoverflow.com/questions/4852879/get-all-types-in-assembly-with-custom-attribute</para>
        /// </remarks>
        public static List<Type> GetAssemblyTypeAttributes(Assembly assembly, Type attributeType)
            => new List<Type>
            (
                from type in assembly.GetTypes()
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
                from type in assembly.GetTypes()
                where TypeDescriptor.GetProperties(type, new Attribute[] { attribute }) != null
                select type
            );
    }
}
