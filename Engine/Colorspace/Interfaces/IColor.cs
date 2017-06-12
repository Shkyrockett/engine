// <copyright file="Colors.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.Colorspace
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="C"></typeparam>
    public interface IColor<C>
        : IFormattable, //IComparable<C>, //IConvertible,
        IEquatable<C> where C : struct, IColor<C>
    { }
}
