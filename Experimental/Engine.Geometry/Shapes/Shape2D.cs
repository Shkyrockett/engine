// <copyright file="Shape2D.cs" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// Base <see cref="Shape2D"/> class for using as a template for various shapes.
    /// </summary>
    /// <seealso cref="Engine.IShape" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Shape2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Shape2D
        : IShape, IBoundable
    { }
}