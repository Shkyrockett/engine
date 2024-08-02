// <copyright file="WinformsReflection.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using static Engine.EngineReflection;

namespace Engine.WindowsForms;

/// <summary>
/// The winforms reflection class.
/// </summary>
public static class WinformsReflection
{
    /// <summary>
    /// The angle editor attribute (readonly). Value: new EditorAttribute(typeof(AngleEditor), typeof(UITypeEditor)).
    /// </summary>
    private static readonly Attribute angleEditorAttribute = new EditorAttribute(typeof(AngleEditor), typeof(UITypeEditor));

    /// <summary>
    /// Initializes static members of the <see cref="WinformsReflection"/> class.
    /// </summary>
    static WinformsReflection()
    {
        //var assemblyName = Type.GetType("System.Windows.Forms.Design.StringCollectionEditor")?.Assembly?.GetName();
        var assemblyName = Assembly.GetAssembly(typeof(System.Windows.Forms.Design.ComponentEditorForm)).GetName();
        var version = assemblyName.Version.ToString();
        var token = assemblyName.GetPublicKeyTokenFromAssembly();

        // No clue why System.Design.StringCollectionEditor is internal rather than public.
        TypeDescriptor.AddAttributes(typeof(StringCollection), new EditorAttribute($"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version = {version}, Culture = neutral, PublicKeyToken = {token}", typeof(UITypeEditor)));


        // Add the AngleEditor EditorAttribute UITypeEditor attribute to all properties tagged with the GeometryAngleAttribute attribute.
        Attribute lookupAttribute = new GeometryAngleRadiansAttribute();

        foreach (var item in ListTypesTaggedWithPropertyAttribute(lookupAttribute))
        {
            ReplacePropertyAttribute(item, lookupAttribute, angleEditorAttribute);
        }
    }

    /// <summary>
    /// Call this method to touch the <see cref="WinformsReflection"/> class so the static constructor initializes.
    /// </summary>
    public static void Tickle()
    { }
}
