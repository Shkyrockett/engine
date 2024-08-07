﻿// <copyright file="Star.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>http://csharphelper.com/blog/2014/08/draw-a-non-intersecting-star-in-c/</summary>
// <remarks></remarks>

using System.Runtime.Serialization;

namespace Engine;

/// <summary>
/// The non int star class.
/// </summary>
[DataContract, Serializable]
//[GraphicsObject]
public class NonIntStar
    : PolygonContour2D
{
    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
    {
        if (this is null)
        {
            return "ElipticStar";
        }

        return nameof(Star);
    }
}
